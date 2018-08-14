using Microsoft.AspNetCore.Mvc;

namespace Anarchy {
    public class ValuesController: Controller {
        
        [Route("api/values")]
        [HttpGet]
        public string[] Get(){
            return new string[] {"a", "b", "c"};
        }
    }
}