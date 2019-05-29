using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CosmosDB
{
    class AzureCosmosDbRepository : IDbRepository
    {
        #region private methods

        private void SetupDocumentDbClient(string endpoint, string authKey)
        {
            _documentClient = new DocumentClient(new Uri(endpoint), authKey);
        }

        #endregion

        #region initialization

        internal readonly FeedOptions DefaultOptions = new FeedOptions { EnableCrossPartitionQuery = true };
        private readonly string _databaseId;
        private DocumentClient _documentClient;

        public AzureCosmosDbRepository(string endpoint, string authKey, string databaseId)
        {
            _databaseId = databaseId;

            SetupDocumentDbClient(endpoint, authKey);
        }

        public void Dispose()
        {
            _documentClient?.Dispose();
        }

        #endregion

        #region service methods

        public async Task<TValue> GetEntityAsync<TValue>(string id, string collectionName)
        {
            var document = await _documentClient.ReadDocumentAsync(
                UriFactory.CreateDocumentUri(_databaseId, collectionName, id), new RequestOptions());

            return (dynamic)document.Resource;
        }

        public async Task<IEnumerable<TValue>> GetAllEntitiesAsync<TValue>(string collectionName)
        {
            var result = await _documentClient.ReadDocumentFeedAsync(
                UriFactory.CreateDocumentCollectionUri(_databaseId, collectionName), DefaultOptions);
            return (dynamic)result;
        }

        public async Task<IEnumerable<TValue>> GetEntitiesByPredicateAsync<TValue>(string collectionName,
            Expression<Func<TValue, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var results = new List<TValue>();
            var query =
                _documentClient
                    .CreateDocumentQuery<TValue>(UriFactory.CreateDocumentCollectionUri(_databaseId, collectionName),
                        DefaultOptions)
                    .Where(predicate)
                    .AsDocumentQuery();

            while (query.HasMoreResults)
                results.AddRange(await query.ExecuteNextAsync<TValue>());

            return results;
        }

        public async Task<TValue> CreateEntityAsync<TValue>(string collectionName, TValue entity)
        {
            var result = await _documentClient.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(_databaseId, collectionName), entity);

            return (dynamic)result.Resource;
        }

        public async Task<TValue> UpdateEntityAsync<TValue>(string collectionName, TValue entity)
        {
            var result = await _documentClient.ReplaceDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(_databaseId, collectionName), entity);

            return (dynamic)result.Resource;
        }

        public async Task<TValue> UpsertEntityAsync<TValue>(string collectionName, TValue entity)
        {
            var result = await _documentClient.UpsertDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(_databaseId, collectionName), JObject.FromObject(entity));

            return (dynamic)result.Resource;
        }

        public async Task<IEnumerable<TValue>> GetEntitiesBySqlAsync<TValue>(string collectionName, string sqlQuery)
        {
            if (string.IsNullOrWhiteSpace(sqlQuery))
                throw new ArgumentNullException(nameof(sqlQuery));

            var results = new List<TValue>();
            var query =
                _documentClient
                    .CreateDocumentQuery<TValue>(
                        UriFactory.CreateDocumentCollectionUri(_databaseId, collectionName),
                        sqlQuery,
                        DefaultOptions)
                    .AsDocumentQuery();

            while (query.HasMoreResults)
                results.AddRange(await query.ExecuteNextAsync<TValue>());

            return results;
        }

        #endregion
    }
}

