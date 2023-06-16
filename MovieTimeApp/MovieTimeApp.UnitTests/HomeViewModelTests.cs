using FakeItEasy;
using MovieTimeApp.Helpers;
using MovieTimeApp.Models;
using MovieTimeApp.Services;
using MovieTimeApp.ViewModels;
using Prism.Navigation;
using Prism.Services;

namespace MovieTimeApp.UnitTests
{
	public class HomeViewModelTests
	{
        [Fact]
        public void GivenHomeViewModel_WhenOnAppearing_ThenShouldGetAllMovies()
        {
            // Arrange
            var moviesService = A.Fake<IMoviesService>();
            var navigationService = A.Fake<INavigationService>();
            var dialogService = A.Fake<IPageDialogService>();

            var sut = new HomeViewModel(navigationService, moviesService, dialogService);

            // Act
            sut.OnAppearing();

            // Assert

            A.CallTo(() => moviesService.GetAllMovies())
             .MustHaveHappenedOnceExactly();

        }

        [Fact]
        public void GivenHomeViewModel_WhenGoToMovieDetailsCommandExecutes_ThenShouldNavigateToDetails()
        {
            // Arrange
            var moviesService = A.Fake<IMoviesService>();
            var navigationService = A.Fake<INavigationService>();
            var diaglogService = A.Fake<IPageDialogService>();
            var movie = new Movie
            {
                Id = 1
            };
            var sut = new HomeViewModel(navigationService, moviesService, diaglogService);

            // Act
            sut.GoToMovieDetailsCommand.Execute(movie);

            // Assert

            A.CallTo(() => navigationService.NavigateAsync(NavigationConstants.Detail, A<INavigationParameters>.That.IsSameSequenceAs(new NavigationParameters
            {
                { NavigationParamConstant.Movie, movie }
            })))
             .MustHaveHappenedOnceExactly();

        }
    }
}

