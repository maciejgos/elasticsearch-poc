using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentsService.Api.Configuration;
using DocumentsService.Api.Models;
using Microsoft.Extensions.Options;
using Nest;

namespace DocumentsService.Api.Services
{
    public class DocumentsService
    {
        private readonly ElasticClient _client;

        public DocumentsService(IOptions<ElasticsearchConfiguration> configuration)
        {
            var node = new Uri(configuration.Value.ClusterUrl);
            var settings = new ConnectionSettings(node).DefaultIndex(configuration.Value.DefaultIndex);
            _client = new ElasticClient(settings);
        }

        public async Task<IReadOnlyCollection<Document>> Search(string term)
        {
            var request = new SearchRequest
            {
                From = 0,
                Size = 10,
                Query = new QueryStringQuery { DefaultField = "attachment.content", Query = term }
            };

            var response = await _client.SearchAsync<Document>(request);
            return response.Documents;
        }
    }
}