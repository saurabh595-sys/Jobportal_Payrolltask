using JobPortal.Model.Dto.UserDto;
using JobPortal.Model.Model;
using Jobportel.Data.Infrastructure;
using Jobportel.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jobportel.Data.Interfaces
{
    public interface IUserRepository :IRepository<User>
    {
        Task<IEnumerable<UserGetDto>> GetUsers(Pagination pagination);
    }
}
