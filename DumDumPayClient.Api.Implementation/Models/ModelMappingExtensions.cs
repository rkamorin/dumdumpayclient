namespace DumDumPayClient.Api.Models
{
    /// <summary>
    /// Might use mapping libraries like Automapper to avoid writing this code manually
    /// </summary>
    public static class ModelMappingExtensions
    {
        public static CreatePaymentRequest ToApiRequest(this Contracts.Models.CreatePaymentRequest request)
        {
            return new CreatePaymentRequest
            {
                Amount = request.Amount,
                Country = request.Country,
                Currency = request.Currency,
                CardHolder = request.CardHolder,
                Cvv = request.Cvv,
                CardNumber = request.CardNumber,
                CardExpiryDate = request.CardExpiryDate,
                OrderId = request.OrderId
            };
        }

        public static ConfirmPaymentRequest ToApiRequest(this Contracts.Models.ConfirmPaymentRequest request)
        {
            return new ConfirmPaymentRequest
            {
                PaRes = request.PaRes,
                TransactionId = request.TransactionId
            };
        }

        public static Api.Contracts.Models.ApiError ToContract(this ApiError error)
        {
            return new Api.Contracts.Models.ApiError
            {
                Message = error.Message,
                Type = error.Type
            };
        }

        public static Api.Contracts.Models.CreatePaymentResult ToContract(this CreatePaymentResult result)
        {
            return new Contracts.Models.CreatePaymentResult
            {
                Method = result.Method,
                TransactionId = result.TransactionId,
                Url = result.Url,
                PaReq = result.PaReq,
                TransactionStatus = result.TransactionStatus
            };
        }

        public static Api.Contracts.Models.ConfirmPaymentResult ToContract(this ConfirmPaymentResult result)
        {
            return new Contracts.Models.ConfirmPaymentResult
            {
                Amount = result.Amount,
                Currency = result.Currency,
                Status = result.Status,
                OrderId = result.OrderId,
                TransactionId = result.TransactionId,
                LastFourDigits = result.LastFourDigits
            };
        }
    }
}