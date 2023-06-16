using System;
using FakeItEasy;
using MovieTimeApp.Helpers;
using MovieTimeApp.Models;
using MovieTimeApp.Services;
using MovieTimeApp.ViewModels;
using Prism.Navigation;
using Prism.Services;

namespace MovieTimeApp.UnitTests
{
	public class DetailsViewModelTests
	{
        [Fact]
        public void GivenDetailsViewModel_WhenInitializes_ThenMovieShouldBeAsExpected()
        {
            // Arrange
            var navigationService = A.Fake<INavigationService>();
            var dialogService = A.Fake<IPageDialogService>();
            var movie = new Movie
            {
                Id = 1
            };

            var sut = new DetailsViewModel(navigationService, dialogService);

            var parameters = new NavigationParameters
            {
                { NavigationParamConstant.Movie, movie }
            };

            // Act
            sut.Initialize(parameters);

            // Assert
            Assert.Equal(sut.Movie, movie);

        }
    }
}

