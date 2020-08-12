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

            if (Device.RuntimePlatform == Device.iOS)
            {
                var tabs = new FreshMvvm.FreshTabbedNavigationContainer()
                {
                    BarTextColor = Color.FromRgb(1, 146, 135),
                    BarBackgroundColor = Color.FromRgb(1, 146, 135)
                };

                tabs.AddTab<MainPageModel>("", "earthqauke.png");
                tabs.AddTab<MessagePageModel>("", "chat.png");
                tabs.AddTab<InformationPageModel>("", "information.png");

                MainPage = tabs;
            }
            else
            {
                var bottomBarPage = new CustomNavigation()
                {
                    SelectedTabColor = Color.FromRgb(1, 146, 135),
                    BackgroundColor = Color.FromRgb(1, 146, 135)
                };

                bottomBarPage.AddTab<MainPageModel>("", "earthqauke.png");
                bottomBarPage.AddTab<SettingPageModel>("", "settings.png");
                bottomBarPage.AddTab<InformationPageModel>("", "information.png");

                MainPage = bottomBarPage;

            }

            

            
            //MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<SettingPageModel>());
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
