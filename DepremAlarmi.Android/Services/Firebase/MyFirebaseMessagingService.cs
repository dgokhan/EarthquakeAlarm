using System;
using Android.App;
using Android.Content;
using Android.OS;
using DepremAlarmi.Droid.Helpers;
using Firebase.Messaging;

namespace DepremAlarmi.Droid.Services.Firebase
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            new NotificationHelper().CreateNotification(message.GetNotification().Title, message.GetNotification().Body);
        }

        //private void SendNotification(string title, string body)
        //{
        //    NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
        //    if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
        //    {

        //    }
        //}
    }
}
