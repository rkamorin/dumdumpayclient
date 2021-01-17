using System;
using System.Collections.Generic;
using DumDumPayClient.Api.Contracts.Models;

namespace DumDumPayClient.Api.Contracts.Exceptions
{
    public class ApiException : Exception
    {
        public List<ApiError> ApiErrors { get; }

        public ApiException(List<ApiError> apiErrors, string message) : base(message)
        {
            ApiErrors = apiErrors;
        }
    }
}