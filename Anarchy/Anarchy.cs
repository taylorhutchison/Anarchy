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
    public static class AnarchyMiddlewareExtensions
    {
        public static IApplicationBuilder UseAnarchy(
            this IApplicationBuilder builder, Action<AnarchyConfig> configure)
        {
            var anarchyConfig = new AnarchyConfig();
            configure(anarchyConfig);
            return builder.UseMiddleware<AnarchyMiddleware>(anarchyConfig);
        }
    }

    public class AnarchyRoute {
        public string Route {get; set;}
        public string Response {get; set;}
        public int StatusCode { get; set; }
    }

    public class AnarchyConfig
    {
        public List<AnarchyRoute> Routes {get; private set;}
        public Entropy Entropy { get; set; }
        public Func<bool> Enabled { get; set; }
        public void Route(Action<AnarchyRoute> configure) {
            var route = new AnarchyRoute();
            configure(route);
            Routes.Add(route);
        }

        public void Route(string route, string response, int statusCode) {
            var anarchyRoute = new AnarchyRoute {
                Route = route,
                Response = response,
                StatusCode = statusCode
            };
            Routes.Add(anarchyRoute);
        }

        public AnarchyConfig() {
            Routes = new List<AnarchyRoute>();
            Entropy = Entropy.Inconvience;
            Enabled = () => false;
        }

    }

    public enum Entropy {
        Inconvience,
        Disorder,
        VortexOfChaos,
        EndTimes
    }

    public class AnarchyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AnarchyConfig _config;
        private Random _rng;
        public AnarchyMiddleware(RequestDelegate next, AnarchyConfig config)
        {
            _next = next;
            _config = config;
            _rng = new Random();
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (_config.Enabled())
            {
                var firstMatchingRoute = MatchingRoute(context.Request.Path.Value);
                if(firstMatchingRoute != null && Intercept(_config.Entropy)) {
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

        private bool Intercept(Entropy entropy)
        {
            var random = _rng.Next(0, 100);
            switch(entropy)
            {
                case Entropy.Inconvience:
                    return random > 75;
                case Entropy.Disorder:
                    return random > 50;
                case Entropy.VortexOfChaos:
                    return random > 25;
                case Entropy.EndTimes:
                    return random > 1;
                default:
                    return false;
            }
        }
    }
}
