using JobPortal.Model.Model;
using Jobportel.Data.Model;
using Jobportel.Model;
using Jobportel.Service.Users;
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

        [HttpPost("Users")]
        public async Task<IActionResult> GetUsers([FromBody] Pagination pagination)
        {
            var user = await _user.GetAll(pagination);
            return OkResponse( "Success",user);
        }


        [HttpPost("User/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            User user = await _user.GetById(id);
            return OkResponse("Sucess", user);
        }

        [HttpPost("User")]
        public async Task<IActionResult> AddUser(User user)
        {
            await _user.Add(user);
            return OkResponse("Sucess", user);
        }

        [HttpPut("User/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            await _user.Update(user);
            return OkResponse("Sucess", user);
        }

        [HttpDelete("User/{id}")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            await _user.Delete(Id);
            return OkResponse("Sucess", Id);
        }


    }
}
