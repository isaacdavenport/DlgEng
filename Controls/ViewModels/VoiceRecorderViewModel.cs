using DialogEngine.Controls.Views;
using DialogEngine.Core;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace DialogEngine.Controls.ViewModels
{
    public class VoiceRecorderViewModel : ViewModelBase
    {
        #region - fields -

        private VoiceRecoreder mView;

        private WaveIn mWavein;
        private WaveFileWriter mWaveFileWriter;
        private DateTime mStartTime;
        private List<byte> mTotalbytes;
        private Queue<Point> mDisplaypts;
        private Queue<Int32> mDisplaysht;

        private long mCount = 0;

        //sample 1/100, display for 5 seconds
        private int mNumtodisplay = 2205;

        private bool mIsRecording;
        private int mProgressBarValue;
        private string mTimerText;
        private Timer mTimer;

        #endregion

        #region - contructor -

        public VoiceRecorderViewModel(VoiceRecoreder view)
        {
            this.mView = view;
            mTimer = new Timer(_timerElapsed, null, Timeout.Infinite, Timeout.Infinite);

            _bindCommands();

        }

        #endregion

        #region - properties -

        public bool IsRecording
        {
            get
            {
                return mIsRecording;
            }
            set
            {
                mIsRecording = value;

                OnPropertyChanged("IsRecording");
            }
        }

        public int ProgressBarValue
        {
            get
            {
                return mProgressBarValue;
            }

            set
            {
                mProgressBarValue = value;

                OnPropertyChanged("ProgressBarValue");
            }
        }

        public string TimerText
        {
            get
            {
                return mTimerText;
            }

            set
            {
                mTimerText = value;

                OnPropertyChanged("TimerText");
            }
        }

        #endregion

        #region - commands -

        public RelayCommand StartRecording { get; set; }
        public RelayCommand StopRecording { get; set; }
        public RelayCommand PlayContent { get; set; }

        #endregion

        #region - private functions -

        private void _bindCommands()
        {
            StartRecording = new RelayCommand(x => _startRecording());
            StopRecording = new RelayCommand(x => _stopRecording());
            PlayContent = new RelayCommand(x => _playMP3());
        }

        private void _playMP3()
        {

            MP3Player.Instance.PlayMp3(@"C:\Users\sbstb\Desktop\Output\temp.mp3");
        }

        private void _timerElapsed(object state)
        {
            TimeSpan recordedTime = DateTime.Now - mStartTime;

            TimerText = recordedTime.ToString(@"hh\:mm\:ss");
        }

        private void _stopRecording()
        {
            IsRecording = false;
            mTimer.Change(Timeout.Infinite, Timeout.Infinite);
            TimerText = "";

            mWavein.Dispose();
            mWavein = null;
            mWaveFileWriter.Close();
            mWaveFileWriter.Dispose();
            mWaveFileWriter = null;
        }


        private void _startRecording()
        {
            mWavein = new WaveIn();
            mWavein.DataAvailable += new EventHandler<WaveInEventArgs>(_wavein_DataAvailable);
            mWavein.WaveFormat = new WaveFormat(44100, 32, 2);

            mWaveFileWriter = new WaveFileWriter(@"C:\Users\sbstb\Desktop\Output\temp.mp3", mWavein.WaveFormat);

            mDisplaypts = new Queue<Point>();
            mTotalbytes = new List<byte>();
            mDisplaysht = new Queue<Int32>();

            mWavein.StartRecording();

            IsRecording = true;
            mStartTime = DateTime.Now;
            mTimer.Change(1000, 1000);
        }



        private void _wavein_DataAvailable(object sender, WaveInEventArgs e)
        {
            mWaveFileWriter.Write(e.Buffer, 0, e.BytesRecorded);
            mTotalbytes.AddRange(e.Buffer);


            byte[] shts = new byte[4];


            for (int i = 0; i < e.BytesRecorded - 1; i += 100)
            {
                shts[0] = e.Buffer[i];
                shts[1] = e.Buffer[i + 1];
                shts[2] = e.Buffer[i + 2];
                shts[3] = e.Buffer[i + 3];
                if (mCount < mNumtodisplay)
                {
                    mDisplaysht.Enqueue(BitConverter.ToInt32(shts, 0));
                    ++mCount;
                }
                else
                {
                    mDisplaysht.Dequeue();

                    mDisplaysht.Enqueue(BitConverter.ToInt32(shts, 0));
                }
            }


            Int32[] shts2 = mDisplaysht.ToArray();
            for (Int32 x = 0; x < shts2.Length; ++x)
            {
                if(shts2[x] >=0)
                ProgressBarValue =(int) _normalizeWave(shts2[x]);
            }
        }


        private double _normalizeWave(Int32 strength)
        {
            double value = (strength *1.0 / Int32.MaxValue * 1.0) * 100.0;

            return value;
        }

        #endregion
    }
}
