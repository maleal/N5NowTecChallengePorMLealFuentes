using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using N5Now.Core.Interfaces;
using N5Now.Core.Interfaces.Repository;
using N5Now.Infrastructure.DataBaseIttion;
using N5Now.Infrastructure.Interfaces.Repository;

namespace N5Now.Infrastructure.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        #region -- Constructor --
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _dbContext;
        
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPermissionTypeRepository _permissionTypeRepository;

        public UnitOfWork(ApplicationDbContext dbContext, ILoggerFactory logger)
        {
            _dbContext = dbContext;
            _logger = logger.CreateLogger("Logs");
        }
        #endregion

        public IPermissionRepository PermissionRepository
        {
            get
            {
                if (_permissionRepository != null) { return _permissionRepository; }
                else { return new PermissionRepository(_dbContext, _logger); }
            }
        }

        public IPermissionTypeRepository PermissionTypeRepository
        {
            get
            {
                if (_permissionTypeRepository != null) { return _permissionTypeRepository; }
                else { return new PermissionTypeRepository(_dbContext, _logger); }
            }
        }

        #region -- IDisposable --
        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }
        #endregion
        public Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
