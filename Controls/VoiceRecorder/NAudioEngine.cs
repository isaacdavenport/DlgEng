//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Threading;
using NAudio.Wave;
using System.Windows;

namespace DialogEngine.Controls.VoiceRecorder
{
    public class NAudioEngine : INotifyPropertyChanged, ISpectrumPlayer, IDisposable
    {
        #region - Fields -
        private const int mcWaveformCompressedPointCount = 2000;
        private const int mcRepeatThreshold = 200;
        private readonly DispatcherTimer mcPositionTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle);
        private readonly BackgroundWorker mcWaveformGenerateWorker = new BackgroundWorker();
        private readonly int mcfftDataSize = (int)FFTDataSize.FFT2048;

        private static NAudioEngine msInstance;

        private bool mDisposed;
        private bool mCanPlay;
        private bool mCanPause;
        private bool mCanStop;
        private bool mIsPlaying;
        private bool mInChannelTimerUpdate;
        private double mChannelLength;
        private double mChannelPosition;
        private bool mInChannelSet;
        private WaveIn mWaveInDevice;
        private WaveFileWriter mWaveFileWriter;
        private WaveOut mWaveOutDevice;
        private WaveStream mActiveStream;
        private WaveChannel32 mInputStream;
        private SampleAggregator mSampleAggregator;
        private SampleAggregator mWaveformAggregator;
        private string mPendingWaveformPath;        
        private float[] mFullLevelData;
        private float[] mWaveformData;
        private TagLib.File mFileTag;
        private TimeSpan mRepeatStart;
        private TimeSpan mRepeatStop;
        private bool mInRepeatSet;

        #endregion

        #region  - Singleton -

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

        private NAudioEngine()
        {
            mcPositionTimer.Interval = TimeSpan.FromMilliseconds(50);
            mcPositionTimer.Tick += positionTimer_Tick;

            mcWaveformGenerateWorker.DoWork += waveformGenerateWorker_DoWork;
            mcWaveformGenerateWorker.RunWorkerCompleted += waveformGenerateWorker_RunWorkerCompleted;
            mcWaveformGenerateWorker.WorkerSupportsCancellation = true;
        }
        
        #endregion

        #region - IDisposable -

