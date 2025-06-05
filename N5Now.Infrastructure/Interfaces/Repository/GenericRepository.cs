using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using N5Now.Core.Interfaces.Repository;
using N5Now.Infrastructure.DataBaseIttion;

namespace N5Now.Infrastructure.Interfaces.Repository
{
    /// <summary>
    /// Notas:
    ///     Implementamos el contrato genérico base (IRepository<T>) 
    ///     para tener operaciones mínimas reutilizables.
    ///
    ///     Si necesitás lógica personalizada (como con ..., etc)
    ///     extendés en el repositorio específico ...
    ///     (esta implementacion No estás obligado a usarla, pero está disponible por si lo necesitás.)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ILogger _logger;
        protected readonly ApplicationDbContext _dBcontext;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext dbContext, ILogger logger)
        {
            _dBcontext = dbContext;
            _dbSet = _dBcontext.Set<T>();
            _logger = logger;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    $"Error getting All entities of {typeof(T).Name}: {ex.Message}");
                throw new Exception("Error: GetAllAsync()");
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    $"Error getting entity by Id of{typeof(T).Name}: {ex.Message}");
                throw new Exception("Error: GetByIdsync()");
            }
        }

        public void UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Updating entity");
                throw new Exception($"Error al actualizar registro de {typeof(T).Name}: {ex.Message}", ex);
            }
        }

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    $"Error adding entity {typeof(T).Name}: {ex.Message}");
                return false;
            }
        }

        public void DeleteAsync(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Removing entity");
                throw new Exception($"Error al eliminar un registro de {typeof(T).Name}: {ex.Message}", ex);
            }
        }
    }
}
