using JobPortal.Model.Model;
using JobPortal.Service.Roles;
using Jobportel.Api.Controllers;
using Jobportel.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Admin")]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService role)
        {
            _roleService = role;
        }
        [HttpPost("Roles")]
        public async Task<IActionResult> GetRoles([FromBody] Pagination pagination)
        {
            var roles = await _roleService.GetAll(pagination);
            return OkResponse("Success", roles);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            Role role = await _roleService.GetById(id);
            return OkResponse("Sucess", role);
        }

        [HttpPost("Role")]
        public async Task<IActionResult> AddRole(Role role)
        {
            await _roleService.Add(role);
            return OkResponse("Sucess", role);
        }

        [HttpPut("Role/{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] Role role)
        {
            await _roleService.Update(role);
            return OkResponse("Sucess", role);
        }

        [HttpDelete("Role/{id}")]
        public async Task<IActionResult> DeleteRole(int Id)
        {
            await _roleService.Delete(Id);
            return OkResponse("Sucess", Id);
        }



    }
}
