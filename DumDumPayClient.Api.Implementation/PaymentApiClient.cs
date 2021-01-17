using System;
using System.Linq;
using DumDumPayClient.Api.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DumDumPayClient.Api.Contracts;
using DumDumPayClient.Api.Contracts.Exceptions;
using DumDumPayClient.Api.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DumDumPayClient.Api
{
    /// <summary>
    /// In a real life I would use code generation tools like nswag to generate this code 
    /// </summary>
    public class PaymentApiClient : IPaymentApiClient
    {
        private readonly ILogger<PaymentApiClient> _logger;
        private readonly PaymentApiClientSettings _settings;

        public PaymentApiClient(IOptions<PaymentApiClientSettings> settings, ILogger<PaymentApiClient> logger)
        {
            _logger = logger;
            _settings = settings.Value;
        }

        public async Task<Contracts.Models.CreatePaymentResult> CreatePaymentAsync(
            Contracts.Models.CreatePaymentRequest request)
        {
            var apiRequest = request.ToApiRequest();
            var response =
                await PostAsync<CreatePaymentRequest, CreatePaymentResponse>(apiRequest,
                    $"{_settings.ApiHost}/api/payment/create");
            if (response.Errors != null && response.Errors.Any())
            {
                var contractErrors = response.Errors.Select(i => i.ToContract()).ToList();
                throw new ApiException(contractErrors, "Payment Api returned an error");
            }

            return response.Result.ToContract();
        }

        public async Task<Contracts.Models.ConfirmPaymentResult> ConfirmPaymentAsync(
            Contracts.Models.ConfirmPaymentRequest request)
        {
            var apiRequest = request.ToApiRequest();
            var response =
                await PostAsync<ConfirmPaymentRequest, ConfirmPaymentResponse>(apiRequest,
                    $"{_settings.ApiHost}/api/payment/confirm");

            if (response.Errors != null && response.Errors.Any())
            {
                var contractErrors = response.Errors.Select(i => i.ToContract()).ToList();
                throw new ApiException(contractErrors, "Payment Api returned an error");
            }

            return response.Result.ToContract();
        }

        private async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request, string url)
        {
            var client = new HttpClient();

            var content = CreateContent(request);

            // serialize request object later
            _logger.LogInformation("POSTing request to {url} with {request}", url, request);
            
            var response = await client.PostAsync(url, content);

            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TResponse>(responseBody);
            }

            throw new Exception("Error calling Payment Api: " + responseBody);
        }

        private StringContent CreateContent<T>(T requestObject)
        {
            var requestBody = JsonConvert.SerializeObject(requestObject);
            var stringContent = new StringContent(requestBody, Encoding.UTF8, "application/json");

            stringContent.Headers.Add("Mechant-Id", _settings.MechantId);
            stringContent.Headers.Add("Secret-Key", _settings.SecretKey);

            return stringContent;
        }
    }
}