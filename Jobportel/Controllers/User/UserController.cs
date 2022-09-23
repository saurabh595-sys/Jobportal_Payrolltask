using JobPortal.Model.Dto.UserDto;
using JobPortal.Model.Model;
using Jobportel.Data.Model;
using Jobportel.Model;
using Jobportel.Service.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobportel.Api.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _user;
        public UserController(IUserService user)
        {
            _user = user;       
        }
        [Authorize(Policy = "Admin")]
        [HttpPost("Users")]
        public async Task<IActionResult> GetUsers([FromBody] Pagination pagination)
        {
            if (!ModelState.IsValid)
            {
                var user = await _user.GetAll(pagination);
                return OkResponse("Success", user);
            }
            return BadResponse("Enter Proper Details", pagination);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("User/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (!ModelState.IsValid)
            {
                User user = await _user.GetById(id);
                return OkResponse("Sucess", user);
            }
            return BadResponse("Enter Proper Details", id);
        }

        [AllowAnonymous]
        [HttpPost("User")]
        public async Task<IActionResult> AddUser(UserAddDto user)
        {

            if (user.RoleId == 2)
            {
                if (!HttpContext.User.IsInRole("Admin"))
                    return BadResponse("Please login As admin ","");
            }
            await _user.Add(user);
            return OkResponse("Sucess", user);
            
        }
        [Authorize(Policy = "AllAllowed")]
        [HttpPut("User/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (!ModelState.IsValid) { 
            await _user.Update(user);
            return OkResponse("Sucess", user);
            }
            return BadResponse("Enter Proper Details", user);
        }
        [Authorize(Policy = "Admin")]
        [HttpDelete("User/{id}")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            if (!ModelState.IsValid)
            {
                await _user.Delete(Id);
                return OkResponse("Sucess", Id);
            }
            return BadResponse("Enter Proper ID", Id);
        }


    }
}
