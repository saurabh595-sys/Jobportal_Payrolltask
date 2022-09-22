using JobPortal.Data.Interfaces.Applicants;
using Jobportel.Data;
using Jobportel.Data.Infrastructure;
using Jobportel.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Data.Repositories.Applicants
{
   public class ApplicantRepository: Repository<Applicant>, IApplicantRepository
    {
        public ApplicantRepository(Context context) : base(context)
        {

        }

        //public async Task <string> getApplicantEmail()
        //{
        //    var Applicantemail =from a in _contex.Applicant.

        //    return Applicantemail;
        //}

    }
}
