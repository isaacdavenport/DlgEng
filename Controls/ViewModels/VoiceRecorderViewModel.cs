using DialogEngine.Controls.Views;
using DialogEngine.Core;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Windows;
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
        private Polyline mPolyline;


        private double mCanvasH = 0;
        private double mCanvasW = 0;
        private double mPolylineH = 0;
        private double mPolylineW = 0;
        private int mTime = 0;
        private double mSeconds = 0;



        private List<byte> mTotalbytes;
        private Queue<Point> mDisplaypts;
        private Queue<Int32> mDisplaysht;


        private long mCount = 0;
        private int mNumtodisplay = 2205;

        private bool mIsRecording;
        //sample 1/100, display for 5 seconds

        #endregion

        #region - contructor -

        public VoiceRecorderViewModel(VoiceRecoreder view)
        {
            this.mView = view;

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
            //StopRecording = new RelayCommand(x => _stopRecording());
        }

        private void _startRecording()
        {
            mWavein = new WaveIn();
            mWavein.DataAvailable += new EventHandler<WaveInEventArgs>(_wavein_DataAvailable);
            //mWavein.RecordingStopped += new EventHandler(mWavein_RecordingStopped);
            mWavein.WaveFormat = new WaveFormat(44100, 32, 2);


            mWaveFileWriter = new WaveFileWriter(@"C:\Users\sbstb\Desktop\Output\temp.mp3", mWavein.WaveFormat);


            mCanvasH = mView.waveCanvas.ActualHeight;
            mCanvasW = mView.waveCanvas.ActualWidth;


           mPolyline = new Polyline();
           mPolyline.Stroke = Brushes.Blue;
           mPolyline.Name = "waveform";
           mPolyline.StrokeThickness = 1;
           mPolyline.MaxHeight = mCanvasH - 4;
           mPolyline.MaxWidth = mCanvasW - 4;


            mPolylineH = mPolyline.MaxHeight;
            mPolylineW = mPolyline.MaxWidth;


            //this.mTime = time;


            mDisplaypts = new Queue<Point>();
            mTotalbytes = new List<byte>();
            //displaysht = new Queue<short>();
            mDisplaysht = new Queue<Int32>();


            mWavein.StartRecording();
        }


        void _wavein_RecordingStopped(object sender, EventArgs e)
        {
            mWavein.Dispose();
            mWavein = null;
            mWaveFileWriter.Close();
            mWaveFileWriter.Dispose();


            mWaveFileWriter = null;
        }


        void _wavein_DataAvailable(object sender, WaveInEventArgs e)
        {
            //mSeconds += (double)(1.0 * e.BytesRecorded / mWavein.WaveFormat.AverageBytesPerSecond * 1.0);
            //if (mSeconds > mTime)
            //{
            //    mWavein.StopRecording();
            //}


            mWaveFileWriter.Write(e.Buffer, 0, e.BytesRecorded);
            mTotalbytes.AddRange(e.Buffer);


            //byte[] shts = new byte[2];
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
            this.mView.waveCanvas.Children.Clear();
            mPolyline.Points.Clear();

            //short[] shts2 = displaysht.ToArray();
            Int32[] shts2 = mDisplaysht.ToArray();
            for (Int32 x = 0; x < shts2.Length; ++x)
            {
                mPolyline.Points.Add(_normalize(x, shts2[x]));
            }



            this.mView.waveCanvas.Children.Add(mPolyline);


        }


        Point _normalize(Int32 x, Int32 y)
        {
            Point p = new Point();


            p.X = 1.0 * x / mNumtodisplay * mPolylineW;

            p.Y = mPolylineH / 2.0 - y / (Int32.MaxValue * 1.0) * (mPolylineH / 2.0);
            return p;
        }

        #endregion










    }
}
