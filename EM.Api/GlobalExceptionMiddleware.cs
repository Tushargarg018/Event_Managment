﻿using EM.Business.Exceptions;
using EM.Data.Exceptions;
using Newtonsoft.Json;
using System;
using System.Net;

namespace EM.Api
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }   

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "An unexpected error occurred. Please try again later.";
            if (ex is UnauthorizedAccessException)
            {
                statusCode = HttpStatusCode.Forbidden;
                message = ex.Message;
            }
            else if(ex is UserNotFoundException)
            {
                statusCode = HttpStatusCode.Unauthorized;
                message = ex.Message;
            }
            else if(ex is UserInactiveException)
            {
                statusCode = HttpStatusCode.Forbidden;
                message = ex.Message;
            }
            else if (ex is EventAlreadyPublishedException || ex is EarlyBirdOfferExistsException)
            {
                statusCode = HttpStatusCode.Conflict; 
                message = ex.Message;
            }else if(ex is NotFoundException)
            {
                
                statusCode = HttpStatusCode.NotFound;
                message = ex.Message;
            } 
            else if(ex is VenueNotAvailableException)
            {
                statusCode = HttpStatusCode.Conflict;
                message = ex.Message;
            }
            else if(ex is PerformerNotAvailableException)
            {
                statusCode = HttpStatusCode.Conflict;
                message = ex.Message;
            }
            else if(ex is CapacityException)
            {
                statusCode= HttpStatusCode.BadRequest;
                message = ex.Message;
            }
            string[] data = [];
            string[] errors = [message];
            var result = JsonConvert.SerializeObject(new { data = data, status="failure", message="Request Failed with errors.", errors = errors});
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
