using MovieTimeApp.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Essentials;
using System.ComponentModel;
using Prism.Services;

namespace MovieTimeApp.ViewModels
{
    public abstract class BaseViewModel : BindableBase, INotifyPropertyChanged
    {
        protected INavigationService NavigationService { get; }
        protected IPageDialogService PageDialogService { get; }
        public bool IsBusy { get; private set; }

        public BaseViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            NavigationService = navigationService;
            PageDialogService = pageDialogService;
        }

        protected async Task<Response<T>> RunSafe<T>(Task<Response<T>> task)
        {
            var response = Response<T>.Void();
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await PageDialogService.DisplayAlertAsync(AppResources.CheckInternetConnectionTitle, AppResources.CheckInternetConnectionMessage, AppResources.Ok);
                }
                else
                {

                    IsBusy = true;
                    response = await task;
                }
            }catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            IsBusy = false;
            return response;
        }
    }
}
