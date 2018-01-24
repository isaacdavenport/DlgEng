//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;
using DialogEngine.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

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

        // default State is On
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
