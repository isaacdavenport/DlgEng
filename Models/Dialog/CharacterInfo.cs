//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using DialogEngine.Models.Enums;

namespace DialogEngine.Models.Dialog
{
    /// <summary>
    /// This is subset of <see cref="Character" />
    /// It is used to store basic information about characters, which should be faster then parsing all json files 
    /// </summary>
    public class CharacterInfo
    {
        [JsonProperty("CharacterName")]
        public string CharacterName { get;  set; }

        [JsonProperty("CharacterPrefix")]
        public string CharacterPrefix { get;  set; }

        public  string FileName { get; set; }

        public CharacterState State { set; get; } // default State is On

    }
}
