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
    public class VoiceRecorderControlViewModel : ViewModelBase
    {
        #region - fields -

        private NAudioEngine mSoundPlayer;
        private double mChannelPosition;
        private bool mIsRecording;
        private bool mIsPlaying;
        private bool mIsLineRecorded;
        private bool mIsPlayingLineInContext;
        private string mCurrentFilePath;


        #endregion

        #region - contructor -

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="player">Instance of <see cref="ISoundPlayer"/></param>
        public VoiceRecorderControlViewModel(NAudioEngine player)
        {
            this.SoundPlayer = player;
            this.mSoundPlayer.PropertyChanged += _soundPlayer_PropertyChanged;


            _bindCommands();
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
                    }
                    break;
                //case "IsRecording":
                //    IsRecording = mSoundPlayer.IsRecording;
                //    break;
            }
        }

        #endregion

        #region - private functions -

        private void _bindCommands()
        {
            StartRecording = new RelayCommand(x => _startOrStopRecording());
            PlayContent = new RelayCommand(x => PlayOrStop(mCurrentFilePath));
        }

 
        private void _startOrStopRecording()
        {
            if (IsRecording)
            {
                mSoundPlayer.StopRecording();
                IsRecording = false;
            }
            else
            {
                mSoundPlayer.StartRecording(CurrentFilePath);
                IsRecording = true;
            }
        }

        #endregion

        #region - public functions -


        public void ResetData()
        {
            IsLineRecorded = false;
        }


        public void PlayOrStop(string path)
        {
            if (IsPlaying)
            {
                IsPlaying = false;
                if (mSoundPlayer.CanStop)
                    mSoundPlayer.Stop();
            }
            else
            {
                IsPlaying = true;
                if (ChannelPosition == 0)
                    mSoundPlayer.OpenFile(path);

                mSoundPlayer.Play();
            }
        }

        #endregion

        #region - properties -

        public NAudioEngine SoundPlayer
        {
            get { return mSoundPlayer; }
            set
            {
                mSoundPlayer = value;
                OnPropertyChanged("SoundPlayer");
            }
        }

        /// <summary>
        /// Is player recording
        /// </summary>
        public bool IsRecording
        {
            get { return mIsRecording; }
            set
            {
                bool _oldValue = mIsRecording;
                if (_oldValue == value)
                    return;

                mIsRecording = value;
                if(mIsRecording == false)
                {
                    IsLineRecorded = true;
                }

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
                bool _oldValue = mIsPlaying;
                if (_oldValue == value)
                    return;

                mIsPlaying = value;
                OnPropertyChanged("IsPlaying");
            }
        }


        /// <summary>
        /// Is player playing line in context
        /// </summary>
        public bool IsPlayingLineInContext
        {
            get { return mIsPlayingLineInContext; }
            set
            {
                mIsPlayingLineInContext = value;
                OnPropertyChanged("IsPlayingLineInContext");
            }
        }


        public bool IsLineRecorded
        {
            get { return mIsLineRecorded; }
            set
            {
                mIsLineRecorded = value;
                OnPropertyChanged("IsLineRecorded");
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

        /// <summary>
        /// Path of last recorded .mp3 file
        /// </summary>
        public string CurrentFilePath
        {
            get { return mCurrentFilePath; }
            set
            {
                mCurrentFilePath = value;
                OnPropertyChanged("CurrentFilePath");
            }
        }

        #endregion
    }
}
