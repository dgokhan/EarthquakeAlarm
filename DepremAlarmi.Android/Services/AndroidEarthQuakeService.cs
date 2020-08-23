using System;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using DepremAlarmi.Controls.Services.AndroidEarthQuakeServices;
using Xamarin.Forms;

namespace DepremAlarmi.Droid.Services
{
    [Service]
    public class AndroidEarthQuakeService : Service
    {
        private CancellationTokenSource _cts;
        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            _cts = new CancellationTokenSource();

            Intent notificationIntent = new Intent(this, typeof(MainActivity));
            PendingIntent pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent, 0);
            Notification notification = new Notification.Builder(this, MainActivity.CHANNEL_ID)
                .SetContentTitle("Deprem Alarmi")
                .SetContentText("Deprem bildirimi: AÇIK")
                .SetSmallIcon(Resource.Drawable.abc_ic_star_black_16dp)
                .SetContentIntent(pendingIntent)
                .Build();
            try
            {
                StartForeground(10000, notification);
            }
            catch (Exception ex)
            {
                throw;
            }


            Task.Run(() =>
            {
                try
                {
                    var counter = new Checker();
                     counter.RunChecker(_cts.Token).Wait();
                }
                catch (System.OperationCanceledException)
                {
                }
                finally
                {
                    if (_cts.IsCancellationRequested)
                    {
                        var message = new CancelledServices();
                        Device.BeginInvokeOnMainThread(
                            () => MessagingCenter.Send(message, "CancelledMessage")
                        );
                    }
                }

            }, _cts.Token);
            return StartCommandResult.Sticky;
        }
    }
}
