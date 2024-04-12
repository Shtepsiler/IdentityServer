using BlazorAppForClient.Exceptions;
using BlazorAppForClient.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net;

namespace BlazorAppForClient.Extensions
{
    public static class StatusCodeHandler
    {

        private static readonly Dictionary<HttpStatusCode, Action<string>> statusCodesHandlers = new()
        {
            {
                HttpStatusCode.NotFound,
                responseBody =>
                {
                    throw new EntityNotFoundException(
                        responseBody.Deserialize<ErrorViewModel>().Message);
                }
            },
            {
                HttpStatusCode.BadRequest,
                responseBody =>
                {
                    throw new ValidationException(
                        responseBody.Deserialize<ValidationErrorViewModel>().Errors);
                }
            },
            {
                HttpStatusCode.InternalServerError,
                responseBody =>
                {
                    throw new ServerResponseException(
                        responseBody.Deserialize<ErrorViewModel>().Message);
                }
            },
                        {
                HttpStatusCode.Unauthorized,
                responseBody =>
                {
                    throw new NotAuthrizeExeption();
                }
            }
            
           











        };

        public static void TryHandleStatusCode(HttpStatusCode statusCode, string responseBody)
        {
            if (statusCodesHandlers.TryGetValue(statusCode, out Action<string> statusCodeHandler))
            {
                statusCodeHandler.Invoke(responseBody);
            }
        }
    }
}
