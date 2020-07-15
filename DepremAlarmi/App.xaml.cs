using DepremAlarmi.Controls.CustomHelpers;
using DepremAlarmi.PageModels;
using Xamarin.Forms; 
namespace DepremAlarmi
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<SettingPageModel>());
            var bottomBarPage = new CustomNavigation()
            { 
                BackgroundColor = Color.FromRgb(1, 146, 135),
            };

            bottomBarPage.AddTab<MainPageModel>("", "earthqauke.png");
            bottomBarPage.AddTab<MessagePageModel>("", "earthqauke.png");
            bottomBarPage.AddTab<InformationPageModel>("", "information.png");

            MainPage = bottomBarPage;
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
