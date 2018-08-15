namespace Anarchy {

        ///<summary>
        ///Type used to specify whether a path should be matched in full or partially.
        ///</summary>
        public enum PathMatch {

            ///<summary>
            ///Full path match option
            ///</summary>
            Full,
            ///<summary>
            ///Partial path match option
            ///</summary>
            Partial
        }
        ///<summary>
        ///</summary>
        public class AnarchyRoute {
            ///<summary>
            ///</summary>
            public PathMatch PathMatch {get; set;}
            ///<summary>
            ///</summary>
            public string Route {get; set;}
            ///<summary>
            ///</summary>
            public string Response {get; set;}
            ///<summary>
            ///</summary>
            public int StatusCode { get; set; }
            ///<summary>
            ///</summary>
            public AnarchyRoute() {
                PathMatch = PathMatch.Partial;
            }
        }
}