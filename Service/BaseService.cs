using log4net;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;

namespace PBTeste.Service
{
    public abstract class BaseService
    {
        protected readonly ILog logger;

        protected readonly IRestClient client;

        protected IRestRequest EndPoint { get; private set; }
        public IRestResponse Response { get; private set; }

        public BaseService(ILog logger, IRestClient client)
        {
            this.logger = logger ?? throw new ArgumentException("Logger não deve ser nulo."); ;
            this.client = client ?? throw new ArgumentException("RestClient não deve ser nulo.");
        }

        // Template Method
        protected T ExecutarRequisicao<T>(string endPoint, Method method, object payload) where T : class
        {
            try
            {
                Endpoint(endPoint);
                Method(method);
                Body(payload);

                var res = ExecutarRequisicao<T>();
                ExibirResposta();

                return res;
            }
            catch (Exception e)
            {
                logger.Error($"Ops... ocorreu o seguinte erro: {e.Message} > {e.StackTrace}");
                throw;
            }
        }

        protected void Endpoint(string rota)
        {
            logger.Info($"Configurando EndPoint: {rota}");
            EndPoint = new RestRequest(rota);
        }

        protected void Header(string chave, string valor)
        {
            logger.Info($"Adicionando Cabeçalho: {chave}:{valor}");
            EndPoint.AddHeader(chave, valor);
        }

        protected void Method(Method method, DataFormat dataFormat = DataFormat.Json)
        {
            logger.Info($"Configurando tipo de Requisição {method} e formato dos dados {dataFormat}");
            EndPoint.Method = method;
            EndPoint.RequestFormat = dataFormat;
        }
        protected void Body(object body)
        {
            logger.Info($"Configurando corpo da requisição");
            EndPoint.RequestFormat = DataFormat.Json;
            EndPoint.AddJsonBody(body);
        }

        protected void Get() => Method(RestSharp.Method.GET);
        protected void Post() => Method(RestSharp.Method.POST);
        protected void Put() => Method(RestSharp.Method.PUT);
        protected void Delete() => Method(RestSharp.Method.DELETE);


        // Primitive Operation
        protected virtual T ExecutarRequisicao<T>() where T : class
        {
            logger.Info($"Executando requisição...");
            var res = client.Execute<T>(EndPoint);
            Response = res;

            return res.Data;
        }

        protected void ExibirResposta()
        {
            JObject resposta = JObject.Parse(Response.Content);
            Console.WriteLine(resposta);
        }
    }
}
