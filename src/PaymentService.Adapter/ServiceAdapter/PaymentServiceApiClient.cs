using Common.ServiceFabric.HttpExtensions;
using CorrelationId.Middleware;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace PaymentService.ClientAdapter.ServiceAdapter.PaymentApiClient
{
    public class API
    {
        public static string ControllerName => $"Payment";
        public static string ProcessPayment => $"/api/{ControllerName}/processpayment";
        public static string Refund => $"/api/{ControllerName}/refund";
    }

    public interface IPaymentServiceApiClient
    {
        Task<HttpOperationResponse<string>> ProcessPayment(JObject jsonData, CancellationToken cancellationToken = default(CancellationToken));
        Task<HttpOperationResponse<string>> Refund(JObject jsonData, CancellationToken cancellationToken = default(CancellationToken));
    }
    public class PaymentServiceApiClient : BasePaymentServiceApiClient, IPaymentServiceApiClient
    {
        public PaymentServiceApiClient(string baseUrl, HttpClient httpClient, ICorrelationContextAccessor correlationContext)
             : base(baseUrl, httpClient, correlationContext)
        {
        }
        public async Task<HttpOperationResponse<string>> ProcessPayment(JObject jsonData, CancellationToken cancellationToken = default(CancellationToken))
        {
            var serviceUrl = $"{BaseUrl.TrimEnd('/')}{API.ProcessPayment.TrimEnd('/')}";
            return await PostAsync(
                serviceUrl,
                new StringContent(
                    JsonConvert.SerializeObject(
                        jsonData,
                        new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        }),
                    Encoding.UTF8,
                    Constant.APP_JSON),
                cancellationToken);
        }

        public async Task<HttpOperationResponse<string>> Refund(JObject jsonData, CancellationToken cancellationToken = default(CancellationToken))
        {
            var serviceUrl = $"{BaseUrl.TrimEnd('/')}{API.Refund.TrimEnd('/')}";
            return await PostAsync(
                serviceUrl,
                new StringContent(
                    JsonConvert.SerializeObject(
                        jsonData,
                        new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        }),
                    Encoding.UTF8,
                    Constant.APP_JSON),
                cancellationToken);
        }
    }
}

