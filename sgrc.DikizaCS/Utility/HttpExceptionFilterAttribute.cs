using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace sgrc.DikizaCS.Utility
{
    public class HttpExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is NotImplementedException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
            else if (context.Exception is FormatException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(context.Exception.Message),
                    //ReasonPhrase = "An unhandled exception was thrown by Customer Web API controller."
                };
            }
            else
                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An unhandled exception was thrown by the Web Application."),
                    //ReasonPhrase = "An unhandled exception was thrown by the Web Application."
                };
        }
    }
}