using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using N5Now.Core.Entities;

namespace N5Now.Core.Interfaces.Repository
{
    public interface IPermissionRepository : IGenericRepository<Permissions>
    {
        Task<IEnumerable<Permissions>> GetAllPermissionsAsync();
        Task<IEnumerable<Permissions>> GetAllPermissionsWhitTypesAsync();
    }
}
