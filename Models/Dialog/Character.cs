

using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;
using DialogEngine.Models.Enums;
using DialogEngine.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DialogEngine.Models.Dialog
{
    public class Character
    {
        #region - fields -

        private const int mcMaxAllowedCharactersOn = 2;             // max allowed characters in On state is 2
        private CharacterState mState;
        private int mRadioNum = -1;                                 // assigned radio number - unassigned value is -1

        #endregion

        #region - properties -

        public int CharacterAge { get; set; }
        public string CharacterGender { get; set; }
        public string CharacterName { get; set; }
        public string CharacterPrefix { get; set; }
        public List<PhraseEntry> Phrases = new List<PhraseEntry>();
        public PhraseEntry PhraseTotals = new PhraseEntry();

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
