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
                    BarTextColor = Color.Black,
                    BarBackgroundColor = Color.FromHex("#019287")
                };
                 
                tabs.BarTextColor = Color.FromHex("#019287");

                tabs.AddTab<InformationPageModel>("Anasayfa", "icon_home.png");
                tabs.AddTab<MainPageModel>("Depremler", "icon_earthquake.png");
                tabs.AddTab<SettingPageModel>("Güvendeyim", "icon_safe.png");
                tabs.AddTab<SettingPageModel>("Ayarlar", "icon_settings.png");
                tabs.AddTab<InformationPageModel>("Hakkında", "icon_information.png");

                MainPage = tabs;
            }
            else
            {
                var bottomBarPage = new CustomNavigation() { BarTextColor = Color.Black, BarBackgroundColor = Color.FromHex("#019287") };

                bottomBarPage.FixedMode = true;
                bottomBarPage.BarTheme = BottomBarPage.BarThemeTypes.Light;
                bottomBarPage.BarTextColor = Color.FromHex("#019287");

                bottomBarPage.AddTab<InformationPageModel>("Anasayfa", "icon_home.png");
                bottomBarPage.AddTab<MainPageModel>("Depremler", "icon_earthquake.png");
                bottomBarPage.AddTab<SettingPageModel>("Güvendeyim", "icon_safe.png");
                bottomBarPage.AddTab<SettingPageModel>("Ayarlar", "icon_settings.png");
                bottomBarPage.AddTab<InformationPageModel>("Hakkında", "icon_information.png");

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
