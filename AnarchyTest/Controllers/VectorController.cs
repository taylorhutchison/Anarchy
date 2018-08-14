using Microsoft.AspNetCore.Mvc;

namespace Anarchy {
    public class VectorController: Controller {
        
        [Route("api/vector")]
        [HttpGet]
        public string[] Get(){
            return new string[] {"a", "b", "c"};
        }
    }
}