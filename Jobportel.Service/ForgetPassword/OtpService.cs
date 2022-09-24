using JobPortal.Data.Interfaces.Forgetpassword;
using JobPortal.Model.Model;
using System;
using System.Threading.Tasks;

namespace JobPortal.Service.ForgetPassword
{
    public class OtpService:IOtpService
    {
        private readonly IOtpRepositry _otpRepository;
        public OtpService(IOtpRepositry otp)
        {
            _otpRepository = otp;
        }

        public async Task<Otp> Add(Otp entity)
        {
            try
            {
                return await _otpRepository.Add(entity);
            }
            catch (Exception ex)
            {
              
            }
            return null;
        }

        public async Task<bool> IsOtpUnique(int otp)
        {
            var isUnique = await _otpRepository.GetDefault(x => x.OtpNumber == otp);
            return isUnique == null ? true : false;
        }

        public async Task<Otp> Validate(int otp)
        {
            try
            {
                return await _otpRepository.GetDefault(x => x.OtpNumber == otp && x.expiry >= DateTime.Now);
            }
            catch (Exception ex)
            {
               
            }
            return null;
        }
    }
}
