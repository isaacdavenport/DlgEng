using Microsoft.VisualStudio.TestTools.UnitTesting;
using DialogEngine;
using System.Collections.Generic;
using DialogEngine.Helpers;
using DialogEngine.Models.Dialog;

namespace UnitTests
{
    [TestClass]
    public class TestDialogTracker
    {
        [TestMethod]
        public void TestintakeCharacters()
        {
            DialogTracker instanceForTests = new DialogTracker();
            instanceForTests.IntakeCharacters();
            Assert.IsTrue(instanceForTests.CharacterList.Count > 1);

            //charlist is of objects type chararcter
            foreach(char type in instanceForTests.CharacterList.ToString())
            {
                Assert.IsTrue(type.ToString() == "Character");
            }

            //characters in charlist have not null values.
            //volume of content per character.
            List<string> seen = new List<string>();
            foreach (Character element in instanceForTests.CharacterList)
            {
                //test that character is only in charlist once.
                Assert.IsFalse(seen.Contains(element.CharacterPrefix));
                seen.Add(element.CharacterPrefix);
                //check valid entries for character atributes.
                Assert.IsNotNull(element.CharacterName);
                Assert.IsNotNull(element.CharacterPrefix);
                Assert.IsNotNull(element.Phrases);
                //check volume of dialogs? not great. Improve this.
                Assert.IsTrue(element.Phrases.Count > 2);
                Assert.IsNotNull(element.PhraseTotals);
                Assert.IsNotNull(element.RecentPhrases);
            }

        }

        [TestMethod]
        public void TestSwapChars1and2()
        {
            DialogTracker instanceForTests = new DialogTracker();
            instanceForTests.IntakeCharacters();
            //initial state. Vals are not equal
            Assert.IsTrue(instanceForTests.Character1Num != instanceForTests.Character2Num);
            int Char1Num = instanceForTests.Character1Num;
            int Char2Num = instanceForTests.Character2Num;
            //try swap
            instanceForTests.SwapCharactersOneAndTwo();
            //verify vals were swapped and are still not equal.
            Assert.IsTrue(instanceForTests.Character1Num != instanceForTests.Character2Num);
            Assert.IsTrue(instanceForTests.Character1Num == Char2Num);
            Assert.IsTrue(instanceForTests.Character2Num == Char1Num);
        }

        [TestMethod]
        public void TestRemovePhrasesOverParentalRating()
        {
            DialogTracker instanceForTests = new DialogTracker();
            instanceForTests.IntakeCharacters();
            //check rating of all dialogs.
            // all phrases are now below the threshold.
            Dictionary<string, int> _ratings = new Dictionary<string, int> { { "G", 1 }, { "PG", 2 }, { "PG13", 3 }, { "R", 4 }, { "1", 1 }, { "2", 2 }, { "3", 3 }, { "4", 4 } };  //future proof if notation changes
            foreach (Character _character in instanceForTests.CharacterList)
            {
                foreach(PhraseEntry _dialog in _character.Phrases)
                {
                    Assert.IsTrue(_ratings[_dialog.PhraseRating] <= _ratings[SessionVars.CurrentParentalRating]);
                }
            }
        }

        [TestMethod]
        public void TestPlayAudio()
        {
            DialogTracker instanceForTests = new DialogTracker();
            instanceForTests.IntakeCharacters();
            //make sure audio plays
            //chars are changed (?)
            //audio does not overlap.

        }
    }
}
