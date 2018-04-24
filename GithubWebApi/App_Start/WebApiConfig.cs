using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using GithubWebApi.Models;
using System.Net.Http.Headers;
    
namespace GithubWebApi
{
    
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            //builder.EntitySet<User>("Users");
            //builder.EntitySet<Organization>("Organizations");
            //builder.EntitySet<OrganizationMember>("OrganizationMembers");
            //builder.EntitySet<Repository>("Repositories");
            //config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());


        }
    }
}
