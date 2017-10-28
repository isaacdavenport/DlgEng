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
            DialogEngine.WindowsMediaPlayerMp3 _player = new DialogEngine.WindowsMediaPlayerMp3();
            Assert.IsFalse(_player.IsPlaying());
            _player.PlayMp3(@"C:\Isaac\Toys2Life\DlgEng\DialogAudio\CM_YouWillRespectMy.mp3");
            Thread.Sleep(500);
            Assert.IsTrue(_player.IsPlaying());
            Thread.Sleep(8500);
            Assert.IsFalse(_player.IsPlaying());
        }
    }
}