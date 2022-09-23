using JobPortal.Data.Interfaces.Roles;
using JobPortal.Model.Dto.JobDto;
using JobPortal.Model.Model;
using Jobportel.Data;
using Jobportel.Data.Infrastructure;
using Jobportel.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Data.Repositories.Jobs
{
    
    public class JobRepositry : Repository<Job>, IJobRepositry
    {
        public JobRepositry(Context  context):base(context)
        {
                
        }
        public async Task<IEnumerable<GetJobDto>> GetJobs(Pagination pagination)
        {
            var Jobs = await (from j in _contex.Job
                              join u in _contex.User on j.CreatedBy equals u.Id 
                              where j.CreatedBy == u.Id
                              select new GetJobDto
                              {
                                  Id = j.Id,
                                  Title=j.Title,
                                  Description=j.Description,
                                  CreatedByName = u.Name,
                                  JobEndAt =j.EndAt,
                                  IsActive=j.IsActive
                                 
                              }).OrderBy(x => x.Id)
                               
                                .ToListAsync();
            var count = Jobs.Count();
            if (pagination.PageSize == -1)
            {
                pagination.PageSize = count;
            }

            var result = Jobs.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                           .Take(pagination.PageSize);
            return result;

            
        }
    }
}
