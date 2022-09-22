using JobPortal.Model.Dto.UserDto;
using JobPortal.Model.Model;
using Jobportel.Data.Infrastructure;
using Jobportel.Data.Interfaces;
using Jobportel.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobportel.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(Context contex) : base(contex)
        {

        }

        public async Task<IEnumerable<UserGetDto>> GetUsers(Pagination pagination)
        {
            var users = await (from u in _contex.User
                               join r in _contex.Role on u.RoleId equals r.Id
                               select new UserGetDto
                               {
                                   Id = u.Id,
                                   Name = u.Name,
                                   Email = u.Email,
                                   RoleName = r.Name,
                                   CreatedAt = u.CreatedAt,
                                   IsActive=u.IsActive

                               }).OrderBy(x => x.Id)
                               .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                               .Take(pagination.PageSize)
                               .ToListAsync();
            return users;
        }
    }
}
