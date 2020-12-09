
using Common.AspNetMvc.Core.ServiceProxy;
using Common.ServiceFabric.Communication;
using Common.ServiceFabric.Communication.ServiceModel;
using Common.ServiceFabric.HttpExtensions;
using Microsoft.ServiceFabric.Services.Client;
using Newtonsoft.Json.Linq;
using PaymentService.ClientAdapter.ServiceAdapter.PaymentApiClient;
using System;
using System.Threading.Tasks;
namespace PaymentService.ClientAdapter.ServiceProxy
{
    public interface IPaymentServiceProxy
    {
        Task<ProxyResponse<T, ServiceErrorResponse>> ProcessPayment<T>(JObject jsonData);
        Task<ProxyResponse<T, ServiceErrorResponse>> Refund<T>(JObject jsonData);
    }
    public class PaymentServiceProxy : IPaymentServiceProxy
    {
        private readonly IPartitionClientFactory<CommunicationClient<IPaymentServiceApiClient>> partitionClientFactory;
        public PaymentServiceProxy(IPartitionClientFactory<CommunicationClient<IPaymentServiceApiClient>> partitionClientFactory)
        {
            this.partitionClientFactory = partitionClientFactory;
        }

        public async Task<HttpOperationResponse<string>> CreateApiClient(Func<IPaymentServiceApiClient, Task<HttpOperationResponse<string>>> invoke)
        {
            return await partitionClientFactory.CreatePartitionClient(new ServicePartitionKey(/*partitionKey*/)).InvokeWithRetryAsync(async client =>
            {
                var api = await client.CreateApiClient();
                return await invoke(api);
            });
        }

        public async Task<ProxyResponse<T, ServiceErrorResponse>> ProcessPayment<T>(JObject jsonData)
        {
            var httpOperationResponse = await CreateApiClient(async apiClient => await apiClient.ProcessPayment(jsonData));
            var result = new ProxyResponse<T, ServiceErrorResponse/*Api Standard error format*/>(httpOperationResponse);
            httpOperationResponse.Dispose();
            return result;
        }

        public async Task<ProxyResponse<T, ServiceErrorResponse>> Refund<T>(JObject jsonData)
        {
            var httpOperationResponse = await CreateApiClient(async apiClient => await apiClient.Refund(jsonData));
            var result = new ProxyResponse<T, ServiceErrorResponse/*Api Standard error format*/>(httpOperationResponse);
            httpOperationResponse.Dispose();
            return result;
        }
    }
}