        public void Dispose()
        {
            Dispose(true);            
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!mDisposed)
            {
                if(disposing)
                {
                    StopAndCloseStream();
                }

                mDisposed = true;
            }
        }

        #endregion

        #region - ISpectrumPlayer -

        public bool GetFFTData(float[] fftDataBuffer)
        {
            mSampleAggregator.GetFFTResults(fftDataBuffer);
            return mIsPlaying;
        }

        public int GetFFTFrequencyIndex(int frequency)
        {
            double _maxFrequency;
            if (ActiveStream != null)
                _maxFrequency = ActiveStream.WaveFormat.SampleRate / 2.0d;
            else
                _maxFrequency = 22050; // Assume a default 44.1 kHz sample rate.
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

        public float[] WaveformData
        {
            get { return mWaveformData; }
            protected set
            {
                float[] _oldValue = mWaveformData;
                mWaveformData = value;

                if (_oldValue != mWaveformData)
                    NotifyPropertyChanged("WaveformData");
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

        #region - Waveform Generation -

        private class WaveformGenerationParams
        {
            public WaveformGenerationParams(int points, string path)
            {
                Points = points;
                Path = path;
            }

            public int Points { get; protected set; }
            public string Path { get; protected set; }
        }

        private void GenerateWaveformData(string path)
        {
            if (mcWaveformGenerateWorker.IsBusy)
            {
                mPendingWaveformPath = path;
                mcWaveformGenerateWorker.CancelAsync();
                return;
            }

            if (!mcWaveformGenerateWorker.IsBusy && mcWaveformCompressedPointCount != 0)
                mcWaveformGenerateWorker.RunWorkerAsync(new WaveformGenerationParams(mcWaveformCompressedPointCount, path));
        }

        private void waveformGenerateWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                if (!mcWaveformGenerateWorker.IsBusy && mcWaveformCompressedPointCount != 0)
                    mcWaveformGenerateWorker.RunWorkerAsync(new WaveformGenerationParams(mcWaveformCompressedPointCount, mPendingWaveformPath));
            }
        }        

        private void waveformGenerateWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            WaveformGenerationParams _waveformParams = e.Argument as WaveformGenerationParams;
            Mp3FileReader _waveformMp3Stream = new Mp3FileReader(_waveformParams.Path);
            WaveChannel32 _waveformInputStream = new WaveChannel32(_waveformMp3Stream);            
            _waveformInputStream.Sample += waveStream_Sample;
            
            int _frameLength = mcfftDataSize;
            int _frameCount = (int)((double)_waveformInputStream.Length / (double)_frameLength);
            int _waveformLength = _frameCount * 2;
            byte[] readBuffer = new byte[_frameLength];
            mWaveformAggregator = new SampleAggregator(_frameLength);
   
            float _maxLeftPointLevel = float.MinValue;
            float _maxRightPointLevel = float.MinValue;
            int _currentPointIndex = 0;
            float[] _waveformCompressedPoints = new float[_waveformParams.Points];
            List<float> _waveformData = new List<float>();
            List<int> _waveMaxPointIndexes = new List<int>();
            
            for (int i = 1; i <= _waveformParams.Points; i++)
            {
                _waveMaxPointIndexes.Add((int)Math.Round(_waveformLength * ((double)i / (double)_waveformParams.Points), 0));
            }
            int readCount = 0;
            while (_currentPointIndex * 2 < _waveformParams.Points)
            {
                _waveformInputStream.Read(readBuffer, 0, readBuffer.Length);

                _waveformData.Add(mWaveformAggregator.LeftMaxVolume);
                _waveformData.Add(mWaveformAggregator.RightMaxVolume);

                if (mWaveformAggregator.LeftMaxVolume > _maxLeftPointLevel)
                    _maxLeftPointLevel = mWaveformAggregator.LeftMaxVolume;
                if (mWaveformAggregator.RightMaxVolume > _maxRightPointLevel)
                    _maxRightPointLevel = mWaveformAggregator.RightMaxVolume;

                if (readCount > _waveMaxPointIndexes[_currentPointIndex])
                {
                    _waveformCompressedPoints[(_currentPointIndex * 2)] = _maxLeftPointLevel;
                    _waveformCompressedPoints[(_currentPointIndex * 2) + 1] = _maxRightPointLevel;
                    _maxLeftPointLevel = float.MinValue;
                    _maxRightPointLevel = float.MinValue;
                    _currentPointIndex++;
                }
                if (readCount % 3000 == 0)
                {
                    float[] clonedData = (float[])_waveformCompressedPoints.Clone();
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        WaveformData = clonedData;
                    }));
                }

                if (mcWaveformGenerateWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                readCount++;
            }

            float[] finalClonedData = (float[])_waveformCompressedPoints.Clone();
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                mFullLevelData = _waveformData.ToArray();
                WaveformData = finalClonedData;
            }));
            _waveformInputStream.Close();
            _waveformInputStream.Dispose();
            _waveformInputStream = null;
            _waveformMp3Stream.Close();
            _waveformMp3Stream.Dispose();
            _waveformMp3Stream = null;
        }

        #endregion

        #region - Event Handlers -

        private void inputStream_Sample(object sender, SampleEventArgs e)
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

        void waveStream_Sample(object sender, SampleEventArgs e)
        {
            mWaveformAggregator.Add(e.Left, e.Right);
        }

        void positionTimer_Tick(object sender, EventArgs e)
        {
            mInChannelTimerUpdate = true;
            ChannelPosition = ((double)ActiveStream.Position / (double)ActiveStream.Length) * ActiveStream.TotalTime.TotalSeconds;
            mInChannelTimerUpdate = false;

            if(ChannelPosition == ActiveStream.TotalTime.TotalSeconds)
            {
                Stop();
            }
        }

        #endregion

        #region - Private Utility Methods -

        private void StopAndCloseStream()
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

        public void StartRecording()
        {
            mWaveInDevice = new WaveIn();
            mWaveInDevice.DataAvailable += new EventHandler<WaveInEventArgs>(_waveIn_DataAvailable);
            //mWaveInDevice.RecordingStopped += new EventHandler(_waveIn_RecordingStopped);
            mWaveInDevice.WaveFormat = new WaveFormat(44100, 32, 2);

            mWaveFileWriter = new WaveFileWriter(@"C:\Users\sbstb\Desktop\Output\temp.mp3", mWaveInDevice.WaveFormat);
        }

        public void StopRecording()
        {
            mWaveInDevice.Dispose();
            mWaveInDevice = null;
            mWaveFileWriter.Close();
            mWaveFileWriter.Dispose();
            mWaveFileWriter = null;
        }

        private void _waveIn_RecordingStopped(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            throw new NotImplementedException();
        }

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

        public void Play()
        {
            if (CanPlay)
            {
                mWaveOutDevice.Play();
                IsPlaying = true;
                CanPause = true;
                CanPlay = false;
                CanStop = true;
            }
        }

        public void OpenFile(string path)
        {
            Stop();

            if (ActiveStream != null)
            {
                SelectionBegin = TimeSpan.Zero;
                SelectionEnd = TimeSpan.Zero;
                ChannelPosition = 0;
            }
            
            StopAndCloseStream();            

            if (System.IO.File.Exists(path))
            {
                try
                {
                    mWaveOutDevice = new WaveOut()
                    {
                        DesiredLatency = 100
                    };
                    ActiveStream = new Mp3FileReader(path);
                    mInputStream = new WaveChannel32(ActiveStream);
                    mSampleAggregator = new SampleAggregator(mcfftDataSize);
                    mInputStream.Sample += inputStream_Sample;
                    mWaveOutDevice.Init(mInputStream);
                    ChannelLength = mInputStream.TotalTime.TotalSeconds;
                    FileTag = TagLib.File.Create(path);
                    GenerateWaveformData(path);
                    CanPlay = true;
                }
                catch
                {
                    ActiveStream = null;
                    CanPlay = false;
                }
            }
        }
        #endregion

        #region - Properties -
        public TagLib.File FileTag
        {
            get { return mFileTag; }
            set
            {
                TagLib.File _oldValue = mFileTag;
                mFileTag = value;
                if (_oldValue != mFileTag)
                    NotifyPropertyChanged("FileTag");
            }
        }

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
        #endregion

    }
}
