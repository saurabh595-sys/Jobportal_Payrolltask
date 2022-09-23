using JobPortal.Service.Applicants;
using Jobportel.Api.Controllers;
using Jobportel.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Api.Controllers.Applicants
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : BaseController
    {
        private readonly IApplicantService _applicantService;
        public ApplicantController(IApplicantService applicant)
        {
            _applicantService = applicant;
        }
        [Authorize(Policy = "AdminRecruiterOnly")]
        [HttpGet("Applicants")]
        public async Task<IActionResult> GetApplicant()
        {
            var applicants = await _applicantService.GetAll();
            return OkResponse("Success", applicants);
        }
        [Authorize(Policy = "AdminCandidateOnly")]
        [HttpGet("Appliedjob")]
        public async Task<IActionResult> Appliedjob()
        {
            var jobApplieds = await _applicantService.AppliedJobs(UserId);
            return OkResponse("Success", jobApplieds);
        }

        [Authorize(Policy = "AllAllowed")]
        [HttpPost("{id}")]
        public async Task<IActionResult> GetApplicantById(int Id)
        {
            Applicant applicant = await _applicantService.GetById(Id);
            return OkResponse("Sucess", applicant);
        }
        
            [Authorize(Policy = "AdminCandidateOnly")]
        [HttpPost("ApplyJob")]
        public async Task<IActionResult> ApplyJob(Applicant applicant)
        {
            applicant.AppliedBy = UserId;
          
            Applicant applicants= await _applicantService.Add(applicant);
            return OkResponse("Sucess", applicants);
        }
        [Authorize(Policy = "AdminRecruiterOnly")]
        [HttpPut("Applicant/{id}")]
        public async Task<IActionResult> UpdateApplicant(int id, [FromBody] Applicant applicant)
        {
           var applicants= await _applicantService.Update(applicant);
            return OkResponse("Sucess", applicants);
        }
        [Authorize(Policy = "AdminRecruiterOnly")]
        [HttpDelete("Applicant/{id}")]
        public async Task<IActionResult> DeleteApplicant(int Id)
        {
            await _applicantService.Delete(Id);
            return OkResponse("Sucess", Id);
        }

    }
}

