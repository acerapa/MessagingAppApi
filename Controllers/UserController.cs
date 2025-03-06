using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MessagingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController() : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(new List<object> {
                new {
                    Name = "harvey",
                    Age = 21,
                    Email = "harvey@gmail.com"
                },
                new {
                    Name = "Jane",
                    Age = 21,
                    Email = "jane@gmail.com"
                }

            });
        }
    }
}
