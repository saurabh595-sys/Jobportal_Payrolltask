using JobPortal.Api.Dto;
using JobPortal.Data.Repositories.Jobs;
using Jobportel.Data;
using Jobportel.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using JobPortal.Model.Model;
using JobPortal.Model.Dto.JobDto;

namespace JobPortal.Service.Jobs
{
    public class JobService : IJobService
    {
        public readonly IJobRepositry _jobRepositry;
        public readonly Context _context;
        public JobService(IJobRepositry job,Context context)
        {
            _jobRepositry = job;
            _context = context;
        }

        public async Task<Job> Add(Job job)
        {
          return await _jobRepositry.Add(job);
        }

        public async Task<IEnumerable<JobApplied>> AppliedJobs(int id)
        {
            var JobApplied = await(from j in _context.Job
                                   join a in _context.Applicant on j.Id equals a.jobId
                                   join u in _context.User on a.AppliedBy equals u.Id
                                   where j.CreatedBy == id
                                  select new JobApplied
                                  {
                                      CandidateId = u.Id,
                                      CandidateName = u.Name,
                                      JobTitle = j.Title,
                                      Description = j.Description,
                                      AppliedAt = a.AppliedAt
                                  }).ToListAsync();
            return JobApplied;
        }

        public async Task<bool> Delete(int id)
        {
            Job job = await GetById(id);
            if (job != null) { await _jobRepositry.Delete(job); return true; } else { return false; }

        }

        public async Task<IEnumerable<GetJobDto>> GetAll(Pagination pagination)
        {
            return await _jobRepositry.GetJobs(pagination);
        }

        public async Task<Job> GetById(int id)
        {
            return await _jobRepositry.GetById(id);
        }

        public async Task<Job> Update(Job job)
        {
            Job  jobs = await _jobRepositry.GetById(job.Id);
            if (jobs != null)
            {
                jobs.Title = job.Title;
                jobs.CreatedAt = job.CreatedAt;
                jobs.CreatedBy = job.CreatedBy;
                jobs.Description = job.Description;
                jobs.EndAt = jobs.EndAt;
                await _jobRepositry.Update(jobs);
                return jobs;
            }
            return jobs;

        }

 
    }
}
