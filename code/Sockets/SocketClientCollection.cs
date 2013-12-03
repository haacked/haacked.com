using System;
using System.Collections;
using System.Net.Sockets;
using GU.Threading;
using log4net;

namespace GU.Net.Sockets
{
	/// <summary>
	/// ThreadSafe socket collection class.
	/// </summary>
	public class SocketClientCollection : IDisposable
	{
		private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		
		ArrayList _connections = new ArrayList();
		bool _isDisposed = false;

		public SocketClientCollection() : base()
		{
		}

		/// <summary>
		/// Tries to add the SocketClient instance to the collection. 
		/// If there is no room, it returns false.
		/// </summary>
		/// <param name="socket"></param>
		/// <returns></returns>
		/// <exception cref="GU.Threading.LockTimeoutException">
		/// Thrown if a lock on the internal list cannot be acquired.
		/// </exception>
		public void Add(SocketClient client)
		{
			if(this._isDisposed)
				throw new ObjectDisposedException(GetType().Name, "This object is disposed.");

			using(TimedLock.Lock(_connections))
			{
				_connections.Add(client);
			}
			client.SocketDisconnected += new SocketDisconnectedEventHandler(SocketDisconnected);
		}

		/// <summary>
		/// Removes the client from the collection and disposes it.
		/// </summary>
		/// <param name="client"></param>
		protected void Remove(SocketClient client)
		{
			using(TimedLock.Lock(_connections))
			{
				_connections.Remove(client);
			}
			client.Dispose();
		}

		/// <summary>
		/// Clears and disposes the SocketClient instances within 
		/// this collection in a thread safe manner.
		/// </summary>
		/// <exception cref="GU.Threading.LockTimeoutException">
		/// Thrown if a lock on the internal list cannot be acquired.
		/// </exception>
		public void Clear()
		{
			if(this._isDisposed)
				throw new ObjectDisposedException(GetType().Name, "This object is disposed.");

			Log.Info("Clearing SocketClientCollection.");

			using(TimedLock.Lock(_connections))
			{
				while(_connections.Count > 0)
				{
					SocketClient client = _connections[0] as SocketClient;
					_connections.RemoveAt(0);
					client.Dispose();
				}
			}
		}

		/// <summary>
		/// Returns the number SocketClient instances in this collection.
		/// </summary>
		public int Count
		{
			get
			{
				if(this._isDisposed)
					throw new ObjectDisposedException(GetType().Name, "This object is disposed.");

				using(TimedLock.Lock(_connections))
				{
					return _connections.Count;
				}
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
			Clear();
		}

		// Disposes unmanaged resources
		void ReleaseUnmanagedResources()
		{}

		// Use C# destructor syntax for finalization code.
		// This destructor will run only if the Dispose method 
		// does not get called.
		// It gives your base class the opportunity to finalize.
		// Do not provide destructors in types derived from this class.
		~SocketClientCollection()      
		{
			// Do not re-create Dispose clean-up code here.
			// Calling Dispose(false) is optimal in terms of
			// readability and maintainability.
			Dispose(false);
		}

		#endregion

		/// <summary>
		/// Method called when a client is disconnected.  This is fired 
		/// asynchronously.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void SocketDisconnected(object source, SocketDisconnectedEventArgs e)
		{
			SocketClient client = source as SocketClient;
			if(client != null)
			{
				Log.Info("Client is disconnected and is being removed. It was connected for " + e.UpTime.TotalMilliseconds + "ms. ");
				Remove(client);
			}
		}
	}
}
