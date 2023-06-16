using System.Threading;
using System.Threading.Tasks;
using MovieTimeApp.Helpers;
using MovieTimeApp.Models.Responses;

namespace MovieTimeApp.Services
{
	public class MoviesService : BaseService, IMoviesService
    {
        private readonly IApiClient<IMoviesApi> _moviesApi;
        private readonly IConfig _config;

        public MoviesService(IApiClient<IMoviesApi> moviesApi, IConfig config) 
        {
            _moviesApi = moviesApi;
            _config = config;
        }

        public async Task<Response<MoviesResponse>> GetAllMovies()
        {
            var response = await RemoteRequestAsync<MoviesResponse>(_moviesApi.Client.GetAllMovies(_config.ApiKey, new CancellationTokenSource().Token));
            return response;
        }

        public async Task<Response<GenreResponse>> GetGenres()
        {
            var response = await RemoteRequestAsync<GenreResponse>(_moviesApi.Client.GetGenres(_config.ApiKey, new CancellationTokenSource().Token));
            return response;
        }
    }
}