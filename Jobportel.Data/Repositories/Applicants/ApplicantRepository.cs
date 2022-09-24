using JobPortal.Data.Interfaces.Applicants;
using Jobportel.Data;
using Jobportel.Data.Infrastructure;
using Jobportel.Data.Model;

namespace JobPortal.Data.Repositories.Applicants
{
    public class ApplicantRepository: Repository<Applicant>, IApplicantRepository
    {
        public ApplicantRepository(Context context) : base(context)
        {

        }
    }
}
