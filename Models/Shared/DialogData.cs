
using DialogEngine.Models.Dialog;
using DialogEngine.Models.Logger;
using DialogEngine.Models.Wizard;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DialogEngine.Models.Shared
{
    public class DialogData : INotifyPropertyChanged
    {
        #region - fields -

        private static DialogData msInstance;
        private bool mIsDialogDataLoaded;

        private ObservableCollection<Character> mCharacterCollection;
        private ObservableCollection<ModelDialogInfo> mDialogModelCollection;
        private ObservableCollection<InfoMessage> mInfoMessagesCollection;
        private ObservableCollection<WarningMessage> mWarningMessagesCollection;
        private ObservableCollection<ErrorMessage> mErrorMessagesCollection;
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

        #endregion
    }
}
