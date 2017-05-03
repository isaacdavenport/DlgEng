using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.IO.Ports;
using DialogEngine;
//using Moq;

namespace SerialTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        //[TestMethod]
        //public void Test_InitSerial()
        //{
        //    var mock = new Mock<SerialPort>();

        //    // Don't need to set up any return values for the serial port calls that we expect

        //    // Hand mock.Object as a collaborator and exercise it, 
        //    // like calling methods on it...
        //    SerialPort port = mock.Object;

        //    SerialComs.InitSerial(port);

        //    // Verify that the given method was indeed called with the expected value at most once
        //    mock.Verify(p => p.Open(), Times.Once());
        //    mock.Verify(p => p.DiscardInBuffer(), Times.Once());
        //}

        [TestMethod]
        public void Test_ParseMessage()
        {
            int[] rssiRow = new int[SerialComs.NUM_RADIOS + 1];

            // Too short
            Assert.AreEqual(-1, ParseMessage.Parse("", rssiRow));
            // Doesn't contain FF and A5
            Assert.AreEqual(-1, ParseMessage.Parse("000000000087003b00100000", rssiRow));
            // Also too short - doesn't have 19 characters because we're not including
            // the \n that we would get when reading from the serial port
            Assert.AreEqual(-1, ParseMessage.Parse("ffff0000000000a500", rssiRow));


            // Just right - has the newline from the serial port
            Assert.AreEqual(2, ParseMessage.Parse("ffaf00ff000000a502\n", rssiRow));
            Assert.AreEqual(2, ParseMessage.Parse("ffc100ff00b500a50e\n", rssiRow));
            Assert.AreEqual(4, ParseMessage.Parse("ffbb00bb00ff00a501\n", rssiRow));

        }
    }
}
