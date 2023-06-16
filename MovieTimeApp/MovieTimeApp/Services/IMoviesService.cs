using System.Threading.Tasks;
using MovieTimeApp.Models.Responses;

namespace MovieTimeApp.Services
{
    public interface IMoviesService
    {
        Task<Response<MoviesResponse>> GetAllMovies();
        Task<Response<GenreResponse>> GetGenres();
    }
}
