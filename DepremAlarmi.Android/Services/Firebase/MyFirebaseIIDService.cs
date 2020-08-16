using Android.App;
using Android.Util;
using Firebase.Iid;

namespace DepremAlarmi.Droid.Services.Firebase
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {  
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            SendRegistrationToServer(refreshedToken);
        }

        void SendRegistrationToServer(string token)
        {
            Log.Debug(PackageName,token);
        }
    }
}
