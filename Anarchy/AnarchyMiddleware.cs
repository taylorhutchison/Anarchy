using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Anarchy
{
    ///<summary>
    ///Extension class for Anarchy.
    ///</summary>
    public static class AnarchyMiddlewareExtensions
    {
        ///<summary>
        ///Extension method to add the AnarchyMiddleware to the IApplicationBuilder interface.
        ///</summary>
        public static IApplicationBuilder UseAnarchy(
            this IApplicationBuilder builder, Action<AnarchyConfig> configure)
        {
            var anarchyConfig = new AnarchyConfig();
            configure(anarchyConfig);
            return builder.UseMiddleware<AnarchyMiddleware>(anarchyConfig);
        }
    }

    ///<summary>
    ///Anarchy Middleware for ASP.NET
    ///</summary>
    public class AnarchyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AnarchyConfig _config;
        private Random _rng;

        ///<summary>
        ///AnarchyMiddle constructor used by Middleware pipeline.
        ///</summary>
        public AnarchyMiddleware(RequestDelegate next, AnarchyConfig config)
        {
            _next = next;
            _config = config;
            _rng = new Random();
        }

        ///<summary>
        ///Invoke method used by Middleware pipeline.
        ///</summary>
        public async Task InvokeAsync(HttpContext context)
        {
            if (_config.Enabled())
            {
                var firstMatchingRoute = MatchingRoute(context.Request.Path.Value);
                if(firstMatchingRoute != null && Intercept(_config.CaptureRate)) {
                    context.Response.StatusCode = firstMatchingRoute.StatusCode;
                    await context.Response.WriteAsync(firstMatchingRoute.Response);
                }
                else {
                    await _next(context);
                }
            }
            else 
            {
                await _next(context);
            }
        }

        private bool RouteMatches(string route) {
            Console.WriteLine(route);
            return _config.Routes.Select(r => r.Route).Any(r => r.Contains(route));
        }

        private AnarchyRoute MatchingRoute(string route) {
            if(_config.Routes != null && _config.Routes.Any()) {
                return _config.Routes.FirstOrDefault(r => route.Contains(r.Route));
            }
            return null;
        }

        private bool Intercept(int percent)
        {
            var random = _rng.Next(0, 100);
            return percent > random;
        }
    }
}
