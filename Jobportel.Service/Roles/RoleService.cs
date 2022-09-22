using Jobportel.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jobportel.Data.Interfaces;
using JobPortal.Data.Interfaces.Roles;
using JobPortal.Model.Model;

namespace JobPortal.Service.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository role)
        {
            _roleRepository = role;
        }
        public async Task<Role> Add(Role role)
        {
            return await _roleRepository.Add(role);
        }

        public async Task<bool> Delete(int id)
        {
            Role role = await GetById(id);
            if (role != null) { await _roleRepository.Delete(role); return true; } else { return false; }

        }

        public async Task<IEnumerable<Role>> GetAll(Pagination pagination)
        {
          
            return await _roleRepository.GetRoles(pagination);

        }

        public async Task<Role> GetById(int id)
        {
            return await _roleRepository.GetById(id);
        }

        public async Task<Role> Update(Role r)
        {
            Role role = await _roleRepository.GetById(r.Id);
            if (role != null)
            {
                role.Name = r.Name;
               
                
                await _roleRepository.Update(role);
                return role;
            }
            return role;

        }
    }
}
