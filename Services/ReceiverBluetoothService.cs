

using GalaSoft.MvvmLight;
using InTheHand.Net.Sockets;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;


namespace DialogEngine.Services
{
    public class ReceiverBluetoothService : ObservableObject,IDisposable
    {
        private readonly Guid mServiceClassId;
        private Action<string> mResponseAction;
        private BluetoothListener mListener;
        private CancellationTokenSource mCancelSource;
        private bool mWasStarted;
        private string mStatus;
        private BluetoothLEAdvertisementWatcher advertisement;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiverBluetoothService" /> class.
        /// </summary>
        public ReceiverBluetoothService()
        {
            mServiceClassId = new Guid("0e6114d0-8a2e-477a-8502-298d1ff4b4ba");

            advertisement = new BluetoothLEAdvertisementWatcher();
            advertisement.Received += OnAdvertisementReceived;

            advertisement.Start();
        }

        /// <summary>
        /// Gets or sets a value indicating whether was started.
        /// </summary>
        /// <value>
        /// The was started.
        /// </value>
        public bool WasStarted
        {
            get { return mWasStarted; }
            set { Set(() => WasStarted, ref mWasStarted, value); }
        }

        private void OnAdvertisementReceived(BluetoothLEAdvertisementWatcher watcher, BluetoothLEAdvertisementReceivedEventArgs eventArgs)
        {
            // Tell the user we see an advertisement and print some properties
            Console.WriteLine(String.Format("Advertisement:"));
            Console.WriteLine(String.Format("  BT_ADDR: {0}", eventArgs.BluetoothAddress));
            Console.WriteLine(String.Format("  FR_NAME: {0}", eventArgs.Advertisement.LocalName));
            Console.WriteLine(eventArgs.Advertisement.ManufacturerData?.ToString() + " data ");
  
        }

        /// <summary>
        /// Starts the listening from Senders.
        /// </summary>
        /// <param name="_reportAction">
        /// The report Action.
        /// </param>
        public void Start(Action<string> _reportAction)
        {
            WasStarted = true;
            mResponseAction = _reportAction;
            if (mCancelSource != null && mListener != null)
            {
                Dispose(true);
            }
            mListener = new BluetoothListener(mServiceClassId)
            {
                ServiceName = "MyService"
            };
            mListener.Start();

            mCancelSource = new CancellationTokenSource();

            Task.Run(() => Listener(mCancelSource));
        }

        /// <summary>
        /// Stops the listening from Senders.
        /// </summary>
        public void Stop()
        {
            WasStarted = false;
            mCancelSource.Cancel();
        }

        /// <summary>
        /// Listeners the accept bluetooth client.
        /// </summary>
        /// <param name="_token">
        /// The token.
        /// </param>
        private void Listener(CancellationTokenSource _token)
        {
            try
            {
                while (true)
                {
                    var client = mListener.AcceptBluetoothClient();

                    using (var _client = mListener.AcceptBluetoothClient())
                    {
                        if (_token.IsCancellationRequested)
                        {
                            return;
                        }

                        using (var _streamReader = new StreamReader(_client.GetStream()))
                        {
                            try
                            {
                                var _content = _streamReader.ReadToEnd();
                                if (!string.IsNullOrEmpty(_content))
                                {
                                    mResponseAction(_content);
                                }
                            }
                            catch (IOException)
                            {
                                _client.Close();
                                break;
                            }
                        }
                    }
                }
            }
            catch(InvalidOperationException ex)
            {
                var a = ex;
            }
            catch (Exception _exception)
            {
                var a = _exception;
                // todo handle the exception
                // for the sample it will be ignored
            }
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected virtual void Dispose(bool _disposing)
        {
            if (_disposing)
            {
                if (mCancelSource != null)
                {
                    mListener.Stop();
                    mListener = null;
                    mCancelSource.Dispose();
                    mCancelSource = null;
                }
            }
        }
    }
}
