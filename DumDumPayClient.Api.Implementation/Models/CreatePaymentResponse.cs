using System;
using System.Collections.Generic;

namespace DumDumPayClient.Api.Models
{
    public class CreatePaymentResponse
    {
        public CreatePaymentResult Result { get; set; }
        public List<ApiError> Errors { get; set; }
    }
    
    public class CreatePaymentResult
    {
        public Guid TransactionId { get; set; }
        public string TransactionStatus { get; set; }
        public string PaReq { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
    }
    
}
