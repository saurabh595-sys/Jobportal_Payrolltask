using DevRequired.Service;
using JobPortal.Model.Dto.UserDto;
using JobPortal.Model.Model;
using JobPortal.Service.ForgetPassword;
using Jobportel.Data.Interfaces;
using Jobportel.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jobportel.Service.Users
{
    public class UserService :IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOtpService _otpService;
        private readonly IEmailService _emailService;
        private static Random _random = new Random();
        public UserService(IUserRepository userRepositry, IOtpService otp,IEmailService email)
        {
            _userRepository = userRepositry;
            _otpService = otp;
            _emailService = email;
        }


        public async Task<User> Add(User u)
        {
            User Users = u;
            Users.Password = BCrypt.Net.BCrypt.HashPassword(u.Password);
            return await _userRepository.Add(Users);
        }

        public async Task<bool> Delete(int id)
        {
            User user = await GetById(id);
            if (user != null) { await _userRepository.Delete(user); return true; } else { return false; }

        }

        public async Task<IEnumerable<UserGetDto>> GetAll(Pagination pagination)
        {
            return await _userRepository.GetUsers(pagination);
            
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<User> Update(User Users)
        {
            User user = await _userRepository.GetById(Users.Id);
            if(user != null)
            {
                user.Name = Users.Name;
                user.Password = Users.Password;
                user.Email = Users.Email;
                await _userRepository.Update(user);
                return user;
            }
                return user;
            
        }


        public async Task<User> GetUser(string email, string password)
        {
            try
            {
                var user =await  _userRepository.GetDefault(x => x.Email == email);
                if (user != null)
                {
                    
                    if(!BCrypt.Net.BCrypt.Verify(password,user.Password))
                    {
                        return null;
                    }
                    return user;
                }
                return user;
            }
            catch (Exception ex)
            {
                
            }
            return null;
        }

        public async Task<bool> ForgotPassword(string email)
        {
            try
            {
                StringBuilder body = new StringBuilder();
                //var body = "";
                var user = await GetUserByMail(email);
                if (user != null)
                {
                regenerate:
                    var otp = Convert.ToInt32(GenerateRandomNo());
                    var isUnique = await _otpService.IsOtpUnique(otp);
                    if (!isUnique)
                        goto regenerate;
                    var to = user.Email;
                    var sub = "OTP";
                    var emailBody = body;
                    body.Append("<h3>Job Portal</h3>");
                    body.AppendLine($"<h4 style='font-size:1.1em'>Hi, {user.Name}</h4>");
                    body.AppendLine("<h5>For Reseting your password, OTP is valid for 10 minutes</h5>");
                    body.AppendLine($"<h2 style='background: #00466a;margin: 0 auto;width: max-content;padding: 0 10px;color: #fff;border-radius: 4px;'>{otp}</h2>");

                    var userOtp = new Otp();
                    userOtp.OtpNumber = Convert.ToInt32(otp);
                    userOtp.CreatedBy = user.Id;
                    userOtp.CreateDate = DateTime.Now;
                    userOtp.expiry = DateTime.Now.AddMinutes(10);

                    await _otpService.Add(userOtp);
                    await _emailService.SendEmailAsync(to, body, sub, "","");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                
            }
            return false;
        }

        public async Task<User> ResetPassword(int otp, string newPassword, string confirmPassword)
        {
            try
            {
                var details = await ValidateOtp(otp);
                var updateUser = new User();
                if (details != null)
                {
                    updateUser = await _userRepository.GetById(details.CreatedBy);
                    if (newPassword == confirmPassword)
                    {
                        updateUser.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                       await _userRepository.Update(updateUser);
                        return updateUser;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
               
            }
            return null;
        }

        private async Task<Otp> ValidateOtp(int otp)
        {
            return await _otpService.Validate(otp);
        }

        public async Task<User> GetUserByMail(string email)
        {
            try
            {
                var user = await _userRepository.GetDefault(x => x.Email == email);
                if (user != null)
                {
                    return user;
                }
                return user;
            }
            catch (Exception ex)
            {
               
            }
            return null;
        }

       
        private static string GenerateRandomNo()
        {
            return _random.Next(0, 999999).ToString("D6");
        }

       
    }
}
