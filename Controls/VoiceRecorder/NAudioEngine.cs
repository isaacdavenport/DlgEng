﻿//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Threading;
using log4net;
using NAudio.Wave;


namespace DialogEngine.Controls.VoiceRecorder
{
    /// <summary>
    /// 
    /// </summary>
    public class NAudioEngine : INotifyPropertyChanged, ISpectrumPlayer, IDisposable
    {
        #region - Fields -
        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly int mcRepeatThreshold = 200;
        private readonly DispatcherTimer mcPositionTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle);
        private readonly int mcfftDataSize = (int)FFTDataSize.FFT2048;
        // singleton instance
        private static NAudioEngine msInstance;
        private bool mDisposed;
        // player's conditions
        private bool mCanPlay;
        private bool mCanPause;
        private bool mCanStop;

        // player's state
        private bool mIsPlaying;
        private bool msIsRecording;

        private bool mInChannelTimerUpdate;
        private double mChannelLength;
        private double mChannelPosition;
        private bool mInChannelSet;
        // records sound 
        private WaveIn mWaveInDevice;
        // writes recorded bytes to .mp3 file
        private WaveFileWriter mWaveFileWriter;
        // plays .mp3 file
        private WaveOut mWaveOutDevice;
        private WaveStream mActiveStream;
        private WaveChannel32 mInputStream;
        private SampleAggregator mSampleAggregator;
        private TimeSpan mRepeatStart;
        private TimeSpan mRepeatStop;
        private bool mInRepeatSet;

        #endregion

        #region  - Singleton -
        /// <summary>
        /// Singleton
        /// </summary>
        public static NAudioEngine Instance
        {
            get
            {
                if (msInstance == null)
                    msInstance = new NAudioEngine();
                return msInstance;
            }
        }

        #endregion

        #region - Constructor -
        /// <summary>
        /// Default constructor
        /// </summary>
        private NAudioEngine()
        {
            mcPositionTimer.Interval = TimeSpan.FromMilliseconds(50);
            mcPositionTimer.Tick += _positionTimer_Tick;
        }
        
        #endregion

        #region - IDisposable -

        /// <summary>
        /// Destructor
        /// </summary>
        public void Dispose()
        {
            dispose(true);            
            GC.SuppressFinalize(this);
        }

        protected virtual void dispose(bool disposing)
        {
            if(!mDisposed)
            {
                if(disposing)
                {
                    _stopAndCloseStream();
                }

                mDisposed = true;
            }
        }

        #endregion

        #region - ISpectrumPlayer -

        public bool GetFFTData(float[] _fftDataBuffer)
        {
            bool status = IsPlaying || IsRecording;

            if (status)
            {
                mSampleAggregator.GetFFTResults(_fftDataBuffer);
            }
            return status;
        }

        public int GetFFTFrequencyIndex(int frequency)
        {         
            double _maxFrequency;

            if (IsRecording)
            {
                _maxFrequency = mWaveInDevice.WaveFormat.SampleRate / 2.0d;
            }
            else
            {
                if (ActiveStream != null)
                    _maxFrequency = ActiveStream.WaveFormat.SampleRate / 2.0d;
                else
                    _maxFrequency = 22050; // Assume a default 44.1 kHz sample rate.
            }

            return (int)((frequency / _maxFrequency) * (mcfftDataSize / 2));
        }

        #endregion

        #region  - IWaveformPlayer -

        public TimeSpan SelectionBegin
        {
            get { return mRepeatStart; }
            set
            {
                if (!mInRepeatSet)
                {
                    mInRepeatSet = true;
                    TimeSpan _oldValue = mRepeatStart;
                    mRepeatStart = value;

                    if (_oldValue != mRepeatStart)
                        NotifyPropertyChanged("SelectionBegin");
                    mInRepeatSet = false;
                }
            }
        }

        public TimeSpan SelectionEnd
        {
            get { return mRepeatStop; }
            set
            {
                if (!mInChannelSet)
                {
                    mInRepeatSet = true;
                    TimeSpan oldValue = mRepeatStop;
                    mRepeatStop = value;

                    if (oldValue != mRepeatStop)
                        NotifyPropertyChanged("SelectionEnd");
                    mInRepeatSet = false;
                }
            }
        }        


