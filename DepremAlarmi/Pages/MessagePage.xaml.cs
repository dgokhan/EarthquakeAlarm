using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DepremAlarmi.Controls.Client;
using DepremAlarmi.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DepremAlarmi.Pages
{
    public partial class MessagePage : ContentPage
    {
        IList<SignalRUser> model = new ObservableCollection<SignalRUser>();
        SignalRClient client = new SignalRClient();

        public MessagePage()
        {
            InitializeComponent();

            client.Connect("gokhan");
            client.ConnectionError += Client_ConnectionError;
            client.OnMessageReceived += Client_OnMessageReceived;
            this.BindingContext = model;
        }

        private void Client_OnMessageReceived(SignalRUser user)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                model.Add(user);
            });
        }

        private void Client_ConnectionError()
        {
            DisplayAlert("Connection", "Error", "Ok");
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                var message = new SmsMessage("test", "905546889731");
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Sms is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }

            //client.SendMessage("gokhan", message.Text);
        }

        async void SendMessage(object sender, EventArgs e)
        { 
             Device.BeginInvokeOnMainThread(() =>
             {
                 client.SendMessage("gokhan", message.Text);
                 message.Text = "";
                 message.Focus();
             });
        }

    }
}
