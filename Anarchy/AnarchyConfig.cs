using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anarchy {
    ///<summary>
    ///Configuration object for Anarchy.
    ///</summary>
    public class AnarchyConfig
    {
        ///<summary>
        ///A list of routes for Anarchy to match.
        ///</summary>
        public List<AnarchyRoute> Routes {get; private set;}

        ///<summary>
        ///The likelhood that a route will fail or succeed.
        ///</summary>
        public int FailPercent { get; set; }

        ///<summary>
        ///Used to specify a method to check check at run-time if Anarchy should be enabled.false
        ///</summary>
        public Func<bool> Enabled { get; set; }

        ///<summary>
        ///Flag to specify if all routes should be matched. When set to true, any settings for specific routes will be ignored.
        ///</summary>
        public bool MatchAllRoutes {get; set;}

        ///<summary>
        ///Add and configure a new AnarchyRoute
        ///</summary>
        public void Route(Action<AnarchyRoute> configure) {
            var route = new AnarchyRoute();
            configure(route);
            Routes.Add(route);
        }

        ///<summary>
        ///Add and configure a new AnarchyRoute
        ///</summary>
        public void Route(string route, string response, int statusCode) {
            var anarchyRoute = new AnarchyRoute {
                Route = route,
                Response = response,
                StatusCode = statusCode
            };
            Routes.Add(anarchyRoute);
        }
        ///<summary>
        ///Default constructor that initializes AnarchyConfig properties.
        ///</summary>
        public AnarchyConfig() {
            Routes = new List<AnarchyRoute>();
            FailPercent = 25;
            Enabled = () => false;
            MatchAllRoutes = false;
        }

    }
}