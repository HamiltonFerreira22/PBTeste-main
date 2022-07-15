using log4net;
using PBTeste.Model.Request;
using PBTeste.Model.Response;
using RestSharp;

namespace PBTeste.Service
{
    public class ExampleService : BaseService
    {
        public ExampleService(ILog logger, IRestClient client) : base(logger, client)
        {
        }

        public ExampleResponse ExamplePOST(ExampleRequest exampleRequest)
        {
            var response = ExecutarRequisicao<ExampleResponse>("/example/example", RestSharp.Method.POST, exampleRequest);
            return response;
        }

        public ExampleResponse ExampleGET(ExampleRequest exampleRequest)
        {
            var response = ExecutarRequisicao<ExampleResponse>("/example/example", RestSharp.Method.GET, exampleRequest);
            return response;
        }
    }
}
