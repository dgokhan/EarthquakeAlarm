using System;
using Android.App;
using Android.Runtime;
using Shiny;

namespace DepremAlarmi.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }

        public override void OnCreate()
        {
            AndroidShinyHost.Init(this, new EarthQuakeStartup());
            base.OnCreate();
        }
    }
}