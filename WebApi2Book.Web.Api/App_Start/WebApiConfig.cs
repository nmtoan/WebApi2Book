using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;
using System.Web.Http.Tracing;
using WebApi2Book.Common.Logging;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.ErrorHandling;
using WebApi2Book.Web.Common.Routing;

namespace WebApi2Book.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var constraintsResolver = new DefaultInlineConstraintResolver();

            constraintsResolver.ConstraintMap.Add("apiVersionConstraint", typeof(ApiVersionConstraint));
            config.MapHttpAttributeRoutes(constraintsResolver);
            config.Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(config));
            //config.EnableSystemDiagnosticsTracing(); // replaced by custom writer
            config.Services.Replace(typeof(ITraceWriter), new SimpleTraceWriter(WebContainerManager.Get<ILogManager>()));
            config.Services.Add(typeof(IExceptionLogger), new SimpleExceptionLogger(WebContainerManager.Get<ILogManager>()));
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            #region old MapHttpRoute
            //// Web API configuration and services

            //// Web API routes
            //// Enables attribute-based routing
            //config.MapHttpAttributeRoutes();

            //// Matches route with the taskNum parameter
            //config.Routes.MapHttpRoute(
            //    name: "FindByTaskNumberRoute",
            //    routeTemplate: "api/{controller}/{taskNum}",
            //    defaults: new { taskNum = RouteParameter.Optional }
            //);

            //// Default catch-all
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            #endregion
        }
    }
}
