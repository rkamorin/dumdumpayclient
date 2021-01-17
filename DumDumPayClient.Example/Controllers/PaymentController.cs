using System;
using System.Threading.Tasks;
using DumDumPayClient.Api.Contracts;
using DumDumPayClient.Api.Contracts.Exceptions;
using DumDumPayClient.Api.Contracts.Models;
using DumDumPayClient.Example.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DumDumPayClient.Example.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentApiClient _paymentApiClient;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentApiClient paymentApiClient, ILogger<PaymentController> logger)
        {
            _paymentApiClient = paymentApiClient;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var request = new CreatePaymentModel
            {
                Amount = 123,
                Country = "CYP",
                Currency = "USD",
                Cvv = 123,
                CardExpiryDate = 1123,
                CardNumber = "4111111111111111",
                CardHolder = "TEST TESTER"
            };
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreatePaymentModel request)
        {
            var paymentRequest = new CreatePaymentRequest
            {
                OrderId = Guid.NewGuid(),
                Amount = request.Amount,
                Country = request.Country,
                Currency = request.Currency,
                CardHolder = request.CardHolder,
                CardNumber = request.CardNumber,
                CardExpiryDate = request.CardExpiryDate,
                Cvv = request.Cvv
            };

            try
            {
                var response = await _paymentApiClient.CreatePaymentAsync(paymentRequest);

                var paymentRedirectModel = new PaymentRedirectModel
                {
                    PaReq = response.PaReq,
                    TermUrl = Url.ActionLink("confirm") + $"/{response.TransactionId}",
                    MD = "Order-1",
                    Method = response.Method,
                    Url = "https://dumdumpay.site/secure" // why it returns "http://dummypay.host/secure?
                };

                return View("PaymentRedirect", paymentRedirectModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error happened while calling Payment Api");
                
                if (e is ApiException apiEx)
                    ViewBag.Error = apiEx.GetErrorDescription();
                else
                    ViewBag.Error = e.Message;

                return View(request);
            }
        }

        [HttpGet("payment/confirm/{transactionId}")]
        public async Task<IActionResult> Confirm(Guid transactionId, string md, string paRes)
        {
            var confirmPaymentRequest = new ConfirmPaymentRequest
            {
                PaRes = paRes,
                TransactionId = transactionId
            };

            try
            {
                var response = await _paymentApiClient.ConfirmPaymentAsync(confirmPaymentRequest);

                var model = new PaymentConfirmedModel
                {
                    Status = response.Status,
                    TransactionId = response.TransactionId
                };
                return View(model);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error happened while calling Payment Api");
                
                if (e is ApiException apiEx)
                    ViewBag.Error = apiEx.GetErrorDescription();
                else
                    ViewBag.Error = e.Message;

                return View(new PaymentConfirmedModel());
            }
        }
    }
}