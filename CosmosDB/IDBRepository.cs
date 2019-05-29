using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDB
{

        public interface IDbRepository : IDisposable
        {
            Task<TValue> GetEntityAsync<TValue>(string id, string collectionName);

            Task<IEnumerable<TValue>> GetAllEntitiesAsync<TValue>(string collectionName);

            Task<IEnumerable<TValue>> GetEntitiesByPredicateAsync<TValue>(string collectionName, Expression<Func<TValue, bool>> predicate);

            Task<TValue> CreateEntityAsync<TValue>(string collectionName, TValue entity);

            Task<TValue> UpdateEntityAsync<TValue>(string collectionName, TValue entity);

            Task<TValue> UpsertEntityAsync<TValue>(string collectionName, TValue entity);

            Task<IEnumerable<TValue>> GetEntitiesBySqlAsync<TValue>(string collectionName, string sqlQuery);
        }
    }
