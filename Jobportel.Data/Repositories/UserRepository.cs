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
        public async Task<IEnumerable<UserGetDto>> Getcandidate(Pagination pagination)
        {
            //var candidate = await (from u in _contex.User
            //                join r in _contex.Role on u.RoleId equals r.Id 
            //                where r.Id ==3
            //                select new UserGetDto
            //                   {
            //                       Id = u.Id,
            //                       Name = u.Name,
            //                       Email = u.Email,
            //                       RoleName = r.Name,
            //                       CreatedAt = u.CreatedAt,
            //                       IsActive = u.IsActive

            //                   }).OrderBy(x => x.Id)
            //                   .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            //                   .Take(pagination.PageSize)
            //                   .ToListAsync();
            //var result = new List<UserGetDto>();
            var candidate = await (from u in _contex.User
                                   join r in _contex.Role on u.RoleId equals r.Id
                                   where r.Id == 3
                                   select new UserGetDto
                                   {
                                       Id = u.Id,
                                       Name = u.Name,
                                       Email = u.Email,
                                       RoleName = r.Name,
                                       CreatedAt = u.CreatedAt,
                                       IsActive = u.IsActive

                                   })
                                   .OrderBy(x => x.Id)
                               .ToListAsync();
            var count = candidate.Count();
            if(pagination.PageSize == -1)
            {
                pagination.PageSize = count;
            }

            var result = candidate.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                           .Take(pagination.PageSize);
            return result;
        }

        public async Task<IEnumerable<UserGetDto>> Getrecruiter(Pagination pagination)
        {
            //var recruiter = await (from u in _contex.User
            //                       join r in _contex.Role on u.RoleId equals r.Id
            //                       where r.Id == 2
            //                       select new UserGetDto
            //                       {
            //                           Id = u.Id,
            //                           Name = u.Name,
            //                           Email = u.Email,
            //                           RoleName = r.Name,
            //                           CreatedAt = u.CreatedAt,
            //                           IsActive = u.IsActive

            //                       }).OrderBy(x => x.Id)
            //                   .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            //                   .Take(pagination.PageSize)
            //                   .ToListAsync();
            //return recruiter;
            var recruiter = await (from u in _contex.User
                                   join r in _contex.Role on u.RoleId equals r.Id
                                   where r.Id == 2
                                   select new UserGetDto
                                   {
                                       Id = u.Id,
                                       Name = u.Name,
                                       Email = u.Email,
                                       RoleName = r.Name,
                                       CreatedAt = u.CreatedAt,
                                       IsActive = u.IsActive

                                   }).OrderBy(x => x.Id)
                               .ToListAsync();
            var count = recruiter.Count();
            if (pagination.PageSize == -1)
            {
                pagination.PageSize = count;
            }

            var result = recruiter.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                           .Take(pagination.PageSize);
            return result;
           
        }


    }
}
