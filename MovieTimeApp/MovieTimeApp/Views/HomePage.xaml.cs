using MovieTimeApp.Helpers;
using Xamarin.Forms;

namespace MovieTimeApp.Views
{	
	public partial class HomePage : ContentPage
	{
		public HomePage(IConfig config)
		{
			InitializeComponent();
			AppTitle.Text = config.AppName;
        }
	}
}

