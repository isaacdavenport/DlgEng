using System;
using System.Runtime.Remoting;
using Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DialogEngine;
using System.Threading;
using System.Threading.Tasks;


namespace TestEngine
{
    [TestClass]
    public class TestMp3
    {
        [TestMethod]
        public void TestIsPlaying() {
            DialogEngine.WindowsMediaPlayerMp3 player = new DialogEngine.WindowsMediaPlayerMp3();
            Assert.IsFalse(player.IsPlaying());
            player.PlayMp3(@"C:\Isaac\Toys2Life\DlgEng\DialogAudio\CM_YouWillRespectMy.mp3");
            Thread.Sleep(500);
            Assert.IsTrue(player.IsPlaying());
            Thread.Sleep(8500);
            Assert.IsFalse(player.IsPlaying());
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