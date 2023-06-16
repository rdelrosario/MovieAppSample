using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MovieTimeApp.Helpers;
using MovieTimeApp.Services;
using MovieTimeApp.ViewModels;
using MovieTimeApp.Views;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Plugin.Popups;
using Xamarin.Forms;

namespace MovieTimeApp
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }
        private static IConfig _config = new Config();

        protected override void OnInitialized()
        {
            InitializeComponent();
            LoadAppPreferences();

            NavigationService.NavigateAsync($"{NavigationConstants.Navigation}/{NavigationConstants.Home}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IConfig>(_config);
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomePage, HomeViewModel>();
            containerRegistry.RegisterForNavigation<DetailsPage, DetailsViewModel>();
            containerRegistry.RegisterPopupNavigationService();

            containerRegistry.RegisterInstance<IApiClient<IMoviesApi>>(new ApiClient<IMoviesApi>(_config.ApiUrl));
            containerRegistry.Register<IMoviesService, MoviesService>();

            containerRegistry.RegisterInstance<IApiClient<IConfigApi>>(new ApiClient<IConfigApi>(_config.ApiUrl));
            containerRegistry.Register<IConfigServive, ConfigService>();
        }

        async Task LoadAppPreferences()
        {
            try
            {
                var appConfigs = await Container.Resolve<IConfigServive>().GetConfigs();

                if (appConfigs.SuccessResult)
                {
                    _config.ImageBaseUrl = appConfigs.Result?.Images?.Secure_base_url;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}

