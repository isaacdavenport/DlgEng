//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;
using DialogEngine.Models.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace DialogEngine.Models.Dialog
{
    /// <summary>
    /// This is subset of <see cref="ModelDialog" />
    /// It is used to store basic information about dialog models, which should be faster then parsing all json files 
    /// </summary>
    public class ModelDialogInfo 
    {
        #region - fields -

        private int mSelectedModelDialogIndex = -1;
        private ModelDialogState mState;

        #endregion

        #region - properties -

        [JsonProperty("ArrayOfDialogModels")]
        public List<ModelDialog> ArrayOfDialogModels { get; set; }

        [JsonProperty("ModelsCollectionName")]
        public string ModelsCollectionName { set; get; }

        /// <summary>
        /// Index of selected dialog model from dialog .json file
        /// </summary>
        [JsonIgnore]
        public int SelectedModelDialogIndex
        {
            get { return mSelectedModelDialogIndex; }
            set { mSelectedModelDialogIndex = value; }
        }

        /// <summary>
        /// Represents state of dialog .json file
        /// States are [On, Off]
        /// Default state is On
        /// On - we want to load dialog models from dialog .json file
        /// Off - ignore dilogs from dialog .json file
        /// </summary>
        [JsonIgnore]
        public ModelDialogState State
        {
            get { return mState; }
            set
            {
                mState = value;
                EventAggregator.Instance.GetEvent<ChangedModelDialogStateEvent>().Publish();
            }
        }

        [JsonIgnore]
        public string FileName { get; set; }

        [JsonIgnore]
        public int JsonArrayIndex { get; set; }
        
        #endregion

    }
}
