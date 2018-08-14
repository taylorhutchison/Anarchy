using Microsoft.AspNetCore.Mvc;

namespace Anarchy {
    public class DataController: Controller {
        
        [Route("api/data")]
        [HttpGet]
        public int[] Get(){
            return new int[] {1, 2, 3};
        }
    }
}