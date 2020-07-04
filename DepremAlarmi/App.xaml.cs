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
                BarTextColor = Color.White,
                BackgroundColor = Color.FromRgb(1, 146, 135),
            };

            bottomBarPage.AddTab<MainPageModel>("", "menu_tr.png");
            bottomBarPage.AddTab<SettingPageModel>("TARTIŞMA", "chat_tr.png");
            bottomBarPage.AddTab<SettingPageModel>("AYARLAR", "settings_tr.png");
            bottomBarPage.AddTab<InformationPageModel>("HAKKIMIZDA", "info_tr.png");

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