        public double ChannelLength
        {
            get { return mChannelLength; }
            protected set
            {
                double _oldValue = mChannelLength;
                mChannelLength = value;

                if (_oldValue != mChannelLength)
                    NotifyPropertyChanged("ChannelLength");
            }
        }

        public double ChannelPosition
        {
            get { return mChannelPosition; }
            set
            {
                if (!mInChannelSet)
                {
                    mInChannelSet = true; // Avoid recursion
                    double _oldValue = mChannelPosition;
                    double position = Math.Max(0, Math.Min(value, ChannelLength));
                    if (!mInChannelTimerUpdate && ActiveStream != null)
                        ActiveStream.Position = (long)((position / ActiveStream.TotalTime.TotalSeconds) * ActiveStream.Length);
                    mChannelPosition = position;
                    if (_oldValue != mChannelPosition)
                        NotifyPropertyChanged("ChannelPosition");
                    mInChannelSet = false;
                }
            }
        }

        #endregion

        #region - INotifyPropertyChanged -

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        #endregion

        #region - Event Handlers -

        private void _inputStream_Sample(object sender, SampleEventArgs e)
        {
            mSampleAggregator.Add(e.Left, e.Right);
            long _repeatStartPosition = (long)((SelectionBegin.TotalSeconds / ActiveStream.TotalTime.TotalSeconds) * ActiveStream.Length);
            long _repeatStopPosition = (long)((SelectionEnd.TotalSeconds / ActiveStream.TotalTime.TotalSeconds) * ActiveStream.Length);
            if (((SelectionEnd - SelectionBegin) >= TimeSpan.FromMilliseconds(mcRepeatThreshold)) && ActiveStream.Position >= _repeatStopPosition)
            {
                mSampleAggregator.Clear();
                ActiveStream.Position = _repeatStartPosition;
            }
        }

        void _positionTimer_Tick(object sender, EventArgs e)
        {
            if (!IsRecording)
            {
                mInChannelTimerUpdate = true;
                ChannelPosition = ((double)ActiveStream.Position / (double)ActiveStream.Length) * ActiveStream.TotalTime.TotalSeconds;
                mInChannelTimerUpdate = false;

                if (ChannelPosition == ActiveStream.TotalTime.TotalSeconds)
                {
                    Stop();
                }
            }
        }

        private void _waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            mWaveFileWriter.Write(e.Buffer, 0, e.BytesRecorded);

            byte[] buffer = e.Buffer;
            int bytesRecorded = e.BytesRecorded;
            int bufferIncrement = mWaveInDevice.WaveFormat.BlockAlign;

            for (int index = 0; index < bytesRecorded; index += bufferIncrement)
            {
                float sample32 = BitConverter.ToSingle(buffer, index);
                mSampleAggregator.Add(sample32, 0.0f);
            }

