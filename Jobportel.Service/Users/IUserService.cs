using JobPortal.Model.Dto.UserDto;
using JobPortal.Model.Model;
using Jobportel.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jobportel.Service.Users
{
    public interface IUserService
    {
        Task<IEnumerable<UserGetDto>> GetAll(Pagination pagination);
        Task<User> GetById(int id);
        Task<User> Add(UserAddDto Users);
        Task<User> Update(User Users);
        Task<bool> Delete(int id);
        Task<User> GetUser(string email, string password);
        Task<bool> ForgotPassword(string email);
        Task<User> ResetPassword(int otp, string newPassword, string confirmPassword);

    }
}
