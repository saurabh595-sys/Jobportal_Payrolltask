using JobPortal.Data.Interfaces.Forgetpassword;
using JobPortal.Model.Model;
using Jobportel.Data;
using Jobportel.Data.Infrastructure;


namespace JobPortal.Data.Repositories.ForgetPassword
{
    public class OtpRepository : Repository<Otp>, IOtpRepositry
    {
        public OtpRepository(Context context) : base(context)
        {

        }
    }
}
