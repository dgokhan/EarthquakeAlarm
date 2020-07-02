using DepremAlarmi.Controls.CustomHelpers;
using DepremAlarmi.ViewModels;
using FreshMvvm;
using Xamarin.Forms; 
namespace DepremAlarmi
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var loginPage = FreshPageModelResolver.ResolvePageModel<MainPageModel>();
            MainPage = new FreshNavigationContainer(loginPage);

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
