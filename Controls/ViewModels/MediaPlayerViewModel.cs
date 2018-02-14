

using System;
using DialogEngine.Controls.Views;
using DialogEngine.Core;

namespace DialogEngine.Controls.ViewModels
{
    public class MediaPlayerViewModel : ViewModelBase
    {
        #region - fields -

        private MediaPlayer mView;

        #endregion

        #region - constructor -

        public MediaPlayerViewModel(MediaPlayer view)
        {
            this.mView = view;
        }

        #endregion

        #region - properties -

        #endregion

        #region - commands-

        public RelayCommand StartVideo { get; set; }
        public RelayCommand StopVideo  { get; set; }

        #endregion

        #region - private functions -

        private void _bindCommands()
        {
            StartVideo = new RelayCommand(x => _startVideo());
            StopVideo = new RelayCommand(x => _stopVideo());
        }

        private void _stopVideo()
        {
            mView.VideoPlayer.Stop();
        }

        private void _startVideo()
        {
            mView.VideoPlayer.Play();
        }

        #endregion

        #region - public functions -

        #endregion
    }
}
