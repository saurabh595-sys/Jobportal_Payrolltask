using JobPortal.Model.Dto.RecrutureDto;
using JobPortal.Service.Jobs;
using Jobportel.Api.Controllers;
using Jobportel.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JobPortal.Api.Controllers.Recruiter
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecruiterController : BaseController
    {
        private readonly IJobService _jobService;
        public RecruiterController(IJobService job)
        {
            _jobService = job;
        }

        [Authorize(Policy = "AdminRecruiterOnly")]
        [HttpPost("Job")]
        public async Task<IActionResult> AddJob(JobAddDto job)
        { 
            Job j =new Job();
            j.Title = job.Title;
            j.Description = job.Description;
            j.CreatedAt = DateTime.Now;
            j.CreatedBy = UserId;
            j.EndAt = DateTime.Now.AddDays(28);
            await _jobService.Add(j);
            return OkResponse("Sucess", job);
        }

        [Authorize(Policy = "AdminRecruiterOnly")]
        [HttpPost("JobsAppliedByCandidate")]
        public async Task<IActionResult> AppliedJobs()
        {
            var AppliedJobs = await _jobService.AppliedJobs(UserId);
            return OkResponse("Sucess", AppliedJobs);
        }

    }
}
