//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;
using DialogEngine.Models.Enums;
using System;
using System.Collections.Generic;
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
        private ModelDialogState mState;

        public string FileName { set; get; }

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
    }
}
