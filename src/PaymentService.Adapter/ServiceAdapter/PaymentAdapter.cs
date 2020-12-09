
using Common.ServiceFabric.AspnetCore.Configuration;
using Common.ServiceFabric.Communication;
using Common.ServiceFabric.Communication.ServiceAdapter;
using Common.ServiceFabric.HttpExtensions;
using CorrelationId.Middleware;
using Microsoft.Extensions.Configuration;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Client;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace PaymentService.ClientAdapter.ServiceAdapter.PaymentApiClient
{
    #region ==========  ServiceApiClient Partial Class   =========
    public abstract class BasePaymentServiceApiClient
    {
        private HttpClient httpClient;
        private readonly ICorrelationContextAccessor correlationContext;
        private System.Lazy<JsonSerializerSettings> settings;
        public BasePaymentServiceApiClient(string baseUrl, HttpClient httpClient, ICorrelationContextAccessor correlationContext)
        {
            BaseUrl = baseUrl;
            this.httpClient = httpClient;
            this.correlationContext = correlationContext;
            settings = new Lazy<JsonSerializerSettings>(() =>
            {
                var settings = new JsonSerializerSettings();
                UpdateJsonSerializerSettings(settings);
                return settings;
            });
        }
        public string BaseUrl { get; set; }
        protected virtual void UpdateJsonSerializerSettings(JsonSerializerSettings settings) { }
        protected virtual void PrepareRequest(HttpClient client, HttpRequestMessage request, string url) { }
        protected virtual void PrepareRequest(HttpClient client, HttpRequestMessage request, StringBuilder urlBuilder) { }
        protected virtual void ProcessResponse(HttpClient client, HttpResponseMessage response) { }
        protected async Task<HttpOperationResponse<string>> GetAsync(string serviceUrl, CancellationToken cancellationToken)
        {
            // Create Result 
            HttpResponseMessage httpResponse = null;
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.Method = new HttpMethod("GET");
            httpRequest.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpRequest.Headers.Add(Constant.CORRELATIONT_HEADER, correlationContext.CorrelationContext.CorrelationId);
            PrepareRequest(httpClient, httpRequest, serviceUrl);
            httpRequest.RequestUri = new System.Uri(serviceUrl, UriKind.RelativeOrAbsolute);
            PrepareRequest(httpClient, httpRequest, serviceUrl);
            httpResponse = await httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
            var headers = Enumerable.ToDictionary(httpResponse.Headers, h => h.Key, h => h.Value);
            foreach (var item in httpResponse.Content.Headers) headers[item.Key] = item.Value;
            ProcessResponse(httpClient, httpResponse);
            var statusCode = (int)httpResponse.StatusCode;
            var responseContent = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            //Handle Response if not success
            if ((int)statusCode != 201 && (int)statusCode != 400 && (int)statusCode != 404)
            {
                if (statusCode == 410) // Service gone maybe service fabri was moved to anothers node!!!!
                {
                    // Throw this if need to retry
                    throw new ServiceGoneException("Service was gone", statusCode.ToString(), responseContent, headers, null);
                }
                //Dispose
                httpRequest.Dispose();
                if (httpResponse != null)
                {
                    httpResponse.Dispose();
                }
            }
            // Create Result
            var result = new HttpOperationResponse<string>();
            result.Request = httpRequest;
            result.Response = httpResponse;
            // Deserialize Response        
            try
            {
                result.Body = responseContent;
            }
            catch (JsonException ex)
            {
                httpRequest.Dispose();
                if (httpResponse != null)
                {
                    httpResponse.Dispose();
                }
                throw new SerializationException("Unable to deserialize the response.", ex);
            }
            return result;
        }
        protected async Task<HttpOperationResponse<string>> PostAsync(string serviceUrl, HttpContent content, CancellationToken cancellationToken)
        {
            // Create Result 
            HttpResponseMessage httpResponse = null;
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.Method = new HttpMethod("POST");
            httpRequest.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpRequest.Headers.Add(Constant.CORRELATIONT_HEADER, correlationContext.CorrelationContext.CorrelationId);
            PrepareRequest(httpClient, httpRequest, serviceUrl);
            httpRequest.RequestUri = new System.Uri(serviceUrl, UriKind.RelativeOrAbsolute);
            PrepareRequest(httpClient, httpRequest, serviceUrl);
            //Content 
            httpRequest.Content = content;
            httpResponse = await httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
            var headers = Enumerable.ToDictionary(httpResponse.Headers, h => h.Key, h => h.Value);
            foreach (var item in httpResponse.Content.Headers) headers[item.Key] = item.Value;
            ProcessResponse(httpClient, httpResponse);
            var statusCode = (int)httpResponse.StatusCode;
            var responseContent = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            //Handle Response if not success
            if ((int)statusCode != 201 && (int)statusCode != 400 && (int)statusCode != 404)
            {
                if (statusCode == 410) // Service gone maybe service fabri was moved to anothers node!!!!
                {
                    // Throw this if need to retry
                    throw new ServiceGoneException("Service was gone", statusCode.ToString(), responseContent, headers, null);
                }
                //Dispose
                httpRequest.Dispose();
                if (httpResponse != null)
                {
                    httpResponse.Dispose();
                }
            }
            // Create Result
            var result = new HttpOperationResponse<string>();
            result.Request = httpRequest;
            result.Response = httpResponse;
            // Deserialize Response        
            try
            {
                result.Body = responseContent;
            }
            catch (JsonException ex)
            {
                httpRequest.Dispose();
                if (httpResponse != null)
                {
                    httpResponse.Dispose();
                }
                throw new SerializationException("Unable to deserialize the response.", ex);
            }
            return result;
        }
        protected async Task<HttpOperationResponse<string>> PutAsync(string serviceUrl, string id, string json, CancellationToken cancellationToken)
        {
            // Create Result 
            HttpResponseMessage httpResponse = null;
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.Method = new HttpMethod("PUT");
            httpRequest.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpRequest.Headers.Add(Constant.CORRELATIONT_HEADER, correlationContext.CorrelationContext.CorrelationId);
            PrepareRequest(httpClient, httpRequest, serviceUrl);
            httpRequest.RequestUri = new System.Uri(serviceUrl, UriKind.RelativeOrAbsolute);
            PrepareRequest(httpClient, httpRequest, serviceUrl);
            //Content 
            httpRequest.Content = new StringContent(json, Encoding.UTF8, Constant.APP_JSON); ;
            httpResponse = await httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
            var headers = Enumerable.ToDictionary(httpResponse.Headers, h => h.Key, h => h.Value);
            foreach (var item in httpResponse.Content.Headers) headers[item.Key] = item.Value;
            ProcessResponse(httpClient, httpResponse);
            var statusCode = (int)httpResponse.StatusCode;
            var responseContent = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            //Handle Response if not success
            if ((int)statusCode != 201 && (int)statusCode != 400 && (int)statusCode != 404)
            {
                if (statusCode == 410) // Service gone maybe service fabri was moved to anothers node!!!!
                {
                    // Throw this if need to retry
                    throw new ServiceGoneException("Service was gone", statusCode.ToString(), responseContent, headers, null);
                }
                //Dispose
                httpRequest.Dispose();
                if (httpResponse != null)
                {
                    httpResponse.Dispose();
                }
            }
            // Create Result
            var result = new HttpOperationResponse<string>();
            result.Request = httpRequest;
            result.Response = httpResponse;
            // Deserialize Response        
            try
            {
                result.Body = responseContent;
            }
            catch (JsonException ex)
            {
                httpRequest.Dispose();
                if (httpResponse != null)
                {
                    httpResponse.Dispose();
                }
                throw new SerializationException("Unable to deserialize the response.", ex);
            }
            return result;
        }
        protected async Task<HttpOperationResponse<string>> DeleteAsync(string serviceUrl, CancellationToken cancellationToken)
        {
            // Create Result 
            HttpResponseMessage httpResponse = null;
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.Method = new HttpMethod("DELETE");
            httpRequest.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpRequest.Headers.Add(Constant.CORRELATIONT_HEADER, correlationContext.CorrelationContext.CorrelationId);
            PrepareRequest(httpClient, httpRequest, serviceUrl);
            httpRequest.RequestUri = new System.Uri(serviceUrl, UriKind.RelativeOrAbsolute);
            PrepareRequest(httpClient, httpRequest, serviceUrl);
            httpResponse = await httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
            var headers = Enumerable.ToDictionary(httpResponse.Headers, h => h.Key, h => h.Value);
            foreach (var item in httpResponse.Content.Headers) headers[item.Key] = item.Value;
            ProcessResponse(httpClient, httpResponse);
            var statusCode = (int)httpResponse.StatusCode;
            var responseContent = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            //Handle Response if not success
            if ((int)statusCode != 201 && (int)statusCode != 400 && (int)statusCode != 404)
            {
                if (statusCode == 410) // Service gone maybe service fabri was moved to anothers node!!!!
                {
                    // Throw this if need to retry
                    throw new ServiceGoneException("Service was gone", statusCode.ToString(), responseContent, headers, null);
                }
                //Dispose
                httpRequest.Dispose();
                if (httpResponse != null)
                {
                    httpResponse.Dispose();
                }
            }
            // Create Result
            var result = new HttpOperationResponse<string>();
            result.Request = httpRequest;
            result.Response = httpResponse;
            // Deserialize Response        
            try
            {
                result.Body = responseContent;
            }
            catch (JsonException ex)
            {
                httpRequest.Dispose();
                if (httpResponse != null)
                {
                    httpResponse.Dispose();
                }
                throw new SerializationException("Unable to deserialize the response.", ex);
            }
            return result;
        }

    }
    #endregion

    #region ==========  ApiOptions  =========
    public class ApiOptions : IPaymentApiOptions
    {
        public ApiOptions(IConfiguration config)
        {
            ServiceUri = config["PaymentService:ServiceUri"];
        }
        public string ServiceUri { get; internal set; }
    }
    public interface IPaymentApiOptions : IApiOption { }
    #endregion

    #region ========== CommunicationClientFactory ==========	
    public class PaymentServiceApiCommunicationClientFactory : ServiceApiCommunicationClientFactory<IPaymentServiceApiClient>
    {
        public PaymentServiceApiCommunicationClientFactory(IServicePartitionResolver resolver, IApiClientFactory<IPaymentServiceApiClient> apiClientFactory)
            : base(resolver, apiClientFactory)
        {
        }
    }

    #endregion

    #region ========== PartitionClientFactory ==========
    public class PaymentServiceApiPartitionClientFactory : ServiceApiPartitionClientFactory<IPaymentServiceApiClient>
    {
        public PaymentServiceApiPartitionClientFactory(ICommunicationClientFactory<CommunicationClient<IPaymentServiceApiClient>> factory, IPaymentApiOptions paymentApiOptions)
            : base(factory, paymentApiOptions.ServiceUri) { }
    }
    #endregion

    #region ========== ApiFactory ==========	
    public class PaymentServiceApiClientFactory : ServiceApiClientFactory<IPaymentServiceApiClient>
    {
        //##### Uncommnet if needed 
        private readonly ICorrelationContextAccessor correlationContext;

        public PaymentServiceApiClientFactory(HttpClient httpClient, IPaymentApiOptions paymentApiOptions, ICorrelationContextAccessor correlationContext)
            : base(httpClient, paymentApiOptions.ServiceUri)
        {
            this.correlationContext = correlationContext;
        }

        public override async Task<IPaymentServiceApiClient> CreateApiClientAsync(string baseUri)
        {
            return await Task.Run(() => new PaymentServiceApiClient(baseUri, httpClient, correlationContext));
        }
    }
    #endregion
}

