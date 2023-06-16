using MovieTimeApp.Helpers;
using MovieTimeApp.Models;
using Prism.Navigation;
using Prism.Services;

namespace MovieTimeApp.ViewModels
{
    public class DetailsViewModel : BaseViewModel, IInitialize
    {
        public Movie Movie { get; private set; }
        public DetailsViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService) { }

        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.TryGetValue(NavigationParamConstant.Movie, out Movie movie))
            {
                Movie = movie;
            }
        }
    }
}

