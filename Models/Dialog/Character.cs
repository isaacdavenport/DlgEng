﻿//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using DialogEngine.Models.Enums;
using DialogEngine.ViewModels.Dialog;
using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;

namespace DialogEngine.Models.Dialog
{
    /// <summary>
    /// Used for deserialization of json files with created characters.
    /// </summary>
    public class Character
    {


        public const int RecentPhrasesQueueSize = 8;

        /// <summary>
        /// A character's Phrases list holds all the phrases they might say along with 
        /// heuristic phraseWeights on what parts of a model dialog they might use them in.
        /// </summary>
        /// 
                
        // max allowed characters in On state is 2
        private const int mcMaxAllowedCharactersOn = 2;

        private CharacterState mState;

        // assigned radio number - unassigned value is -1
        private int mRadioNum = -1;


        [JsonProperty("Phrases")]
        public List<PhraseEntry> Phrases = new List<PhraseEntry>(); //entry now has string phraseweight tags.

        [JsonProperty("PhraseTotals")]
        public PhraseEntry PhraseTotals = new PhraseEntry();

        public Queue<PhraseEntry> RecentPhrases = new Queue<PhraseEntry>()
            ; //TODO make this a method that runs over the history

        [JsonProperty("CharacterName")]
        public string CharacterName { get; protected set; }

        [JsonProperty("CharacterPrefix")]
        public string CharacterPrefix { get; protected set; }

        [JsonIgnore]
        public int RadioNum
        {
            set
            {
                mRadioNum = value;
            }
            get
            {
                return mRadioNum;
            }
        }

        [JsonIgnore]
        public CharacterState State
        {
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

            get
            {
                return mState;
            }
        }

    }
}
