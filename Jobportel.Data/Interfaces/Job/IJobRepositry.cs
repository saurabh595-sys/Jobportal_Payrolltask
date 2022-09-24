using JobPortal.Model.Dto.JobDto;
using JobPortal.Model.Model;
using Jobportel.Data.Infrastructure;
using Jobportel.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobPortal.Data.Repositories.Jobs
{
    public interface IJobRepositry:IRepository<Job>
    {
        Task<IEnumerable<GetJobDto>> GetJobs(Pagination pagination);
    }
}
