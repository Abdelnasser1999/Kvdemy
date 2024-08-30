using Kvdemy.Core.Dtos;
using Kvdemy.Core.Enums;
using Kvdemy.Core.ViewModels;
using PayPalHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Payments
{
    public interface IPaymentService
    {
        Task<HttpResponse> CreatePaymentAsync(decimal amount, string returnUrl, string cancelUrl);
        Task<HttpResponse> RefundPaymentAsync(string captureId, decimal amount);

    }
}
