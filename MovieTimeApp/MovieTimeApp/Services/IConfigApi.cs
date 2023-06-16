using Refit;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MovieTimeApp.Services
{
    public interface IConfigApi
    {
        [Get("/configuration?api_key={apiKey}")]
        Task<HttpResponseMessage> GetConfigs(string apiKey, CancellationToken cancellationToken);
    }
}

