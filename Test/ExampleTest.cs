using log4net;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using PBTeste.Model.Request;
using PBTeste.Model.Schema;
using PBTeste.Service;
using RestSharp;
using System.Collections.Generic;
using System.IO;

namespace PBTeste.Test
{
    [TestClass]
    public class ExampleTest
    {
        private static ILog logger;
        private static IRestClient client;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));

            logger = LogManager.GetLogger(typeof(ExampleTest));
            client = new RestClient("http://localhost:3000/api");
        }

        [TestMethod]
        public void TestExample()
        {
            ExampleService resposta = new ExampleService(logger, client);

            var response = resposta.ExamplePOST(ExamplePayload());

            Assert.AreEqual("example", response.example);
            Assert.AreEqual((int)resposta.Response.StatusCode, 201);
        }

        [TestMethod]
        public void TestContractExample()
        {
            ExampleService service = new ExampleService(logger, client);

            service.ExamplePOST(ExamplePayload());

            JObject json = JObject.Parse(service.Response.Content);
            Assert.IsTrue(json.IsValid(ExampleSchema.ExampleJson(), out IList<string> messages), string.Join(",", messages));
        }

        private ExampleRequest ExamplePayload() => new ExampleRequest
        {
            example = "example",
        };
    }
}
