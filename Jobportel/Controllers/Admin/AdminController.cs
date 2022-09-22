using JobPortal.Api.Dto;
using JobPortal.Service.Admin;
using Jobportel.Api.Controllers;
using Jobportel.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService admin)
        {
            _adminService = admin;
        }

        [HttpGet("Candidates")]
        public async Task<IActionResult> Candidates()
        {
            var candidates = await _adminService.GetcandidatesAsync();
            return OkResponse("Success", candidates);
        }

        [HttpGet("Recruiters")]
        public async Task<IActionResult> Recruiters()
        {
            var Jobs = await _adminService.GetrecruitersAsync();
            return OkResponse("Success", Jobs);
        }

        [HttpGet("Jobs")]
        public async Task<IActionResult> Jobs()
        {
            var Jobs = await _adminService.GetjobsAsync();
            return OkResponse("Success", Jobs);
        }

        [HttpDelete("Candidate/{Id}")]
        public async Task<IActionResult> CandidateDelete(int Id)
        {
            var Jobs = await _adminService.DeleteCandidateAsync(Id);
            return OkResponse("Success", Jobs);
        }

        [HttpDelete("Recruiter/{Id}")]
        public async Task<IActionResult> DeleteRecruiter(int Id)
        {
            var Jobs = await _adminService.DeleteRecruiterAsync(Id);
            return OkResponse("Success", Jobs);
        }

        [HttpDelete("job/{Id}")]
        public async Task<IActionResult> DeleteJob(int Id)
        {
            var Jobs = await _adminService.DeleteJobAsync(Id);
            return OkResponse("Success", Jobs);
        }

        [HttpGet("JobAppliedByCandidates")]
        public async Task<IActionResult> JobAppliedByCandidates()
        {
            var candidates = await _adminService.GetJobAppliedcandidatesAsync();
            return OkResponse("Success", candidates);
        }

        [HttpGet("Export")]
        public async Task<IActionResult> ExportFile()
        {
            IEnumerable<User> candidatesList =await _adminService.GetcandidatesAsync();
            IEnumerable<User> recruitersList =await _adminService.GetrecruitersAsync();
            IEnumerable<JobApplied> jobsAppliedByCandidatesList =await _adminService.GetJobAppliedcandidatesAsync();

            List<IEnumerable<dynamic>> data = new List<IEnumerable<dynamic>>();
              data.Add(candidatesList);
            data.Add(recruitersList);
            data.Add(jobsAppliedByCandidatesList);
            return Export(data);
        }

      

    }
}
