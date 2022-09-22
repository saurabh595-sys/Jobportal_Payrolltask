using JobPortal.Data.Interfaces.Roles;
using JobPortal.Model.Model;
using Jobportel.Data;
using Jobportel.Data.Infrastructure;
using Jobportel.Data.Interfaces;
using Jobportel.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Data.Repositories.Roles
{
    public class RoleRepository: Repository<Role>, IRoleRepository
    {
        public RoleRepository(Context contex) : base(contex)
        {

        }
        public async Task<IEnumerable<Role>> GetRoles(Pagination pagination)
        {
            var roles = await (from r in _contex.Role
                               select new Role
                               {
                                  Id=r.Id,
                                  Name=r.Name

                               }).OrderBy(x => x.Id)
                               .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                               .Take(pagination.PageSize)
                               .ToListAsync();
            return roles;
        }
    }
}
