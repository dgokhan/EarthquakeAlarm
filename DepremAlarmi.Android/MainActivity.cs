using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using FFImageLoading.Forms.Platform;
using Android;
using Plugin.Permissions;
using Android.Gms.Common;
using Shiny;
using Android.Content; 
using Xamarin.Forms;
using System;
using DepremAlarmi.Controls.Services.AndroidEarthQuakeServices;
using DepremAlarmi.Droid.Services;

namespace DepremAlarmi.Droid
{
    [Activity(Label = "Deprem Alarmi", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
       readonly string[] Permission =
       {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.ForegroundService,
        };

        public static string CHANNEL_ID = "AlarmChnnelId";
        private const int RequestId = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            CachedImageRenderer.Init(true);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            RequestPermissions(Permission, RequestId);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            AiForms.Dialogs.Dialogs.Init(this);

            IsPlayServicesAvailable();

            LoadApplication(new App());
            this.ShinyOnCreate();

            _CreateNotificationChannel();
            MessagingCenter.Subscribe<StartLongRunningTaskMessage>(this, "StartLongRunningTaskMessage", message => {
                var intent = new Intent(this, typeof(AndroidEarthQuakeService));
                try
                {
                    StartService(intent);
                }
                catch (Exception ex)
                { 
                    throw;
                }
            });
            MessagingCenter.Subscribe<StopLongRunningTaskMessage>(this, "StopLongRunningTaskMessage", message => {
                var intent = new Intent(this, typeof(AndroidEarthQuakeService));
                StopService(intent);
            }); 
        }

        private void _CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                NotificationChannel serviceChannel = new NotificationChannel(CHANNEL_ID, "Alarm Channel", NotificationImportance.Low);
                NotificationManager manager = GetSystemService(Context.NotificationService) as NotificationManager;
                manager.CreateNotificationChannel(serviceChannel);
            }
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            this.ShinyOnNewIntent(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            this.ShinyRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {

                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    //msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                }

                else
                {
                    // msgText.Text = "This device is not supported";
                    Finish();
                }
                return false;
            }
            else
            {
                // do whatever if play service is not available
                //msgText.Text = "Google Play Services is available.";
                return true;
            }
        }
    }
}