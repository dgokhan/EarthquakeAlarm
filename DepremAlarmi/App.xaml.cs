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
                    BarTextColor = Color.White,
                    BarBackgroundColor = Color.Black
                };

                tabs.AddTab<MainPageModel>("", "earthqauke.png");
                tabs.AddTab<MessagePageModel>("", "chat.png");

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
                bottomBarPage.AddTab<MessagePageModel>("", "chat.png");

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
