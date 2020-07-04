using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DepremAlarmi.Pages
{
    public partial class SettingPage : ContentPage
    {
        public SettingPage()
        {
            InitializeComponent();
        }

        private void lstCountry_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //var selected = e.Item as Country;
            //SettingsPageModel.CountrySelectedItem = selected;
        }
    }
}
