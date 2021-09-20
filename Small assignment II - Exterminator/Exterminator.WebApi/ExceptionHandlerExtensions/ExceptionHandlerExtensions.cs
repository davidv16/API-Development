using System;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Exterminator.Models;
using Exterminator.Models.Exceptions;
using Exterminator.Services.Interfaces;

namespace Exterminator.WebApi.ExceptionHandlerExtensions
{
  public static class ExceptionHandlerExtensions
  {
    public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
      app.UseExceptionHandler(err =>
      {
        err.Run(async context =>
        {
          var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
          var statusCode = (int)HttpStatusCode.InternalServerError;
          var exception = exceptionHandlerFeature.Error;
          var logService = app.ApplicationServices.GetService(typeof(ILogService)) as ILogService;


          if (exception is ResourceNotFoundException)
          {
            statusCode = (int)HttpStatusCode.NotFound;
          }
          else if (exception is ModelFormatException)
          {
            statusCode = (int)HttpStatusCode.PreconditionFailed;
          }
          else if (exception is ArgumentOutOfRangeException)
          {
            statusCode = (int)HttpStatusCode.BadRequest;
          }

          var exceptionToLog = new ExceptionModel
          {
            StatusCode = statusCode,
            ExceptionMessage = exception.Message,
            StackTrace = exception.StackTrace

          };

          logService.LogToDatabase(exceptionToLog);

          context.Response.ContentType = "application/json";
          context.Response.StatusCode = statusCode;

          await context.Response.WriteAsync(new ExceptionModel
          {
            StatusCode = statusCode,
            ExceptionMessage = exception.Message
          }.ToString());
        });

      });
    }
  }
}