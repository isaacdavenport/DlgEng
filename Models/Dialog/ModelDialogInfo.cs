//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;
using DialogEngine.Models.Enums;
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

        private List<ModelDialog> mInList = new List<ModelDialog>();
        private int mSelectedModelDialogIndex = -1;
        private ModelDialogState mState;
        public string FileName { set; get; }

        #endregion

        #region - properties -

        /// <summary>
        /// List of dialog models in dialog .json file
        /// </summary>
        public List<ModelDialog> InList
        {
            get
            {
                return mInList;
            }
            set
            {
                mInList = value;
            }
        }

        /// <summary>
        /// Index of selected dialog model from dialog .json file
        /// </summary>
        public int SelectedModelDialogIndex
        {
            get
            {
                return mSelectedModelDialogIndex;
            }
            set
            {
                mSelectedModelDialogIndex = value;
            }
        }


        /// <summary>
        /// Represents state of dialog .json file
        /// States are [On, Off]
        /// Default state is On
        /// On - we want to load dialog models from dialog .json file
        /// Off - ignore dilogs from dialog .json file
        /// </summary>
        public ModelDialogState State
        {
            set
            {
                mState = value;
                EventAggregator.Instance.GetEvent<ChangedModelDialogStateEvent>().Publish();
            }
            get
            {
                return mState;
            }

        }

        #endregion
    }
}
