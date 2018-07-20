
using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;
using DialogEngine.Models.Enums;
using DialogEngine.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DialogEngine.Models.Dialog
{
    public class Character : INotifyPropertyChanged,IEquatable<Character>
    {
        #region - fields -

        // max allowed characters for dialog is 2
        private const int mcMaxAllowedCharactersOn = 2;             
        private CharacterState mState;
        private int mCharacterAge = 10;
        private string mCharacterGender = "M";
        private string mCharacterName="";
        private string mCharacterPrefix="";
        private int mRadioNum = -1;                                // assigned radio number - unassigned value is -1
        public event PropertyChangedEventHandler PropertyChanged;
        private List<PhraseEntry> mPhrases;
        private PhraseEntry mPhraseTotals;

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

        [JsonIgnore]
        public PhraseEntry PhraseTotals
        {
            get
            {
                if (mPhraseTotals == null)
                    mPhraseTotals = new PhraseEntry();

                return mPhraseTotals;
            }

            set
            {
                mPhraseTotals = value;
            }
        }


        [Required]
        [JsonProperty("CharacterAge")]
        public int CharacterAge
        {
            get { return  mCharacterAge; }
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

        [Required]
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

        [JsonProperty("Phrases")]
        public List<PhraseEntry> Phrases
        {
            get
            {
                if (mPhrases == null)
                    mPhrases = new List<PhraseEntry>();

                return mPhrases;
            }

            set
            {
                mPhrases = value;
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
            set
            {
                mRadioNum = value;
                OnPropertyChanged("RadioNum");
            }
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

                OnPropertyChanged("State");
            }
        }

        [JsonIgnore]
        public string FileName { get; set; }

        [JsonIgnore]
        public int JsonArrayIndex { get; set; }

        #endregion


        public bool Equals(Character other)
        {
            return other.CharacterPrefix.Equals(this.CharacterPrefix);
        }
    }
}
