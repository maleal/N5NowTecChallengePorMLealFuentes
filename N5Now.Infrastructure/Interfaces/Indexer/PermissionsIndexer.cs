using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using N5Now.Core.Entities;
using N5Now.Core.Interfaces.Indexer;
using Nest;

namespace N5Now.Infrastructure.Interfaces.Indexer
{
    public class PermissionsIndexer : IPermissionsIndexer
    {
        private readonly IElasticClient _elasticClient;

        public PermissionsIndexer(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task IndexAsync(PermissionsDocument document)
        {
            var response = await _elasticClient.IndexDocumentAsync(document);
            if (!response.IsValid)
            {
                // Podés loguear el error, lanzar excepción o continuar según tu lógica
                throw new Exception($"Failed to index permission: {response.OriginalException?.Message}");
            }
        }
    }
}
