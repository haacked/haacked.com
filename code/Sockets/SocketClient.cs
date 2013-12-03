using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using log4net;

namespace GU.Net.Sockets
{
	/// <summary>
	/// Represents a socket client connected to an instance of SocketServer.
	/// </summary>
	public class SocketClient : IDisposable
	{
		private static readonly ILog RawReceivedDataLog = LogManager.GetLogger("RawTcpReceivedData");
		private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		const double BUFFER_SIZE_WARNING_THRESHOLD_FRACTION = .90; //As a fraction of the total buffer.

		#region private members
		private static int _uniqueID = 0;
		bool _receiving = false;
		bool _isDisposed = false;
		byte[] _buffer;
		int _bufferSize;
		int _id;
		DateTime _connectDate = DateTime.MinValue;
		TimeSpan _timeOut = TimeSpan.MaxValue;
		TimeSpan _sendTimeout = TimeSpan.MaxValue;
		SetTimeoutDelegate AsyncSetTimeout = null;
		ManualResetEvent _timeoutResetEvent = new ManualResetEvent(false);
		Socket _socket = null;
		TimeSpan _readTimeout;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="socket">The actual System.Net.Socket instance.</param>
		/// <param name="bufferSize">Size of the buffer when receiving and sending data.</param>
		/// <param name="readTimeout">Amount of time to wait for data from the client before terminating the client socket.</param>
		/// <param name="sendTimeout">Amount of time to wait when sending data to a client socket.</param>
		protected internal SocketClient(Socket socket, int bufferSize, TimeSpan readTimeout, TimeSpan sendTimeout)
		{
			_socket = socket;
			_bufferSize = bufferSize;
			_readTimeout = readTimeout;
			_sendTimeout = sendTimeout;
		}

		/// <summary>
		/// Constructor. Sets the buffer size to 4096 and the readTimeout to 
		/// TimeSpan.MaxValue.
		/// </summary>
		/// <param name="socket">The actual System.Net.Socket instance.</param>
		protected internal SocketClient(Socket socket) : this(socket, 4096, TimeSpan.MaxValue, TimeSpan.MaxValue)
		{}

		/// <summary>
		/// Constructor. Sets the readTimeout to TimeSpan.MaxValue.
		/// </summary>
		/// <param name="socket">The actual System.Net.Socket instance.</param>
		/// <param name="bufferSize">Size of the buffer when receiving and sending data.</param>
		protected internal SocketClient(Socket socket, int bufferSize) : this(socket, bufferSize, TimeSpan.MaxValue, TimeSpan.MaxValue)
		{}
		#endregion

		/// <summary>
		/// Event raised when new data is received. Assumes 
		/// ASCII encoding.
		/// </summary>
		public event TextReceivedEventHandler TextReceived;

		/// <summary>
		/// Event raised when a remote client disconnects. 
		/// This event is not fired when disposing or when the 
		/// server shuts down the connection.
		/// </summary>
		public event SocketDisconnectedEventHandler SocketDisconnected;

		/// <summary>
		/// The connected client socket.
		/// </summary>
		public virtual Socket ClientSocket 
		{
			get
			{
				return _socket;
			}
		}

		/// <summary>
		/// Indicates that the SocketClient should begin receiving data 
		/// from the actual client Socket.
		/// </summary>
		public void BeginReceive()
		{
			if(_isDisposed)
				throw new ObjectDisposedException(GetType().Name, "Object is already disposed..");

			if(_receiving)
				return;

			_connectDate = DateTime.Now;

			unchecked
			{
				_id = Interlocked.Increment(ref _uniqueID);
			}
			Log.Info(_id + ": Waiting to receive data...");

			_receiving = true;

			_buffer = new byte[BufferSize];
			
			SetBeginReceiveCallback(_buffer);
			
			//put timeout here?
			Log.Info("BeginReceive called.");
		}

		/// <summary>
		/// Sends the string data using the specified encoding.  Uses the 
		/// SendTimeout defined by the server to determine how long to wait 
		/// for the client to receive the sent data.
		/// </summary>
		/// <remarks>Uses ASCII encoding.</remarks>
		/// <returns>True if successful</returns>
		/// <param name="text">The string to send.</param>
		public bool Send(string text)
		{
			return Send(text, Encoding.ASCII);
		}

