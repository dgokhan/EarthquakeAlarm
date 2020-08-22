using BottomBar.XamarinForms;
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
                var bottomBarPage = new CustomNavigation() { BarTextColor = Color.Black, BarBackgroundColor = Color.Black };

                bottomBarPage.FixedMode = true;
                bottomBarPage.BarTheme = BottomBarPage.BarThemeTypes.Light;
                bottomBarPage.BarTextColor = Color.FromHex("#019287");

                bottomBarPage.AddTab<MainPageModel>("Anasayfa", "icon_home.png");
                bottomBarPage.AddTab<SettingPageModel>("Ayarlar", "icon_library.png");
                bottomBarPage.AddTab<SettingPageModel>("Ayarlar", "settings.png");
                bottomBarPage.AddTab<InformationPageModel>("Hakkında", "information.png");

                MainPage = bottomBarPage;

            }  
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
