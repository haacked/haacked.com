using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using log4net;

namespace GU.Net.Sockets
{
	/// <summary>
	/// Implements a scalable and fairly robust Tcp Socket Server. 
	/// </summary>
	public class SocketServer : IDisposable
	{
		private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		
		const int BUFFER_SIZE_DEFAULT = 4096;
		const int PENDING_CONNECTION_QUEUE_SIZE = 100;
		// Amount of time we'll wait for the listener thread to join 
		// when we stop the server.
		const int LISTENER_THREAD_JOIN_TIMEOUT_MINUTES = 1;

		#region private members
		bool _isDisposed = false;
		bool _isRunning = false;
		int _bufferSize;
		TimeSpan _clientReadTimeout = TimeSpan.MaxValue; //indefinite.
		TimeSpan _clientSendTimeout = TimeSpan.MaxValue; //indefinite.
		Thread _listenerThread = null;
		Socket _listener = null;
		ManualResetEvent _clientConnected = new ManualResetEvent(false);
		SocketClientCollection _socketClients = new SocketClientCollection();
		EndPoint _hostEndPoint;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="hostEndPoint">Identifies a Network resource that this server is listening on.</param>
		/// <param name="bufferSize">Size of the buffer to use when receiving data from a connected client.</param>
		/// <param name="clientReadTimeout">amount of time that the server will wait 
		/// for a client to send data before timing the client out.</param>
		/// <param name="clientSendTimeout">amount of time that the server will wait 
		/// for a client to receive data before timing the client out.</param>
		public SocketServer(EndPoint hostEndPoint, int bufferSize, TimeSpan clientReadTimeout, TimeSpan clientSendTimeout)
		{
			_hostEndPoint = hostEndPoint;
			_bufferSize = bufferSize;
			_clientReadTimeout = clientReadTimeout;
			_clientSendTimeout = clientSendTimeout;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="hostEndPoint">Identifies a Network resource that this server is listening on.</param>
		/// <param name="bufferSize">Size of the buffer to use when receiving data from a connected client.</param>
		public SocketServer(EndPoint hostEndPoint, int bufferSize) : this(hostEndPoint, bufferSize, TimeSpan.MaxValue, TimeSpan.MaxValue)
		{}

		/// <summary>
		/// Constructor. Uses the default buffer size (4096) for the buffer.
		/// </summary>
		/// <param name="hostEndPoint"></param>
		public SocketServer(EndPoint hostEndPoint) : this(hostEndPoint, BUFFER_SIZE_DEFAULT, TimeSpan.MaxValue, TimeSpan.MaxValue)
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="hostIpAddress">The IP Address for this server to listen in on.</param>
		/// <param name="port">Port number for this server to listen in on.</param>
		/// <param name="bufferSize">Size of the buffer to use when receiving data from a connected client.</param>
		/// <param name="clientReadTimeout">amount of time that the server will wait 
		/// for a client to send data before timing the client out.</param>
		/// <param name="clientSendTimeout">amount of time that the server will wait 
		/// for a client to receive data before timing the client out.</param>
		public SocketServer(IPAddress hostIpAddress, int port, int bufferSize, TimeSpan clientReadTimeout, TimeSpan clientSendTimeout) : this(new IPEndPoint(hostIpAddress, port), bufferSize, clientReadTimeout, clientSendTimeout)
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="hostIpAddress">The IP Address for this server to listen in on.</param>
		/// <param name="port">Port number for this server to listen in on.</param>
		/// <param name="bufferSize">Size of the buffer to use when receiving data from a connected client.</param>
		public SocketServer(IPAddress hostIpAddress, int port, int bufferSize) : this(new IPEndPoint(hostIpAddress, port), bufferSize)
		{
		}

		/// <summary>
		/// Constructor. Uses the default buffer size (4096) for the buffer.
		/// </summary>
		/// <param name="hostIpAddress">The IP Address for this server to listen in on.</param>
		/// <param name="port">Port number for this server to listen in on.</param>
		public SocketServer(IPAddress hostIpAddress, int port) : this(hostIpAddress, port, BUFFER_SIZE_DEFAULT, TimeSpan.MaxValue, TimeSpan.MaxValue)
		{}

		/// <summary>
		/// Constructor. Listens to the specified port on any IPAddress.
		/// </summary>
		/// <param name="port"></param>
		public SocketServer(int port) : this(IPAddress.Any, port)
		{}
		#endregion

		#region Public interface. Properties and Methods
		/// <summary>
		/// Identifies the network address this server will listen on.
		/// </summary>
		public EndPoint HostEndPoint 
		{
			get
			{
				return _hostEndPoint;
			}
		}
		
		/// <summary>
		/// Creates the Socket that will listen for incoming 
		/// client connections.
		/// </summary>
		/// <remarks>This can be overridden in a subclass to return 
		/// a socket of a different type.</remarks>
		/// <returns>A listening Socket.</returns>
		protected virtual Socket CreateListenerSocket()
		{
			return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		}

		/// <summary>
		/// Creates a SocketClient that encapsulates communication 
		/// with the connected client socket.
		/// </summary>
		/// <returns></returns>
		protected virtual SocketClient CreateSocketClient(Socket clientSocket)
		{
			return new SocketClient(clientSocket, _bufferSize, _clientReadTimeout, _clientSendTimeout);
		}
		
		public event SocketClientConnectedEventHandler SocketClientConnected;

		/// <summary>
		/// Gets and Sets the amount of time that the server will wait 
		/// for a client to send data before timing the client out.
		/// </summary>
		public virtual TimeSpan ClientReadTimeout
		{
			get
			{
				return _clientReadTimeout;
			}
			set
			{
				_clientReadTimeout = value;
			}
		}

		/// <summary>
		/// Gets and Sets the amount of time that the server will wait 
		/// when sending data to a client for the client to receive the 
		/// data before timing the client out.
		/// </summary>
		public virtual TimeSpan ClientSendTimeout
		{
			get
			{
				return _clientSendTimeout;
			}
			set
			{
				_clientSendTimeout = value;
			}
		}

		/// <summary>
		/// Returns the number of clients connected to this server. 
		/// </summary>
		public int ClientCount
		{
			get
			{
				if(_socketClients != null)
					return _socketClients.Count;
				return 0;
			}
		}

		/// <summary>
		/// Size of the buffer used to receive data from clients.
		/// </summary>
		public int BufferSize
		{
			get
			{
				return _bufferSize;
			}
		}

		/// <summary>
		/// Returns true if the server is running.
		/// </summary>
		public bool IsRunning
		{
			get
			{
				return _isRunning;
			}
		}

		/// <summary>
		/// Starts a listener thread.
		/// </summary>
		public void Start()
		{
			if(_isDisposed)
				throw new ObjectDisposedException("SocketServer", "Object is already disposed.");

			if(_isRunning) //Could be called on two different threads. but unlikely.
				return;

			Log.Info("Socket Server Start initiated.");
			
			_isRunning = true;
			_listenerThread = new Thread(new ThreadStart(StartListening));
			_listenerThread.Name = "SocketListener";
			_listenerThread.Start();
			while(!_listenerThread.IsAlive)
				Thread.Sleep(10); //Waiting for thread to get started.

			OnStart();
		}

		/// <summary>
		/// Indicates that server has started.
		/// </summary>
		protected virtual void OnStart()
		{
			//Doesn't do anything yet.
		}

		/// <summary>
		/// Stops the socket server and closes every client connection.
		/// </summary>
		public void Stop()
		{
			Log.Info("About to stop the server.");

			if(_isDisposed)
				throw new ObjectDisposedException("SocketServer", "Object is already disposed.");

			if(!_isRunning)
			{
				Log.Info("Stop() called, but it's not running.");
				return;
			}

			//Remove and close every socket. This is threadsafe.
			if(_socketClients != null)
				_socketClients.Clear();
		
			_isRunning = false;

			WaitForListeningThreadToDie();

			CloseListenerSocket();

			_listener = null;
			_listenerThread = null;

			GC.Collect();
			GC.WaitForPendingFinalizers();

			OnStop();
		}

		/// <summary>
		/// Indicates that server has stopped.
		/// </summary>
		protected virtual void OnStop()
		{
			
		}
		#endregion

		// Main listener thread.  Starts an indefinite loop listening for 
		// new socket clients.  As Each client comes in, it is handed off 
		// to OnClientConnect asynchronously on its own ThreadPool thread.
		private void StartListening()
		{
			Log.Info("Inside Listener Thread.");

			_listener = CreateListenerSocket();
			try
			{
				_listener.Bind(HostEndPoint);
				_listener.Listen(PENDING_CONNECTION_QUEUE_SIZE);

				if(Log.IsInfoEnabled) 
				{
					Log.Info("Socket Listener Started Listening on " + HostEndPoint.ToString());
				}

				// This loops indefinitely listening for client connections.
				while(_isRunning)
				{
					// Set event to nonsignaled state.
					_clientConnected.Reset();

					Log.Info("Ready to Accept Incoming Connection.");

					// Perform a blocking call to accept requests.
					// You could also user server.AcceptSocket() here.
					_listener.BeginAccept(new AsyncCallback(AcceptIncomingConnection), _listener);

					if(Log.IsInfoEnabled) Log.Info("Waiting For a Connection...");
					// Wait for the connection to be made before continuing.
					// But do so in a non-blocking manner.
					while(true)
					{
						if(_clientConnected.WaitOne(1000, false))
						{
							Log.Info("Breaking out of the infinite loop because we have a new connection.");
							break;
						}
						if(!_isRunning)
						{
							Log.Warn("Breaking out of the infinite loop because the server is stopping.");
							break;
						}
					}
				}
				Log.Info("Exiting Listener Thread Gracefully.");
			}
			catch(Exception e)
			{
				//TODO: Consider starting up again.
				Log.Error("Exception thrown in StartListening() thread.  Server is shutting down.", e);
			}
		}

		/// <summary>
		/// Callback method called when attempting to accept a new client socket 
		/// conneciton. This method is called asynchronously on another thread.
		/// </summary>
		/// <param name="ar"></param>
		private void AcceptIncomingConnection(IAsyncResult ar)
		{
			//Signal the mainthread to continue.
			_clientConnected.Set();

			if(Thread.CurrentThread.Name == null || Thread.CurrentThread.Name.Length == 0)
				Thread.CurrentThread.Name = "SocketClient_" + _socketClients.Count;
			
			Log.Info("Incoming Connection Accepted.");
			LogThreadPoolCount();
		
			Log.Info("Waiting for EndAccept...");
			Socket clientSocket = _listener.EndAccept(ar);
			Log.Info("Client Accepted.");
		
			//This state object is created per Client.
			SocketClient socketClient = CreateSocketClient(clientSocket);
			Log.Info("SETTING Client Read Timeout to " + ClientReadTimeout);
			socketClient.ReadTimeout = ClientReadTimeout;
			Log.Info("SETTING Client Send Timeout to " + ClientSendTimeout);
			socketClient.SendTimeout = ClientSendTimeout;

			Log.Info("Adding Client to collection.");
			_socketClients.Add(socketClient);
			Log.Info(_socketClients.Count + " Clients Connected.");
			OnSocketClientConnected(socketClient);
		}

		/// <summary>
		/// Fires the SocketClientConnect event.
		/// </summary>
		/// <param name="client"></param>
		protected virtual void OnSocketClientConnected(SocketClient client)
		{
			if(SocketClientConnected != null)
			{
				Log.Info("Firing ClientSocketConnected Event");
				
				//TODO: We may want to fire this asynchronously so that we do 
				//		not block on this thread.
				SocketClientConnected(this, new SocketClientConnectedEventArgs(client));
			}
		}

		#region utility methods
		// Logs the number of threads available in the thread pool.
		private void LogThreadPoolCount()
		{
			int availableIOThreadcount;
			int availableThreadCount;
			ThreadPool.GetAvailableThreads(out availableThreadCount, out availableIOThreadcount);

			if(availableThreadCount == 0)
				Log.Warn("No Worker Threads available in the ThreadPool.");
			else
				Log.Info(availableThreadCount + " ThreadPool Worker Threads available.");

			if(availableIOThreadcount == 0)
				Log.Warn("No Asynchronous I/O Completion Port Threads available in the ThreadPool.");
			else
				Log.Info(availableIOThreadcount + " Asynchronous I/O Completion Port Threads available.");
		}

		private void CloseListenerSocket()
		{
			if(_listener != null)
			{
				Log.Info("Closing the Listener.");
				
				try
				{
					if(_listener.Connected)
					{
						Log.Warn("Calling Shutdown on Listener Socket.");
						_listener.Shutdown(SocketShutdown.Both);
					}
				}
				catch(SocketException e)
				{
					Log.Error("Error occurred while shutting down the listener.", e);
				}
				Log.Warn("Calling Close on Listener Socket.");
				_listener.Close();
			}
		}

		private void WaitForListeningThreadToDie()
		{
			if(_listenerThread == null)
				return;
			
			Log.Info("Going to give listener thread " + LISTENER_THREAD_JOIN_TIMEOUT_MINUTES + " minutes to join us.");

			if(!_listenerThread.Join(TimeSpan.FromMinutes(LISTENER_THREAD_JOIN_TIMEOUT_MINUTES)))
			{
				Log.Error("Listener thread did not join within " + LISTENER_THREAD_JOIN_TIMEOUT_MINUTES + " minutes.");
			}
			else
			{
				Log.Info("Listener Thread Joined Successfully.");
			}
		}
		#endregion

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
			if(this._isRunning)
				Stop();

			if(_socketClients != null)
			{
				_socketClients.Dispose();
			}

			if(_clientConnected != null)
				_clientConnected.Close();
		}

		// Disposes unmanaged resources
		void ReleaseUnmanagedResources()
		{}

		// Use C# destructor syntax for finalization code.
		// This destructor will run only if the Dispose method 
		// does not get called.
		// It gives your base class the opportunity to finalize.
		// Do not provide destructors in types derived from this class.
		~SocketServer()      
		{
			// Do not re-create Dispose clean-up code here.
			// Calling Dispose(false) is optimal in terms of
			// readability and maintainability.
			Dispose(false);
		}

		#endregion
	}

	#region SocketClientConnect event implementation
	public delegate void SocketClientConnectedEventHandler(object source, SocketClientConnectedEventArgs e);
	
	/// <summary>
	/// Provides data for the SocketClientConnected event.
	/// </summary>
	/// <remarks>The event is raised when a new remote client 
	/// socket is connected to the server.</remarks>
	public class SocketClientConnectedEventArgs : System.EventArgs
	{
		private SocketClient _socketClient;
		
		public SocketClientConnectedEventArgs(SocketClient socketClient) : base() 
		{
			_socketClient = socketClient;
		}

		public SocketClient SocketClient
		{
			get
			{
				return _socketClient;
			}
		}
	}
	#endregion
}
