using MessagingApp.Models.DTOs;
using MessagingApp.Models.Entities;
using MessagingApp.Models.Responses;
using MessagingApp.Services.Users;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MessagingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<UserResponse[]>> GetUsers()
        {
            UserResponse[] users = await _userService.GetUsers();

            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserCreateDTO userCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                User user = await _userService.CreateUserAysnc(userCreateDTO);

                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUser(int id)
        {
            UserResponse? user = await _userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, UserUpdateDTO userUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User? updatedUser = await _userService.UpdateUser(id, userUpdateDTO);

            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            UserResponse? user = await _userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteUser(user);

            return NoContent();
        }
    }
}
