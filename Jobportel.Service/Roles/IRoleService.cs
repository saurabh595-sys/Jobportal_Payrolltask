using System.Collections.Generic;
using System.Threading.Tasks;
using JobPortal.Model.Model;
using Jobportel.Data.Model;


namespace JobPortal.Service.Roles
{
    public  interface IRoleService
    {
        Task<IEnumerable<Role>> GetAll(Pagination pagination);
        Task<Role> GetById(int id);
        Task<Role> Add(Role role);
        Task<Role> Update(Role role);
        Task<bool> Delete(int id);
    }
}
