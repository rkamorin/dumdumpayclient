using System.Text;
using DumDumPayClient.Api.Contracts.Exceptions;

namespace DumDumPayClient.Example.Models
{
    public static class Extensions
    {
        public static string GetErrorDescription(this ApiException e)
        {
            var sb = new StringBuilder();
            sb.AppendLine(e.Message);
            sb.AppendLine("Details");
            foreach (var error in e.ApiErrors)
            {
                sb.AppendLine($"{error.Type}: {error.Message}");
            }

            return sb.ToString();
        }
    }
}