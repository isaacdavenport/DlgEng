using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DialogEngine.Models.Dialog
{
    public class DialogItem
    {

        #region - Fields -

        private Character mCharacter;

        private PhraseEntry mPhraseEntry;


        #endregion

        #region -Constructors-

        public DialogItem() { }

        public DialogItem(Character _character,PhraseEntry _phraseEntry)
        {
            this.mCharacter = _character;
            this.mPhraseEntry = _phraseEntry;
        }

        #endregion

        #region -Properties-

        public Character Character
        {
            get
            {
                return this.mCharacter;
            }
            set
            {
                this.mCharacter = value;
            }
        }


        public PhraseEntry PhraseEntry
        {
            get
            {
                return this.mPhraseEntry;
            }

            set
            {
                this.mPhraseEntry = value;
            }
        }

        #endregion



    }
}
