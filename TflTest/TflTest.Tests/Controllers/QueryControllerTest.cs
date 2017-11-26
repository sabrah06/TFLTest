using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using TflTest.Controllers;
using System.Web.Http;
using MyModels.Models;
using System.Net.Http;
using System.Web.Http.Results;

namespace TflTest.Tests.Controllers
{
    /// <summary>
    /// Summary description for QueryControllerTest
    /// </summary>
    [TestClass]
    public class QueryControllerTest
    {
        public QueryControllerTest()
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

        [TestMethod]
        public void QueryValidResponseRoadTest()
        {
            string RoadId = "A2";
            var controller = new QueryController();
            // Act
            var response = controller.SearchResponse(RoadId);

            // Assert
            var contentResult = response as OkNegotiatedContentResult<ApiResponse>;
            // Assert the result  
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("A2", contentResult.Content.displayName);
        }

        [TestMethod]
        public void QueryInValidResponseRoadTest()
        {
            string RoadId = "A233";
            var controller = new QueryController();
            // Act
            IHttpActionResult actionResult = controller.SearchResponse(RoadId);
            // Assert  
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void QueryValidRoadTest()
        {
            string RoadId = "A2";;
            var controller = new QueryController();
            // Act
            var result = controller.Search(RoadId);
            // Assert the result  
            Assert.IsNotNull(result);
            Assert.AreEqual("A2", result.displayName);
        }

        [TestMethod]
        public void QueryInValidRoadTest()
        {
            string RoadId = "A233";
            var controller = new QueryController();
            // Act
            var result = controller.Search(RoadId);
            // Assert the result  
            Assert.IsNotNull(result);
            Assert.AreEqual("404", result.httpStatusCode);
        }
    }
}
