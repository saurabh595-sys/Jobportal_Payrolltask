using DevRequired.Service;
using JobPortal.Api.Dto;
using JobPortal.Data.Interfaces.Applicants;
using JobPortal.Data.Repositories.Jobs;
using JobPortal.Service.Applicants;
using Jobportel.Data;
using Jobportel.Data.Interfaces;
using Jobportel.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service.Applicants
{
   public class ApplicantService: IApplicantService
    {
        public readonly IApplicantRepository _applicantRepository;
        public readonly IJobRepositry _jobRepositry;
        public readonly IUserRepository _userRepository;
        public readonly IEmailService _mailservice;
        public readonly Context _context;
        public ApplicantService(IApplicantRepository applicant, IJobRepositry jobRepositry , IUserRepository userRepository, IEmailService emailService, Context context)
        {
            _applicantRepository = applicant;
            _jobRepositry = jobRepositry;
            _userRepository = userRepository;
            _mailservice = emailService;
            _context = context;
        }

        public async Task<Applicant> Add(Applicant applicant)
        {
            var Applicant = await _userRepository.GetById(applicant.AppliedBy);

            if (Applicant != null)
            {
                var job = await _jobRepositry.GetById(applicant.jobId);
                if (job != null)
                {
                    var recruiter = await _userRepository.GetById(job.CreatedBy);
                    StringBuilder Rmail = new StringBuilder();
                    Rmail.Append($"<p>JobName:{job.Title}</p>");
                    Rmail.Append($"<p>ApplicantName:{Applicant.Name}</p>");
                    await _mailservice.SendEmailAsync(recruiter.Email, Rmail, "Recruiter", "", "");

                    StringBuilder AMail = new StringBuilder();
                    AMail.Append($"<p>Applied job Success</p>");
                    AMail.Append($"<p>Applyed job :{job.Title}</p>");
                    await _mailservice.SendEmailAsync(Applicant.Email, AMail, "Applicant", "", "");

                }
            }

            return await _applicantRepository.Add(applicant);
        }

        public async Task<IEnumerable<JobApplied>> AppliedJobs(int id)
        {
            var AppliedJobs = await (from u in _context.User
                                   join a in _context.Applicant on u.Id equals a.AppliedBy
                                   join j in _context.Job on a.jobId equals j.Id 
                                   where u.Id==id
                                   select new JobApplied
                                   {
                                       CandidateId = u.Id,
                                       CandidateName = u.Name,
                                       JobTitle = j.Title,
                                       Description = j.Description,
                                       AppliedAt = a.AppliedAt
                                   }).ToListAsync();

            return AppliedJobs;
        }

        public async Task<bool> Delete(int id)
        {
            Applicant applicant = await GetById(id);
            if (applicant != null) { await _applicantRepository.Delete(applicant); return true; } else { return false; }

        }

        public async Task<IEnumerable<Applicant>> GetAll()
        {
            return await _applicantRepository.Get();
        }

        public async Task<Applicant> GetById(int id)
        {
            return await _applicantRepository.GetById(id);
        }

        public async Task<Applicant> Update(Applicant applicant)
        {
            Applicant applicants = await _applicantRepository.GetById(applicant.Id);
            if (applicants != null)
            {
                applicants.jobId = applicant.jobId;
                applicants.AppliedAt = applicant.AppliedAt;
                
                await _applicantRepository.Update(applicant);
                return applicant;
            }
            return applicant;
        }
    }
}
