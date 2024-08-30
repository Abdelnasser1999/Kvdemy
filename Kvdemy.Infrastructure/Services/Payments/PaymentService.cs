using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Kvdemy.Web.Data;
using Kvdemy.Core.Dtos;
using Kvdemy.Infrastructure.Services.Interfaces;
using Kvdemy.Infrastructure.Services.Notifications;
using Microsoft.Extensions.Localization;
using AutoMapper;
using Kvdemy.Core.Resourses;
using Kvdemy.Infrastructure.Services.Payments;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;
using PayPalCheckoutSdk.Payments;

namespace Krooti.Infrastructure.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly KvdemyDbContext _context;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IStringLocalizer<Messages> _localizedMessages;

        public PaymentService(KvdemyDbContext dbContext, IMapper mapper,
            INotificationService notificationService, IStringLocalizer<Messages> localizedMessages)
        {
            _context = dbContext;
            _mapper = mapper;
            _notificationService = notificationService;
            _localizedMessages = localizedMessages;

        }

        public async Task<HttpResponse> CreatePaymentAsync(decimal amount, string returnUrl, string cancelUrl)
        {
            var orderRequest = new OrderRequest
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>
        {
            new PurchaseUnitRequest
            {
                AmountWithBreakdown = new AmountWithBreakdown
                {
                    CurrencyCode = "USD",
                    Value = amount.ToString("F")
                }
            }
        },
                ApplicationContext = new ApplicationContext
                {
                    ReturnUrl = returnUrl,
                    CancelUrl = cancelUrl
                }
            };

            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(orderRequest);

            var response = await PayPalClient.Client().Execute(request);
            return response;
        }

        public async Task<HttpResponse> RefundPaymentAsync(string captureId, decimal amount)
        {
            var refundRequest = new RefundRequest
            {
                Amount = new PayPalCheckoutSdk.Payments.Money
                {
                    CurrencyCode = "USD",
                    Value = amount.ToString("F")
                }
            };

            var request = new CapturesRefundRequest(captureId);
            request.RequestBody(refundRequest);

            var response = await PayPalClient.Client().Execute(request);
            return response;
        }

    }
}
