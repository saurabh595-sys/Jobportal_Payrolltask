using JobPortal.Api.Dto;
using JobPortal.Data.Interfaces.Applicants;
using JobPortal.Data.Repositories.Jobs;
using Jobportel.Data;
using Jobportel.Data.Interfaces;
using Jobportel.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service.Admin
{
    public class AdminService : IAdminService
    {
        public readonly IApplicantRepository _applicantRepository;
        public readonly IJobRepositry _jobRepositry;
        public readonly IUserRepository _userRepository;
        public readonly Context _context;

        public AdminService(IApplicantRepository applicant,IJobRepositry job,IUserRepository user,Context context)
        {
            _applicantRepository = applicant;
            _jobRepositry = job;
            _userRepository = user;
            _context = context;


        }
        public async Task<IEnumerable<User>> GetcandidatesAsync()
        {
            return await _userRepository.GetDefaultList(x => x.IsActive == true && x.RoleId == 2);
        }
        public async Task<IEnumerable<Job>> GetjobsAsync()
        {
            return await _jobRepositry.GetDefaultList(x => x.IsActive == true);
        }
        public async Task<IEnumerable<User>> GetrecruitersAsync()
        {
            return await _userRepository.GetDefaultList(x => x.IsActive == true && x.RoleId == 3);
        }
        public async Task<bool> DeleteCandidateAsync(int id)
        {
            User user = await _userRepository.GetById(id);

            if (user != null)
            { 
                await _userRepository.Delete(user); return true; 
            }
            else { 
                return false;
            }
        }
        public async Task<bool> DeleteJobAsync(int id)
        {
           
            Job job = await _jobRepositry.GetById(id);

            if (job != null)
            {
                await _jobRepositry.Delete(job); return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteRecruiterAsync(int id)
        {
            User user = await _userRepository.GetById(id);

            if (user != null)
            {
                await _userRepository.Delete(user); return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<JobApplied>> GetJobAppliedcandidatesAsync()
        {
            var applicant = await (from u in _context.User
                                  join a in _context.Applicant on u.Id equals a.AppliedBy
                                  join j in _context.Job on a.jobId equals j.Id
                                  select new JobApplied
                                  {
                                      CandidateId = u.Id,
                                      CandidateName=u.Name,
                                      JobTitle=j.Title,
                                      Description=j.Description,
                                      AppliedAt=a.AppliedAt
                                  }).ToListAsync();

            return applicant;
           
        }
    }
}
