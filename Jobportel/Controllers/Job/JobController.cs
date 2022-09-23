using JobPortal.Service.Jobs;
using JobPortal.Service.Roles;
using Jobportel.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jobportel.Data.Model;
using JobPortal.Model.Model;
using Microsoft.AspNetCore.Authorization;

namespace JobPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : BaseController
    {
        private readonly IJobService _jobService;
        public JobController(IJobService job)
        {
            _jobService = job;
        }
        [Authorize(Policy = "AllAllowed")]
        [HttpPost("Jobs")]
        public async Task<IActionResult> GetJob([FromBody] Pagination pagination)
        {
            var Jobs = await _jobService.GetAll(pagination);
            return OkResponse("Success", Jobs);
        }
        [Authorize(Policy = "AllAllowed")]
        [HttpPost("GetJobById/{Id}")]
        public async Task<IActionResult> GetJobById(int Id)
        {
            Job job  = await _jobService.GetById(Id);
            return OkResponse("Sucess", job);
        }
        [Authorize(Policy = "AdminRecruiterOnly")]
        [HttpPut("Update/Job")]
        public async Task<IActionResult> UpdateJob(int id, [FromBody] Job job)
        {
            await _jobService.Update(job);
            return OkResponse("Sucess", job);
        }
        [Authorize(Policy = "AdminRecruiterOnly")]
        [HttpDelete("Job/{id}")]
        public async Task<IActionResult> DeleteJob(int Id)
        {
            await _jobService.Delete(Id);
            return OkResponse("Sucess", Id);
        }

    }
}
