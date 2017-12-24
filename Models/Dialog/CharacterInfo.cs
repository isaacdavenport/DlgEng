//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using DialogEngine.Models.Enums;
using System.ComponentModel;
using DialogEngine.ViewModels.Dialog;
using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;

namespace DialogEngine.Models.Dialog
{
    /// <summary>
    /// This is subset of <see cref="Character" />
    /// It is used to store basic information about characters, which should be faster then parsing all json files 
    /// </summary>
    public class CharacterInfo 
    {
        private CharacterState _state;

        [JsonProperty("CharacterName")]
        public string CharacterName { get;  set; }

        [JsonProperty("CharacterPrefix")]
        public string CharacterPrefix { get;  set; }

        public  string FileName { get; set; }

        public CharacterState State
        {
            set
            {
                // max allowed characters in On state is 2
                if( DialogViewModel.SelectedCharactersOn == 2 ){

                    if(value != CharacterState.On)
                    {
                        _state = value;

                        EventAggregator.Instance.GetEvent<ChangedCharactersStateEvent>().Publish();

                    }
                }
                else
                {
                    _state = value;

                    EventAggregator.Instance.GetEvent<ChangedCharactersStateEvent>().Publish();
                }


            }

            get
            {
                return _state;

            }
        }


    }
}
