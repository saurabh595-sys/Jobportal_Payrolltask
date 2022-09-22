using JobPortal.Api.Dto;
using Jobportel.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service.Applicants
{
    public interface IApplicantService
    {
        Task<IEnumerable<Applicant>> GetAll();
        Task<IEnumerable<JobApplied>> AppliedJobs(int id);
        Task<Applicant> GetById(int id);
        Task<Applicant> Add(Applicant applicant);
        Task<Applicant> Update(Applicant applicant);
        Task<bool> Delete(int id);
    }
}
