using JobPortal.Api.Dto;
using Jobportel.Data.Model;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service.Admin
{
    public interface IAdminService
    {
        Task<IEnumerable<User>> GetcandidatesAsync();

        Task<IEnumerable<User>> GetrecruitersAsync();
        Task<IEnumerable<Job>> GetjobsAsync();
        Task<IEnumerable<JobApplied>> GetJobAppliedcandidatesAsync();
        Task<bool> DeleteCandidateAsync(int id);
        Task<bool> DeleteRecruiterAsync(int id);
        Task<bool> DeleteJobAsync(int id);
    }
}
