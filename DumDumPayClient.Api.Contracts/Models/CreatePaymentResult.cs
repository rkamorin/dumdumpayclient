using System;

namespace DumDumPayClient.Api.Contracts.Models
{
    public class CreatePaymentResult
    {
        public Guid TransactionId { get; set; }
        public string TransactionStatus { get; set; }
        public string PaReq { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
    }
}