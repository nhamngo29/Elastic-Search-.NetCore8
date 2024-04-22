using Elastic.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elastic
{
    public interface IElasticService<T> where T : class
    {
        Task<CreateIndexResponse> CreateIndexAsync();
        Task DeleteIndexAsync();
        Task<GetIndexResponse> GetIndexAsync();
        Task<GetIndexResponse> GetAllIndicesAsync();
        Task AddDocumentAsync(T value);
        Task DeleteDocumentAsync(T value);
        Task UpsertDocumentAsync(T value);
        Task UpdateDocumentAsync(T value);
        Task<T> GetDocumentAsync(string id);
        Task<(long Count, IEnumerable<T> Documents)> GetDocumentsAsync(GridQueryModel gridQueryModel);
    }
}
