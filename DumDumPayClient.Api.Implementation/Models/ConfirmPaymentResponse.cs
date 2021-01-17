using System;
using System.Collections.Generic;

namespace DumDumPayClient.Api.Models
{
    public class ConfirmPaymentResponse
    {
        public ConfirmPaymentResult Result { get; set; }
        public List<ApiError> Errors { get; set; }
    }
    
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