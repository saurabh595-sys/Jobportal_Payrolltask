using JobPortal.Api.Dto;
using JobPortal.Model.Dto.UserDto;
using JobPortal.Model.Model;
using JobPortal.Service.Admin;
using Jobportel.Api.Controllers;
using Jobportel.Data.Model;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = "Admin")]
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService admin)
        {
            _adminService = admin;
        }

        [HttpPost("Candidates")]
        public async Task<IActionResult> Candidates(Pagination pagination)
        {
            var candidates = await _adminService.GetcandidatesAsync(pagination);
            return OkResponse("Success", candidates);
        }

        [HttpPost("Recruiters")]
        public async Task<IActionResult> Recruiters(Pagination pagination)
        {
            var Jobs = await _adminService.GetrecruitersAsync(pagination);
            return OkResponse("Success", Jobs);
        }

        [HttpPost("Jobs")]
        public async Task<IActionResult> Jobs(Pagination pagination)
        {
            var Jobs = await _adminService.GetjobsAsync(pagination);
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
        public async Task<IActionResult> JobAppliedByCandidates(Pagination pagination)
        {
            var candidates = await _adminService.GetJobAppliedcandidatesAsync(pagination);
            return OkResponse("Success", candidates);
        }

        [HttpPost("Export")]
        public async Task<IActionResult> ExportFile(Pagination pagination)
        {
            IEnumerable<UserGetDto> candidatesList = await _adminService.GetcandidatesAsync(pagination);
            IEnumerable<UserGetDto> recruitersList =await _adminService.GetrecruitersAsync(pagination);
            IEnumerable<JobApplied> jobsAppliedByCandidatesList =await _adminService.GetJobAppliedcandidatesAsync(pagination);

            List<IEnumerable<dynamic>> data = new List<IEnumerable<dynamic>>();
              data.Add(candidatesList);
            data.Add(recruitersList);
            data.Add(jobsAppliedByCandidatesList);
            return Export(data);
        }

      

    }
}
