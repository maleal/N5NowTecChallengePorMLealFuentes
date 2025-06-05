using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using N5Now.Core.Entities;
using N5Now.Core.Interfaces.Repository;
using N5Now.Infrastructure.DataBaseIttion;

namespace N5Now.Infrastructure.Interfaces.Repository
{
    public class PermissionTypeRepository : GenericRepository<PermissionType>, IPermissionTypeRepository
    {
        public PermissionTypeRepository(ApplicationDbContext dBContext, ILogger logger) : 
            base(dBContext, logger) { }
    }
}
