using Android.App;
using Android.Content.PM;
using Android.OS;

namespace DepremAlarmi.Droid
{
    [Activity(
        Label = "Deprem Alarmi",
        Icon = "@drawable/icon",
        Theme = "@style/MyTheme.Splash",
        MainLauncher = true,
        NoHistory = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.StartActivity(typeof(MainActivity));
        }

        public override void OnBackPressed() { }

    }
}
