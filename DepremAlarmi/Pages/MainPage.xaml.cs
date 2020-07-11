using System;
using System.Linq;
using System.Threading.Tasks;
using AiForms.Dialogs;
using AiForms.Dialogs.Abstractions;
using DepremAlarmi.Controls.Interfaces;
using DepremAlarmi.PageModels;
using Plugin.ExternalMaps;
using Plugin.Share;
using Xamarin.Forms;

namespace DepremAlarmi.Pages
{
    public partial class MainPage : ContentPage
    {
        string searchQuery = string.Empty;

        public MainPage()
        {
            InitializeComponent(); 
        }

        private async void btnShare_Clicked(object sender, EventArgs e)
        {
            Configurations.LoadingConfig = new LoadingConfig
            {
                IndicatorColor = Color.White,
                OverlayColor = Color.Black,
                Opacity = 0.4,
                DefaultMessage = "Bilgiler getiriliyor..",
            };

            await Loading.Instance.StartAsync(async progress =>
            {
                Button btnSelected = (Button)sender;
                var btnInfo = btnSelected.ClassId;

                char[] ayrac = { '@' };
                string[] parcalar = btnInfo.Split(ayrac);


                await CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
                {
                    Text = $"{parcalar[0]} bölgesinde {parcalar[2]} saatinde {parcalar[1]} büyüklüğünde deprem gerçekleşti! #deprem bilylink",
                    Title = "Paylaş!"
                });
                await Task.Delay(1000);
                progress.Report(100);
            });
        }

        private async void btnLocation_Clicked(object sender, EventArgs e)
        {
            try
            {
                Configurations.LoadingConfig = new LoadingConfig
                {
                    IndicatorColor = Color.White,
                    OverlayColor = Color.Black,
                    Opacity = 0.4,
                    DefaultMessage = "Harita açılıyor..",
                };

                await Loading.Instance.StartAsync(async progress =>
                {
                    Button btnSelected = (Button)sender;
                    var btnInfo = btnSelected.ClassId;

                    char[] ayrac = { '@' };
                    string[] parcalar = btnInfo.Split(ayrac);

                    var success = await CrossExternalMaps.Current.NavigateTo(parcalar[0], Convert.ToDouble(parcalar[2]), Convert.ToDouble(parcalar[1]));

                    await Task.Delay(1000);

                    progress.Report(100);
                });
            }
            catch (Exception m)
            {
                await Application.Current.MainPage.DisplayAlert("Harita açılırken hata oluştu!", m.Message, "Tamam");
            }
        }

        void searchBar_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            try
            {
                var _container = BindingContext as MainPageModel;

                if (string.IsNullOrWhiteSpace(e.NewTextValue))
                { 
                    EarthQuakeLists.ItemsSource = _container.EarthQuakeList;
                    searchBar.Text = string.Empty; searchQuery = string.Empty;
                }
                else
                {
                    searchQuery = e.NewTextValue.ToUpper().Replace("Ğ", "G").Replace("İ", "I").Replace("Ş", "S").Replace("Ö", "O").Replace("Ç", "C");
                    EarthQuakeLists.ItemsSource = _container.EarthQuakeList.Where(i => i.Location.Contains(searchQuery));
                }
            } catch (Exception ex) { DependencyService.Get<IMessage>().LongMessage("Hooaydaağğ..\n"+ex.Message ); }
        }

        void SearchSizeButton_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                var _container = BindingContext as MainPageModel;

                var selectedButtonText = (sender as Button).Text;
                if (selectedButtonText == "Tümü")
                {
                    EarthQuakeLists.ItemsSource = _container.EarthQuakeList;
                    searchBar.Text = string.Empty; searchQuery = string.Empty;
                }
                else
                {
                    EarthQuakeLists.ItemsSource = _container.EarthQuakeList.Where(x => Convert.ToDouble(x.Ml) >= Convert.ToDouble(selectedButtonText.Replace(" >", "")) && x.Location.Contains(searchQuery));
                }
            } catch (Exception ex) { DependencyService.Get<IMessage>().LongMessage("Hooaydaağğ..\n" + ex.Message); }
        }
    }
}