            mWaveFileWriter.Flush();
        }

        private void _waveIn_RecordingStopped(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        #endregion

        #region - private functions -

        private void _stopAndCloseStream()
        {
            if (mWaveOutDevice != null)
            {
                mWaveOutDevice.Stop();
            }
            if (mActiveStream != null)
            {
                mInputStream.Close();
                mInputStream = null;
                ActiveStream.Close();
                ActiveStream = null;
            }
            if (mWaveOutDevice != null)
            {
                mWaveOutDevice.Dispose();
                mWaveOutDevice = null;
            }
        }        

        #endregion

        #region - Public Methods -

        /// <summary>
        /// Starts recording sound
        /// </summary>
        public void StartRecording(string path)
        {
            mWaveInDevice = new WaveIn();
            mWaveInDevice.WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100,2);
            mWaveInDevice.DataAvailable += _waveIn_DataAvailable;

            mSampleAggregator = new SampleAggregator(mcfftDataSize);
            mWaveFileWriter = new WaveFileWriter(path, mWaveInDevice.WaveFormat);

            mWaveInDevice.StartRecording();
            IsRecording = true;    
        }


        /// <summary>
        /// Stops recording sound
        /// </summary>
        public void StopRecording()
        {
            mWaveInDevice.Dispose();
            mWaveInDevice = null;
            mWaveFileWriter.Close();
            mWaveFileWriter.Dispose();
            mWaveFileWriter = null;
            IsRecording = false;
        }

        /// <summary>
        /// Stops playing of .mp3 file
        /// </summary>
        public void Stop()
        {
            if (mWaveOutDevice != null)
            {
                mWaveOutDevice.Stop();
            }
            IsPlaying = false;
            CanStop = false;
            CanPlay = true;
            CanPause = false;
        }

        /// <summary>
        /// Pauses playing of .mp3 file
        /// </summary>
        public void Pause()
        {
            if (IsPlaying && CanPause)
            {
                mWaveOutDevice.Pause();
                IsPlaying = false;
                CanPlay = true;
                CanPause = false;
            }
        }

        /// <summary>
        /// Starts playing of .mp3 file
        /// </summary>
        public void Play()
        {
            if (CanPlay)
            {
                try
                {
                    mWaveOutDevice.Play();
                    IsPlaying = true;
                    CanPause = true;
                    CanPlay = false;
                    CanStop = true;
                }
                catch
                {
                    IsPlaying = false;
                    CanPlay = true;
                    CanPause = false;
                    CanStop = false;
                }
            }
        }

        /// <summary>
        /// Opens .mp3 file and prepares for playing 
        /// </summary>
        /// <param name="path"></param>
        public void OpenFile(string path)
        {
            Stop();

            if (ActiveStream != null)
            {
                SelectionBegin = TimeSpan.Zero;
                SelectionEnd = TimeSpan.Zero;
                ChannelPosition = 0;
            }
            
            _stopAndCloseStream();            

            if (System.IO.File.Exists(path))
            {
                try
                {
                    mWaveOutDevice = new WaveOut()
                    {
                        DesiredLatency = 100
                    };
                    ActiveStream = new WaveFileReader(path); 
                    mInputStream = new WaveChannel32(ActiveStream);
                    mSampleAggregator = new SampleAggregator(mcfftDataSize);
                    mInputStream.Sample += _inputStream_Sample;
                    mWaveOutDevice.Init(mInputStream);
                    ChannelLength = mInputStream.TotalTime.TotalSeconds;
                    CanPlay = true;
                }
                catch(Exception ex)
                {
                    mcLogger.Error("OpenFile " + ex.Message);
                    ActiveStream = null;
                    CanPlay = false;
                }
            }
        }
        #endregion

        #region - Properties -

        /// <summary>
        /// Stream of loaded .mp3 file
        /// </summary>
        public WaveStream ActiveStream
        {
            get { return mActiveStream; }
            protected set
            {
                WaveStream _oldValue = mActiveStream;
                mActiveStream = value;
                if (_oldValue != mActiveStream)
                    NotifyPropertyChanged("ActiveStream");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CanPlay
        {
            get { return mCanPlay; }
            protected set
            {
                bool _oldValue = mCanPlay;
                mCanPlay = value;
                if (_oldValue != mCanPlay)
                    NotifyPropertyChanged("CanPlay");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CanPause
        {
            get { return mCanPause; }
            protected set
            {
                bool _oldValue = mCanPause;
                mCanPause = value;
                if (_oldValue != mCanPause)
                    NotifyPropertyChanged("CanPause");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CanStop
        {
            get { return mCanStop; }
            protected set
            {
                bool _oldValue = mCanStop;
                mCanStop = value;
                if (_oldValue != mCanStop)
                    NotifyPropertyChanged("CanStop");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsPlaying
        {
            get { return mIsPlaying; }
            protected set
            {
                bool _oldValue = mIsPlaying;
                mIsPlaying = value;
                if (_oldValue != mIsPlaying)
                    NotifyPropertyChanged("IsPlaying");
                mcPositionTimer.IsEnabled = value;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public bool IsRecording
        {
            get { return msIsRecording; }
            set
            {
                msIsRecording = value;
                NotifyPropertyChanged("IsRecording");
            }
        }
        #endregion
    }
}
