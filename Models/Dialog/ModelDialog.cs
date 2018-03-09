//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DialogEngine.Models.Dialog
{
    /// <summary>
    /// ModelDialog is a sequence of phrase  that represent an exchange between characters 
    /// the model dialog will be filled with randomly selected character phrases of the appropriate phrase type
    /// </summary>
    public class ModelDialog
    {
        private string mFileName;

        [JsonProperty("AddedOnDateTime")]
        public DateTime AddedOnDateTime = new DateTime(2016, 1, 2, 3, 4, 5);

        [JsonProperty("Adventure")]
        public string Adventure = "";

        [JsonProperty("DialogName")]
        private string mName;

        [JsonProperty("PhraseTypeSequence")]
        public List<string> PhraseTypeSequence = new List<string>();

        [JsonProperty("Popularity")]
        public double Popularity = 1.0;

        [JsonProperty("Provides")]
        public List<string> Provides = new List<string>();

        [JsonProperty("Requires")]
        public List<string> Requires = new List<string>();


        /// <summary>
        /// Name of model dialog
        /// </summary>
        public string Name 
        {
            get
            {
                return mName;
            }
            set
            {
                mName = value;
            }
        }

        /// <summary>
        /// Json file name where is located model dialog
        /// </summary>
        public string FileName
        {
            get
            {
                return mFileName;
            }
            set
            {
                mFileName = value;
            }
        }


        public bool AreDialogsRequirementsMet()
        {
            return true;
        }
    }
}
