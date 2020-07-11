using Android.Widget;
using DepremAlarmi.Controls.Interfaces;
using DepremAlarmi.Droid.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(XamarinToast))]
namespace DepremAlarmi.Droid.Services
{
    public class XamarinToast : IMessage
    {
        public void LongMessage(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortMessage(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}
