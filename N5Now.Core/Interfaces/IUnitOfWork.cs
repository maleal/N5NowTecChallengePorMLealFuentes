using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using N5Now.Core.Interfaces.Repository;

namespace N5Now.Core.Interfaces
{
    /// <summary>
    ///     El patrón Unit of Work (UoW) coordina el trabajo de múltiples repositorios bajo una misma
    ///     transacción y contexto de base de datos. Su función principal es:
    ///         Agrupar cambios en entidades y asegurarse de que se apliquen todos juntos(o ninguno), 
    ///         a través de una única llamada a SaveChangesAsync().
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IPermissionRepository PermissionRepository { get; }
        IPermissionTypeRepository PermissionTypeRepository { get; }

        Task SaveChangesAsync();
        Task<int> CompleteAsync();
    }
}
