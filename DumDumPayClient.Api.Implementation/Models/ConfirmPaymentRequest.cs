using System;

namespace DumDumPayClient.Api.Models
{
    public class ConfirmPaymentRequest
    {
        public Guid TransactionId { get; set; }
        public string PaRes { get; set; }
    }
}