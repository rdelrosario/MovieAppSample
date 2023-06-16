using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MovieTimeApp.Helpers;
using MovieTimeApp.Models;
using MovieTimeApp.Services;
using Prism.AppModel;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace MovieTimeApp.ViewModels
{
    public class HomeViewModel : BaseViewModel, IPageLifecycleAware
    {
        public ObservableCollection<Grouping<string, Movie>> Movies { get; private set; }
        public DelegateCommand<Movie> GoToMovieDetailsCommand { get; }
        public DelegateCommand GetMoviesCommand { get; }
        private IMoviesService _moviesService;

        public HomeViewModel(INavigationService navigationService, IMoviesService moviesService, IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
            _moviesService = moviesService;

            GetMoviesCommand = new DelegateCommand(async () => await GetMovies());
            GoToMovieDetailsCommand = new DelegateCommand<Movie>(async (param) => await GoToMovieDetails(param));
        }

        private async Task GetMovies()
        {
            var result = await RunSafe(_moviesService.GetAllMovies());

            if (result.SuccessResult)
            {
                var genresName = await GetGenresName();

                if (genresName?.Count > 0)
                {
                    var sorted = result.Result.Results.SelectMany(movie => movie.GenreIds, (movie, genreId)
                        => (Movie: movie, GenreId: genreId))
                        .GroupBy(x => x.GenreId)
                        .Join(genresName,
                              x => x.Key,
                              x => x.Id,
                              (moviesGroup, genre) => (moviesGroup, genre))
                        .Select(group => new Grouping<string, Movie>(group.genre.Name, group.moviesGroup.Select(x => x.Movie)));

                    Movies = new ObservableCollection<Grouping<string, Movie>>(sorted);
                }
            }
            else
            {
                var error = result.Errors.FirstOrDefault();
                if (error != null)
                    await PageDialogService.DisplayAlertAsync(AppResources.ErrorMessage, error, AppResources.Ok);
            }
        }

        private async Task<List<Genre>> GetGenresName()
        {
            var result = await RunSafe(_moviesService.GetGenres());
            if (result.SuccessResult)
            {
                return result.Result.Genres;
            }

            return null;
        }

        private async Task GoToMovieDetails(Movie movie)
        {
            await NavigationService.NavigateAsync(NavigationConstants.Detail, new NavigationParameters()
                            {
                                { NavigationParamConstant.Movie, movie}
                            });
        }

        public void OnAppearing()
        {
            GetMoviesCommand.Execute();
        }

        public void OnDisappearing()
        {
           
        }
    }
}

