using DialogueDisputeGameMultiplayer.Offline;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DisputeCommon;
using System.Collections.Generic;

namespace OfflineServerTest
{
    
    
    /// <summary>
    ///This is a test class for OfflineServerManagerTest and is intended
    ///to contain all OfflineServerManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OfflineServerManagerTest
    {


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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for FeedbackWriter
        ///</summary>
        [TestMethod()]
        public void FeedbackWriterTest()
        {
            OfflineServerManager_Accessor target = new OfflineServerManager_Accessor(); // TODO: Initialize to an appropriate value
            IFeedbackWriter expected = null; // TODO: Initialize to an appropriate value
            IFeedbackWriter actual;
            target.FeedbackWriter = expected;
            actual = target.FeedbackWriter;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Matches
        ///</summary>
        [TestMethod()]
        public void MatchesTest()
        {
            OfflineServerManager_Accessor target = new OfflineServerManager_Accessor(); // TODO: Initialize to an appropriate value
            List<Match> actual;
            actual = target.Matches;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for View
        ///</summary>
        [TestMethod()]
        public void ViewTest()
        {
            OfflineServerManager_Accessor target = new OfflineServerManager_Accessor(); // TODO: Initialize to an appropriate value
            IServerView expected = null; // TODO: Initialize to an appropriate value
            IServerView actual;
            target.View = expected;
            actual = target.View;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
