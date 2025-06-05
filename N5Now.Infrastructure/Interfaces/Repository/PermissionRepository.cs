using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using N5Now.Core.Entities;
using N5Now.Core.Interfaces.Repository;
using N5Now.Infrastructure.DataBaseIttion;

namespace N5Now.Infrastructure.Interfaces.Repository
{
    public class PermissionRepository : GenericRepository<Permissions>, IPermissionRepository
    {
        public PermissionRepository(ApplicationDbContext dBContext, ILogger logger):base(dBContext, logger)
        { 
        }

        public async Task<IEnumerable<Permissions>> GetAllPermissionsAsync()
        {
            try
            {
                _logger.LogInformation($"Try Get All {nameof(Permissions)} With {nameof(GetAllPermissionsAsync)}");
                return await _dBcontext.permissions.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Getting All {nameof(Permissions)} : {ex.Message}");
                throw new Exception($"Exception: {nameof(GetAllPermissionsAsync)}");
            }
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<Permissions>> GetAllPermissionsWhitTypesAsync()
        {
            try
            {
                _logger.LogInformation($"Try Get All {nameof(Permissions)} With Types {nameof(GetAllPermissionsAsync)}");
                return await _dBcontext.permissions.Include(p => p.PermissionType).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Getting All {nameof(Permissions)} With Types : {ex.Message}");
                throw new Exception($"Exception: {nameof(GetAllPermissionsAsync)}");
            }
            //throw new NotImplementedException();
        }
    }
}
