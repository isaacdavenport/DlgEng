using System;
using System.Runtime.Remoting;
using Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEngine
{
    [TestClass]
    public class TestPlayer
    {
        [TestMethod]
        public void TestHitPointsAreSubtracted()
        {
            Player player = new Player(10);
            player.ReceiveDamage(3);

            Assert.AreEqual(7, player.CurrentHitPoints);
        }

        [TestMethod]
        public void TestNegativeDamageDoesNotChangeCurrentHitPoints()
        {
            Player player = new Player(10);
            player.ReceiveDamage(-3);

            Assert.AreEqual(10, player.CurrentHitPoints);
        }

        [TestMethod]
        public void TestMoreDamageThanCurrentHitPointsEqualsZero()
        {
            Player player = new Player(10);
            player.ReceiveDamage(13);

            Assert.AreEqual(0, player.CurrentHitPoints);
        }


    }
}