using System.Threading.Tasks;
using DumDumPayClient.Api.Contracts.Models;

namespace DumDumPayClient.Api.Contracts
{
    public interface IPaymentApiClient
    {
        Task<CreatePaymentResult> CreatePaymentAsync(CreatePaymentRequest request);
        Task<ConfirmPaymentResult> ConfirmPaymentAsync(ConfirmPaymentRequest request);
    }
}
