//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Controls.Views;
using DialogEngine.Controls.VoiceRecorder;
using DialogEngine.Core;
using DialogEngine.Helpers;
using System.ComponentModel;
using System.IO;

namespace DialogEngine.Controls.ViewModels
{
    /// <summary>
    /// Implementation of <see cref="ViewModelBase" />
    /// DataContext for VoiceRecorder.xaml/> 
    /// </summary>
    public class VoiceRecorderViewModel : ViewModelBase
    {
        #region - fields -

        private VoiceRecoreder mView;
        private NAudioEngine mSoundPlayer;
        private double mChannelPosition;
        private bool mIsRecording;
        private bool mIsPlaying;

        #endregion

        #region - contructor -

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="view">Instance of <see cref="VoiceRecoreder"/></param>
        /// <param name="player">Instance of <see cref="ISoundPlayer"/></param>
        public VoiceRecorderViewModel(VoiceRecoreder view,NAudioEngine player)
        {
            this.mView = view;
            this.mSoundPlayer = player;
            this.mSoundPlayer.PropertyChanged += _soundPlayer_PropertyChanged;

            _bindCommands();
            mView.spectrumAnalyzer.RegisterSoundPlayer(player);
        }

        #endregion

        #region - commands -

        /// <summary>
        /// Starts or stops recording
        /// </summary>
        public RelayCommand StartRecording { get; set; }

        /// <summary>
        /// Starts or stops playing
        /// </summary>
        public RelayCommand PlayContent { get; set; }

        #endregion

        #region - event handlers-

        private void _soundPlayer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "ChannelPosition":
                    ChannelPosition = (double)mSoundPlayer.ActiveStream.Position / (double)mSoundPlayer.ActiveStream.Length;
                    break;
                case "IsPlaying":
                    if (!mSoundPlayer.IsPlaying)
                    {
                        if (mSoundPlayer.ChannelPosition == mSoundPlayer.ChannelLength)
                            ChannelPosition = 0;

                        IsPlaying = false;
                        mView.PlayingBtn.Content = mView.FindResource("PlayBtn");
                    }
                    break;
                case "IsRecording":
                    IsRecording = mSoundPlayer.IsRecording;
                    break;
            }
        }

        #endregion

        #region - private functions -

        private void _bindCommands()
        {
            StartRecording = new RelayCommand(x => _startRecording());
            PlayContent = new RelayCommand(x => _play());
        }

        private void _play()
        {
            if (IsPlaying)
            {
                IsPlaying = false;
                mView.PlayingBtn.Content = mView.FindResource("PlayBtn");
                if(mSoundPlayer.CanPause)
                mSoundPlayer.Pause();
            }
            else
            {
                IsPlaying = true;
                mView.PlayingBtn.Content = mView.FindResource("PauseBtn");

                if(ChannelPosition == 0)
                mSoundPlayer.OpenFile(Path.Combine(SessionVariables.AudioDirectory,"BO_AllowedToDye.mp3"));

                mSoundPlayer.Play();
            }
        }

        private void _startRecording()
        {
            if (IsRecording)
            {
                IsRecording = false;
                mView.RecordingBtn.Content = mView.FindResource("Microphone");
                mSoundPlayer.StopRecording();
            }
            else
            {
                IsRecording = true;
                mView.RecordingBtn.Content = mView.FindResource("StopBtn");
                mSoundPlayer.StartRecording();
            }
        }

        #endregion

        #region - properties -

        /// <summary>
        /// Is player recording
        /// </summary>
        public bool IsRecording
        {
            get { return mIsRecording; }
            set
            {
                mIsRecording = value;
                OnPropertyChanged("IsRecording");
            }
        }

        /// <summary>
        /// Is player playing
        /// </summary>
        public bool IsPlaying
        {
            get { return mIsPlaying; }
            set
            {
                mIsPlaying = value;
                OnPropertyChanged("IsPlaying");
            }
        }

        /// <summary>
        /// Position of current stream
        /// </summary>
        public double ChannelPosition
        {
            get { return mChannelPosition; }
            set
            {
                mChannelPosition = value * 100;
                OnPropertyChanged("ChannelPosition");
            }
        }

        #endregion
    }
}
