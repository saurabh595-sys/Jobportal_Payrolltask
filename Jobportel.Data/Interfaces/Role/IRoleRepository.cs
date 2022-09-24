using JobPortal.Model.Model;
using Jobportel.Data.Infrastructure;
using Jobportel.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobPortal.Data.Interfaces.Roles
{
    public interface IRoleRepository: IRepository<Role>
    {
        Task<IEnumerable<Role>> GetRoles(Pagination pagination);
    }

   

}
