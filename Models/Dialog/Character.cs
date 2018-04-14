

using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;
using DialogEngine.Models.Enums;
using DialogEngine.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace DialogEngine.Models.Dialog
{
    public class Character : INotifyPropertyChanged
    {
        #region - fields -

        private const int mcMaxAllowedCharactersOn = 2;             // max allowed characters in On state is 2
        private CharacterState mState;
        private int mCharacterAge = 10;
        private string mCharacterGender = "M";
        private string mCharacterName="";
        private string mCharacterPrefix="";
        private int mRadioNum = -1;                                 // assigned radio number - unassigned value is -1
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region - public functions -

        /// <summary>
        /// Notifies of property changed.
        /// </summary>
        /// <param name="_propertyName">Name of changed property.</param>
        public virtual void OnPropertyChanged(string _propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }

        #endregion

        #region - properties -
        public List<PhraseEntry> Phrases = new List<PhraseEntry>();
        public PhraseEntry PhraseTotals = new PhraseEntry();

        [JsonProperty("CharacterAge")]
        public int CharacterAge
        {
            get { return mCharacterAge; }
            set
            {
                mCharacterAge = value;
                OnPropertyChanged("CharacterAge");
            }
        }

        [JsonProperty("CharacterGender")]
        public string CharacterGender
        {
            get { return mCharacterGender; }
            set
            {
                mCharacterGender = value;
                OnPropertyChanged("CharacterGender");
            }
        }


        [JsonProperty("CharacterName")]
        public string CharacterName
        {
            get { return mCharacterName; }
            set
            {
                mCharacterName = value;
                OnPropertyChanged("CharacterName");
            }
        }


        [JsonProperty("CharacterPrefix")]
        public string CharacterPrefix
        {
            get { return mCharacterPrefix; }
            set
            {
                mCharacterPrefix = value;
                OnPropertyChanged("CharacterPrefix");
            }
        }

        // json ignore properties

        [JsonIgnore]
        public Queue<PhraseEntry> RecentPhrases = new Queue<PhraseEntry>();

        [JsonIgnore]
        public const int RecentPhrasesQueueSize = 8;

        /// <summary>
        /// Radio number assigned to character
        /// Default value is unassigned ( -1 )
        /// </summary>
        [JsonIgnore]
        public int RadioNum
        {
            get { return mRadioNum; }
            set { mRadioNum = value; }
        }


        /// <summary>
        /// Represents state of character
        /// Default state is Available
        /// States are [Avaialble,On,Off]
        /// Available - character can be random selected
        /// On - character is forced in selection
        /// Off - character can't be selected
        /// </summary>
        [JsonIgnore]
        public CharacterState State
        {
            get { return mState; }
            set
            {
                if (DialogViewModel.SelectedCharactersOn == mcMaxAllowedCharactersOn)
                {
                    if (value != CharacterState.On)
                    {
                        mState = value;
                        EventAggregator.Instance.GetEvent<ChangedCharactersStateEvent>().Publish();
                    }
                }
                else
                {
                    mState = value;
                    EventAggregator.Instance.GetEvent<ChangedCharactersStateEvent>().Publish();
                }
            }
        }

        #endregion

    }
}
