using Microsoft.AspNetCore.Mvc;

namespace MessagingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            return Ok(Task.CompletedTask);
        }
    }
}