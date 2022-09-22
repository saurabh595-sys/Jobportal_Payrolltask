using JobPortal.Service.Applicants;
using Jobportel.Api.Controllers;
using Jobportel.Data.Model;
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

        [HttpGet("Applicants")]
        public async Task<IActionResult> GetApplicant()
        {
            var applicants = await _applicantService.GetAll();
            return OkResponse("Success", applicants);
        }

        [HttpGet("Appliedjob")]
        public async Task<IActionResult> Appliedjob()
        {
            var jobApplieds = await _applicantService.AppliedJobs(UserId);
            return OkResponse("Success", jobApplieds);
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> GetApplicantById(int Id)
        {
            Applicant applicant = await _applicantService.GetById(Id);
            return OkResponse("Sucess", applicant);
        }

        [HttpPost("ApplyJob")]
        public async Task<IActionResult> ApplyJob(Applicant applicant)
        {
            applicant.AppliedBy = UserId;
          
            Applicant applicants= await _applicantService.Add(applicant);
            return OkResponse("Sucess", applicants);
        }

        [HttpPut("Applicant/{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] Applicant applicant)
        {
           var applicants= await _applicantService.Update(applicant);
            return OkResponse("Sucess", applicants);
        }

        [HttpDelete("Applicant/{id}")]
        public async Task<IActionResult> DeleteRole(int Id)
        {
            await _applicantService.Delete(Id);
            return OkResponse("Sucess", Id);
        }

    }
}

