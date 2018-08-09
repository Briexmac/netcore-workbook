using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace BaseProject.Intrastructure
{
    public class UnwrappedExceptionMiddleware
    {

        private readonly RequestDelegate next;
        public UnwrappedExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex.GetBaseException()).Throw();
            }
        }
    }
}
