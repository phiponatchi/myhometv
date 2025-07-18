using myhometv.Data;
using myhometv.Models.Auth;
using Microsoft.AspNetCore.Mvc;


namespace myhometv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {

        private readonly AppDbContext _db;

        public APIController(AppDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public IEnumerable<APIDefinition> GetAPIs() => _db.ApiDefs;
        

        [HttpGet("{type}")]
        public ActionResult<APIDefinition?> GetByType(APIType type) => _db.ApiDefs.Where(api => api.ApiType == type).SingleOrDefault();


        [HttpGet("types")]
        public ActionResult GetTypes() => Ok(Enum.GetNames(typeof(APIType)));

        [HttpPut]
        public IActionResult UpdateAPI([FromBody] APIDefinition api)
        {
            if (_db.ApiDefs.Where(_api => api.Id == _api.Id).Count() == 0)
            {
                return NotFound();
            }
            _db.ApiDefs.Update(api);
            _db.SaveChanges();
            return Ok(api);
        }
    }
}
