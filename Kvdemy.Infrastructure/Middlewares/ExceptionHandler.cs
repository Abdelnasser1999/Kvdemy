using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Kvdemy.Core.ViewModels;
using Kvdemy.Core.Exceptions;

namespace Kvdemy.Infrastructure.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public ExceptionHandler(RequestDelegate next,
                                IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature.Error;
            var response = new JsonResponseViewModel();
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            switch (exception)
            {
                case DuplicateEmailException _:
                case DuplicatePhoneException _:
                case EntityNotFoundException _:
                case OperationFailedException _:


                    response.msg = $"e:{exception.Message}";
                    response.close = 0;
                    response.status = false;
                    break;

                case UserNotFoundException _:
                case TokenInvalidException _:
                case NationalityNotFoundException _:
                case ProductNotFoundException _:
                case NoEnoughProductQuantityException _:
                case CouponNotFoundException _:
                case CouponExpireDateException _:
                    response.msg = $"{exception.Message}";
                    response.close = 0;
                    response.status = false;
                    break;

                default:
                    response.msg = $"e:{exception}";
                    response.close = 0;
                    response.status = false;
                    var requestBody = string.Empty;
                    var req = context.Request;
                    req.EnableBuffering();
                    if (req.Body.CanSeek)
                    {
                        req.Body.Seek(0, SeekOrigin.Begin);
                        using (var reader = new StreamReader(
                            req.Body,
                            Encoding.UTF8,
                            false,
                            8192,
                            true))
                        {
                            requestBody = await reader.ReadToEndAsync();
                        }
                        req.Body.Seek(0, SeekOrigin.Begin);
                    }
                    break;
            }

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(
                JsonConvert.SerializeObject(response)
            );
        }
    }
}