		/// <summary>
		/// Sends the string data using the specified encoding.  Uses the 
		/// SendTimeout defined by the server to determine how long to wait 
		/// for the client to receive the sent data.
		/// </summary>
		/// <returns>True if successful</returns>
		/// <param name="text">The string to send.</param>
		/// <param name="encoding">The System.Text.Encoding of the string.</param>
		public bool Send(string text, Encoding encoding)
		{
			return Send(text, encoding, _sendTimeout);
		}

		/// <summary>
		/// Sends the string data using the specified encoding.  Allows overriding 
		/// the SendTimeout defined for this instance.
		/// </summary>
		/// <returns>True if successful</returns>
		/// <param name="text">The string to send.</param>
		/// <param name="encoding">The System.Text.Encoding of the string.</param>
		/// <param name="timeout">Amount of time to wait before assuming the send operation failed.</param>
		public bool Send(string text, Encoding encoding, TimeSpan timeout)
		{
			if(_isDisposed)
				throw new ObjectDisposedException(GetType().Name, "Object is already disposed..");

			byte[] buffer = encoding.GetBytes(text);
			
			IAsyncResult result = _socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), _socket);
			Log.Info("Async Sent Data To Client");
			return true; //TODO: Handle this better.
		}

		private void SendCallback(IAsyncResult ar)
		{
			int bytesSent = _socket.EndSend(ar);
			Log.Info(bytesSent + " bytes sent to the client socket.");
		}

		private void SetBeginReceiveCallback(byte[] buffer)
		{
			if(ReadTimeout != TimeSpan.MaxValue)
				AsyncSetTimeout = new SetTimeoutDelegate(SetTimeout);

			try
			{
				ClientSocket.BeginReceive( buffer, 0, BufferSize, 0, new AsyncCallback(DataReceived), null);
				if(AsyncSetTimeout != null)
					AsyncSetTimeout();
			}
			catch(System.ObjectDisposedException)
			{
				if(_receiving || _isDisposed)
					Log.Info(_id + ": Stopped Waiting for data because disposing.");
				else
					throw;
			}
			catch(SocketException)
			{
				if(_receiving || _isDisposed)
					Log.Info(_id + ": Stopped Waiting for data because disposing or Timed Out.");
				else
					throw;
			}
		}

		private delegate void SetTimeoutDelegate();
		/// <summary>
		/// Forces a DisconnectedEvent after the TimeOut expires.
		/// </summary>
		void SetTimeout()
		{
			_timeoutResetEvent.Reset();
			
			//Wait for the signal from DataReceived that data was received.
			if(!_timeoutResetEvent.WaitOne(ReadTimeout, false))
			{
				//Doh!  We haven't received data in time. 
				OnDisconnect(SocketDisconnectReason.TimedOut);
			}
		}

		/// <summary>
		/// Asynchronous Callback method called when data is received from 
		/// the remote client socket.
		/// </summary>
		/// <param name="ar"></param>
		private void DataReceived(IAsyncResult ar)
		{
			if(Thread.CurrentThread.Name == null || Thread.CurrentThread.Name.Length == 0)
				Thread.CurrentThread.Name = "ClientRead_" + Thread.CurrentThread.GetHashCode();

			if(!_receiving || this._isDisposed) //While waiting for data, we might get disposed.
				return;

			_timeoutResetEvent.Set(); // Signals that the timeout should not occur.

			Log.Info(_id + ": BEFORE EndReceive Data. _receiving = " + _receiving + " " + GetHashCode());
			// Blocks till there's data to read.
			int bytesRead = ClientSocket.EndReceive(ar);
			Log.Info(_id + ": AFTER EndReceive Data.");

			if(bytesRead > (BufferSize * BUFFER_SIZE_WARNING_THRESHOLD_FRACTION))
			{
				Log.Warn(_id + ": " + String.Format("Socket sent {0} bytes which is within the {1}% warning threshold of the buffer size {2}", bytesRead, 100 * BUFFER_SIZE_WARNING_THRESHOLD_FRACTION, BufferSize));
			}

			if(bytesRead > 0)
			{
				Log.Info(_id + ": Received " + bytesRead + " bytes!");

				#region Logs Raw Bytes
				// Log the raw information.
				if(RawReceivedDataLog.IsInfoEnabled)
				{
					string byteData = _buffer[0].ToString();
					for(int i = 1; i < bytesRead; i++)
					{
						if(_buffer[i] == (byte)'|')
							byteData += "|";
						else if(_buffer[i] == (byte)';')
							byteData += ";";
						else
							byteData += ",";
						byteData += _buffer[i];
					}
					RawReceivedDataLog.Info(byteData);
				}
				#endregion

				OnTextReceived(Encoding.ASCII.GetString(_buffer, 0, bytesRead));
				
				if(!_receiving || this._isDisposed) //While waiting for more data, we might get disposed.
					return;

				Log.Info(_id + ": Waiting to receive more data...");				
				
				// More data could be coming.
				SetBeginReceiveCallback(_buffer);
			}
			else
			{
				Log.Info(_id + ": No more data...");
				OnDisconnect(SocketDisconnectReason.RemoteClientClosed);
			}
		}

		/// <summary>
		/// Called when the remote client sends data.
		/// </summary>
		protected virtual void OnTextReceived(string text)
		{
			if(TextReceived != null)
				TextReceived(this, new TextReceivedEventArgs(text));
		}

		/// <summary>
		/// Called when the remote client disconnects.
		/// </summary>
		protected virtual void OnDisconnect(SocketDisconnectReason disconnectReason)
		{
			_receiving = false; //no longer reading.

			if(SocketDisconnected != null)
			{
				Log.Info(_id + ": Firing SocketDisconnected event. Reason is " + disconnectReason.ToString());
				//SocketDisconnected(this, new SocketDisconnectedEventArgs(_connectDate, DateTime.Now));
				GU.EventHelper.FireAsync(SocketDisconnected, this, new SocketDisconnectedEventArgs(disconnectReason, _connectDate, DateTime.Now));
			}
		}

		
		/// <summary>
		/// Size of the buffer to use when reading data from 
		/// the underlying client socket.
		/// </summary>
		public int BufferSize 
		{
			get
			{
				return _bufferSize;
			}
		}

		/// <summary>
		/// Represents the amount of time this client will wait 
		/// on a read operation before deciding to kill itself.
		/// TimeSpan.MaxValue means it will wait indefinitely.
		/// </summary>
		public TimeSpan ReadTimeout 
		{
			get
			{
				return _readTimeout;
			}
			set
			{
				_readTimeout = value;
			}
		}

		/// <summary>
		/// Amount of time we'll wait for a send operation to complete.
		/// </summary>
		public TimeSpan SendTimeout
		{
			get
			{
				return _sendTimeout;
			}
			set
			{
				_sendTimeout = value;
			}
		}

		#region IDisposable Implementation

		/// <summary>
		/// Implement IDisposable.  
		/// </summary>
		/// <remarks>
		/// Do not make this method virtual.  A derived class should 
		/// not be able to override this method.
		/// </remarks>
		public void Dispose()
		{
			Dispose(true);
			// Take yourself off the Finalization queue 
			// to prevent finalization code for this object
			// from executing a second time.
			GC.SuppressFinalize(this);
		}

		// Dispose(bool disposing) executes in two distinct scenarios.
		// If disposing equals true, the method has been called directly
		// or indirectly by a user's code. Managed and unmanaged resources
		// can be disposed.
		// If disposing equals false, the method has been called by the 
		// runtime from inside the finalizer and you should not reference 
		// other objects. Only unmanaged resources can be disposed.
		protected virtual void Dispose(bool disposing)
		{
			Log.Info(_id + ": Disposing");
			_receiving = false;
			// Check to see if Dispose has already been called.
			if(!_isDisposed)
			{
				// If disposing equals true, dispose all managed 
				// and unmanaged resources.
				if(disposing)
				{
					// Dispose managed resources.  Not safe to do if 
					// not disposing (i.e. if this is being finalized).
					DisposeManagedResources();
				}
				// Note, if not disposing, this is the only thing that is released.
				ReleaseUnmanagedResources();  //Nothing to release.

				// Note that this is not thread safe.
				// Another thread could start disposing the object
				// after the managed resources are disposed,
				// but before the disposed flag is set to true.
				// If thread safety is necessary, it must be
				// implemented by the client.

			}
			_isDisposed = true;         
		}

		// Disposes managed resources
		void DisposeManagedResources()
		{
			Log.Info(_id + ": Closing down SocketClient.");
			if(ClientSocket.Connected)
			{
				ClientSocket.Shutdown(SocketShutdown.Both);
			}
			ClientSocket.Close();
		}

		// Disposes unmanaged resources
		void ReleaseUnmanagedResources()
		{}

		// Use C# destructor syntax for finalization code.
		// This destructor will run only if the Dispose method 
		// does not get called.
		// It gives your base class the opportunity to finalize.
		// Do not provide destructors in types derived from this class.
		~SocketClient()      
		{
			// Do not re-create Dispose clean-up code here.
			// Calling Dispose(false) is optimal in terms of
			// readability and maintainability.
			Dispose(false);
		}

		#endregion
	}

	#region TextReceived event implementation
	public delegate void TextReceivedEventHandler(object source, TextReceivedEventArgs e);
	
	public class TextReceivedEventArgs : System.EventArgs
	{
		string _text;
		
		public TextReceivedEventArgs(string text) : base() 
		{
			_text = text;
		}

		/// <summary>
		/// The text received from the remote client socket. 
		/// This text is converted from ASCII.
		/// </summary>
		public string Text
		{
			get
			{
				return _text;
			}
		}
	}
	#endregion

	#region SocketDisconnect event implementation
	public delegate void SocketDisconnectedEventHandler(object source, SocketDisconnectedEventArgs e);
	
	/// <summary>
	/// Provides data for the SocketDisconnected event.
	/// </summary>
	/// <remarks>
	/// <B>SocketDisconnectedEventArgs</B> contains the time that the SocketClient 
	/// instance started to receive data from a remote client socket as well as 
	/// the time that the remote client was disconnected. The <b>SocketDisconnected</b> 
	/// event is raised when a remote client is disconnected for one of two 
	/// reasons: It has finished sending data and has nothing else to send, or the 
	/// SocketClient instance handling the remote client has timed out.
	/// </remarks>
	public class SocketDisconnectedEventArgs : System.EventArgs
	{
		DateTime _connectDate;		//Date this client was connected started sending data.
		DateTime _disconnectDate;	//Date this client was disconnected.
		SocketDisconnectReason _reason;
		/// <summary>
		/// buffer
		/// </summary>
		/// <param name="startReadDate">Date this socket was connected and started receiving data.</param>
		public SocketDisconnectedEventArgs(SocketDisconnectReason reason, DateTime connectDate, DateTime disconnectDate) : base() 
		{
			_reason = reason;
			_connectDate = connectDate;
			_disconnectDate = disconnectDate;
		}

		/// <summary>
		/// Date this client was connected and started receiving data from 
		/// the remote client.
		/// </summary>
		public DateTime ConnectDate
		{
			get
			{
				return _connectDate;
			}
		}

		/// <summary>
		/// Date this client was disconnected from the remote clietn.
		/// </summary>
		public DateTime DisconnectDate
		{
			get
			{
				return _disconnectDate;
			}
		}

		/// <summary>
		/// Timespan between connection and disconnection.
		/// </summary>
		public TimeSpan UpTime
		{
			get
			{
				return (_disconnectDate - _connectDate);
			}
		}

		/// <summary>
		/// The reason the remote client was disconnected.
		/// </summary>
		public SocketDisconnectReason DisconnectReason
		{
			get
			{
				return _reason;
			}
		}
	}

	/// <summary>
	/// Specifies a reason that the socket was disconnected.
	/// </summary>
	public enum SocketDisconnectReason
	{
		/// <summary>The remote client socket closed the connection.</summary>
		RemoteClientClosed,
		/// <summary>The SocketClient instance timed out.</summary>
		TimedOut
	}
	#endregion
}
