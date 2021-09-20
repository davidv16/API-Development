using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TechnicalRadiation.WebApi.Attributes
{
    public class AuthorizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            
            if(httpContext.Request.Headers["Authorization"] != "188d0539-c6dc-4155-9ec4-a2a17412068c")
            {
                context.Result = new JsonResult(new { HttpStatusCode.Unauthorized });
            }
        }  
    }
}