using myhometv.Data;
using myhometv.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace myhometv.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _db;

        public UserController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet(Name = "get user")]
        public ActionResult<User> GetUser()
        {
            return Ok();
        }

        [HttpPost]
        public ActionResult<User> AddUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return Ok();
        }


    }
}