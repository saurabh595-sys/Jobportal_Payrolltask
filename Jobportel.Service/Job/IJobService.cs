
using System.Collections.Generic;

using System.Threading.Tasks;
using Jobportel.Data.Model;
using JobPortal.Api.Dto;
using JobPortal.Model.Model;
using JobPortal.Model.Dto.JobDto;

namespace JobPortal.Service.Jobs
{
   public interface IJobService
    {
        Task<IEnumerable<GetJobDto>> GetAll(Pagination pagination);
        Task<IEnumerable<JobApplied>> AppliedJobs(int id);
        
        Task<Job> GetById(int id);
        Task<Job> Add(Job job);
        Task<Job> Update(int id, Job job);
        Task<bool> Delete(int id);
    }
}
