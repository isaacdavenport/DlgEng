using Microsoft.VisualStudio.TestTools.UnitTesting;
using DialogEngine;
using System.Collections.Generic;
using DialogEngine.Helpers;
using DialogEngine.Models.Dialog;
using DialogEngine.ViewModels.Dialog;

namespace UnitTests
{
    [TestClass]
    public class TestDialogTracker
    {
        [TestMethod]
        public async void TestintakeCharacters()
        {
            await DialogTracker.GetInstance(DialogViewModel.Instance).IntakeCharactersAsync();
            Assert.IsTrue(DialogViewModel.Instance.CharacterCollection.Count > 1);

            //charlist is of objects type chararcter
            foreach(char _type in DialogViewModel.Instance.CharacterCollection.ToString())
            {
                Assert.IsTrue(_type.ToString() == "Character");
            }

            //characters in charlist have not null values.
            //volume of content per character.
            List<string> _seen = new List<string>();
            foreach (Character _element in DialogViewModel.Instance.CharacterCollection)
            {
                //test that character is only in charlist once.
                Assert.IsFalse(_seen.Contains(_element.CharacterPrefix));
                _seen.Add(_element.CharacterPrefix);
                //check valid entries for character atributes.
                Assert.IsNotNull(_element.CharacterName);
                Assert.IsNotNull(_element.CharacterPrefix);
                Assert.IsNotNull(_element.Phrases);
                //check volume of dialogs? not great. Improve this.
                Assert.IsTrue(_element.Phrases.Count > 2);
                Assert.IsNotNull(_element.PhraseTotals);
                Assert.IsNotNull(_element.RecentPhrases);
            }

        }

        [TestMethod]
        public async void TestSwapChars1And2()
        {
            DialogTracker _instanceForTests = DialogTracker.Instance;
            await _instanceForTests.IntakeCharactersAsync();
            //initial state. Vals are not equal
            Assert.IsTrue(_instanceForTests.Character1Num != _instanceForTests.Character2Num);
            int _char1Num = _instanceForTests.Character1Num;
            int _char2Num = _instanceForTests.Character2Num;
            //try swap
            _instanceForTests.SwapCharactersOneAndTwo();
            //verify vals were swapped and are still not equal.
            Assert.IsTrue(_instanceForTests.Character1Num != _instanceForTests.Character2Num);
            Assert.IsTrue(_instanceForTests.Character1Num == _char2Num);
            Assert.IsTrue(_instanceForTests.Character2Num == _char1Num);
        }

        [TestMethod]
        public async void TestRemovePhrasesOverParentalRating()
        {
            DialogTracker _instanceForTests = DialogTracker.Instance;
            await _instanceForTests.IntakeCharactersAsync();
            //check rating of all dialogs.
            // all phrases are now below the threshold.
            Dictionary<string, int> _ratings = new Dictionary<string, int> { { "G", 1 }, { "PG", 2 }, { "PG13", 3 }, { "R", 4 }, { "1", 1 }, { "2", 2 }, { "3", 3 }, { "4", 4 } };  //future proof if notation changes
            foreach (Character _character in DialogViewModel.Instance.CharacterCollection)
            {
                foreach(PhraseEntry _dialog in _character.Phrases)
                {
                    Assert.IsTrue(_ratings[_dialog.PhraseRating] <= _ratings[SessionVariables.CurrentParentalRating]);
                }
            }
        }

        [TestMethod]
        public async void TestPlayAudio()
        {
            DialogTracker _instanceForTests = DialogTracker.Instance;
            await _instanceForTests.IntakeCharactersAsync();
            //make sure audio plays
            //chars are changed (?)
            //audio does not overlap.

        }
    }
}
