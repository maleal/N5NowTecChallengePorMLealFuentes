using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using N5Now.Core.Entities;

namespace N5Now.Core.Interfaces.Indexer
{
    public interface IPermissionsIndexer
    {
        Task IndexAsync(PermissionsDocument document);
    }
}
