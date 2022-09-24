using JobPortal.Api.Dto;
using JobPortal.Model.Dto.JobDto;
using JobPortal.Model.Dto.UserDto;
using JobPortal.Model.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobPortal.Service.Admin
{
    public interface IAdminService
    {
        Task<IEnumerable<UserGetDto>> GetcandidatesAsync(Pagination pagination);
      
        Task<IEnumerable<UserGetDto>> GetrecruitersAsync(Pagination pagination);
        
        Task<IEnumerable<GetJobDto>> GetjobsAsync(Pagination pagination);
       
        Task<IEnumerable<JobApplied>> GetJobAppliedcandidatesAsync(Pagination pagination);
      
        Task<bool> DeleteCandidateAsync(int id);
        Task<bool> DeleteRecruiterAsync(int id);
        Task<bool> DeleteJobAsync(int id);
    }
}
