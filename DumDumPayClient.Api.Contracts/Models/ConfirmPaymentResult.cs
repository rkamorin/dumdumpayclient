using System;

namespace DumDumPayClient.Api.Contracts.Models
{
    public class ConfirmPaymentResult
    {
        public Guid TransactionId { get; set; }
        public string Status { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public Guid OrderId { get; set; }
        public int LastFourDigits { get; set; }
    }
}