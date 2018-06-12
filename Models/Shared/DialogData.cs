
using DialogEngine.Models.Dialog;
using DialogEngine.Models.Logger;
using DialogEngine.Models.Wizard;
using DialogEngine.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DialogEngine.Models.Shared
{
    public class DialogData : INotifyPropertyChanged
    {
        #region - fields -

        private static DialogData msInstance;
        private int[,] mHeatMapUpdate = new int[SerialSelectionService.NumRadios, SerialSelectionService.NumRadios];

        private ObservableCollection<Character> mCharacterCollection = new ObservableCollection<Character>();
        private ObservableCollection<ModelDialogInfo> mDialogModelCollection;
        private ObservableCollection<InfoMessage> mInfoMessagesCollection = new ObservableCollection<InfoMessage>();
        private ObservableCollection<WarningMessage> mWarningMessagesCollection = new ObservableCollection<WarningMessage>();
        private ObservableCollection<ErrorMessage> mErrorMessagesCollection = new ObservableCollection<ErrorMessage>();
        private ObservableCollection<WizardType> mWizardTypesCollection;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region - singleton -

        public static DialogData Instance
        {
            get
            {
                if (msInstance == null)
                {
                    msInstance = new DialogData();
                }
                return msInstance;
            }
        }

        #endregion

        #region - constructor -

        public DialogData()
        {

        }

        #endregion

        #region - INotifyPropertyChanged -


        private void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        #endregion

        #region - properties -

        public ObservableCollection<Character> CharacterCollection
        {
            get { return mCharacterCollection; }
            set
            {
                mCharacterCollection = value;
                NotifyPropertyChanged("CharacterCollection");
            }
        }

        public ObservableCollection<ModelDialogInfo> DialogModelCollection
        {
            get { return mDialogModelCollection; }
            set
            {
                mDialogModelCollection = value;
                NotifyPropertyChanged("DialogModelCollection");
            }
        }


        public ObservableCollection<InfoMessage> InfoMessagesCollection
        {
            get { return mInfoMessagesCollection; }
            set
            {
                mInfoMessagesCollection = value;
                NotifyPropertyChanged("InfoMessagesCollection");
            }
        }


        public ObservableCollection<WarningMessage> WarningMessagesCollection
        {
            get { return mWarningMessagesCollection; }
            set
            {
                mWarningMessagesCollection = value;
                NotifyPropertyChanged("WarningMessagesCollection");
            }
        }


        public ObservableCollection<ErrorMessage> ErrorMessagesCollection
        {
            get { return mErrorMessagesCollection; }
            set
            {
                mErrorMessagesCollection = value;
                NotifyPropertyChanged("ErrorMessagesCollection");
            }
        }


        public ObservableCollection<WizardType> WizardTypesCollection
        {
            get { return mWizardTypesCollection ; }
            set
            {
                mWizardTypesCollection = value;
                NotifyPropertyChanged("WizardTypesCollection");
            }
        }


        public int[,] HeatMapUpdate
        {
            get { return mHeatMapUpdate; }
            set
            {
                mHeatMapUpdate = value;
                NotifyPropertyChanged("HeatMapUpdate");
            }
        }

        #endregion
    }
}
