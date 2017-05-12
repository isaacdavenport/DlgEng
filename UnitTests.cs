using Microsoft.VisualStudio.TestTools.UnitTesting;
using DialogEngine;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class TestDialogTracker
    {
        [TestMethod]
        public void TestintakeCharacters()
        {
            DialogTracker instanceForTests = new DialogTracker();
            instanceForTests.intakeCharacters();
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
            instanceForTests.intakeCharacters();
            //characters are swapped?
            
        }

        [TestMethod]
        public void TestRemovePhrasesOverParentalRating()
        {
            DialogTracker instanceForTests = new DialogTracker();
            instanceForTests.intakeCharacters();
            // all phrases are now below the threshold.
            // no under-threshold phrases were removed.

        }

        [TestMethod]
        public void TestPlayAudio()
        {
            DialogTracker instanceForTests = new DialogTracker();
            instanceForTests.intakeCharacters();
            //make sure audio plays
            //chars are changed (?)
            //audio does not overlap.

        }
    }
}
