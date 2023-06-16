using Refit;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MovieTimeApp.Services
{
	public interface IMoviesApi
    {
        [Get("/discover/movie?api_key={apiKey}&sort_by=popularity.desc")]
        Task<HttpResponseMessage> GetAllMovies(string apiKey, CancellationToken cancellationToken);

        [Get("/genre/movie/list?api_key={apiKey}")]
        Task<HttpResponseMessage> GetGenres(string apiKey, CancellationToken cancellationToken);
    }
}
