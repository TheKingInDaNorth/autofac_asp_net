using Autofac;
using Autofac.Integration.Owin;
using Lib.Abstractions;
using Microsoft.Owin;
using System;
using System.Threading.Tasks;

namespace OwinHost
{
    public class LoggerMiddleware : OwinMiddleware
    {
        public ILogger _Logger;
        public LoggerMiddleware(OwinMiddleware next, ILogger logger) : base(next)
        {
            _Logger = logger;
        }

        public override async Task Invoke(IOwinContext context)
        {
            ILifetimeScope scope = context.GetAutofacLifetimeScope();

            _Logger.Log("Inside the 'Invoke' method of the 'LoggerMiddleware' middleware.");
           
            await Next.Invoke(context);
        }
    }
}
