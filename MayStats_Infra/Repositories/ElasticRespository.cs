using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using MyStats_Rest.Models;
using System.Net;
using System.Text.RegularExpressions;

namespace MayStats_Infra.Repositories
{
    public class ElasticRespository
    {
        private readonly ElasticsearchClient _client;

        public ElasticRespository(string url)
        {
            string username, password;
            username = "elastic";
            password = "changeme";


            var options = new ElasticsearchClientSettings(new Uri("https://localhost:9200"))
                             .ServerCertificateValidationCallback(CertificateValidations.AllowAll)
                            .Authentication(new BasicAuthentication(username, password));
            _client = new ElasticsearchClient(options);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // or Tls13 if supported

        }
        private string ConvertToHyphenated(string input)
        {
            // Use regex to insert hyphens between words
            string result = Regex.Replace(input, "(?<!^)([A-Z])", "-$1").ToLower();
            return result;
        }
        // Create or Index a document
        public async Task CreateAsync(Stats document)
        {
            try
            {
                var response = await _client.IndexAsync(document, idx => idx.Index("record-entry-index"));

                if (response.IsValidResponse)
                {
                    Console.WriteLine($"Document indexed successfully with ID: {response.Id}");
                }
                else
                {
                    Console.WriteLine($"Error indexing document: {response.Result}");
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        // Get a document by Id
        //public async Task<T> GetAsync<T>(string id, string indexName) where T : class
        //{
        //    var response = await _client.GetAsync<T>(id, g => g.Index(indexName));
        //    if (!response.IsValid)
        //    {
        //        Console.WriteLine($"Error retrieving document: {response.OriginalException.Message}");
        //        return null;
        //    }
        //    return response.Source;
        //}

        //// Update a document by Id
        //public async Task<UpdateResponse<T>> UpdateAsync<T>(string id, T document, string indexName) where T : class
        //{
        //    var response = await _client.UpdateAsync<T>(id, u => u.Index(indexName).Doc(document));
        //    if (!response.IsValid)
        //    {
        //        Console.WriteLine($"Error updating document: {response.OriginalException.Message}");
        //    }
        //    return response;
        //}

        //// Delete a document by Id
        //public async Task<DeleteResponse> DeleteAsync<T>(string id, string indexName) where T : class
        //{
        //    var response = await _client.DeleteAsync<T>(id, d => d.Index(indexName));
        //    if (!response.IsValid)
        //    {
        //        Console.WriteLine($"Error deleting document: {response.OriginalException.Message}");
        //    }
        //    return response;
        //}

        //// Search documents
        //public async Task<ISearchResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, ISearchRequest> searchDescriptor) where T : class
        //{
        //    var response = await _client.SearchAsync(searchDescriptor);
        //    if (!response.IsValid)
        //    {
        //        Console.WriteLine($"Error searching documents: {response.OriginalException.Message}");
        //    }
        //    return response;
        //}
    }
}
