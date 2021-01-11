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
        private readonly string _indexName;

        public DocumentsService(IOptions<ElasticsearchConfiguration> configuration)
        {
            _indexName = configuration.Value.DefaultIndex;
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

        public async Task<bool> IndexDocument(string path)
        {
            var fileInfo = new System.IO.FileInfo(path);
            var base64File = Convert.ToBase64String(System.IO.File.ReadAllBytes(path));
            var document = new Document
            {
                Filename = fileInfo.Name,
                Attachment = new Attachment
                {
                    Content = base64File
                }
            };

            var response = await _client.IndexAsync(document, idx => idx.Index(_indexName));
            return response.Result == Nest.Result.Created || response.Result == Nest.Result.Updated;
        }
    }
}