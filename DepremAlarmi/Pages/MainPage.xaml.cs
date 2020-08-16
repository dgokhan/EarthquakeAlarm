using System;
using System.Linq;
using System.Threading.Tasks;
using AiForms.Dialogs;
using AiForms.Dialogs.Abstractions;
using DepremAlarmi.Controls.Interfaces;
using DepremAlarmi.PageModels; 
using Xamarin.Essentials;
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

                await Share.RequestAsync(new ShareTextRequest
                {
                    Text = $"( {parcalar[1]} ) büyüklüğünde {parcalar[0]} bölgesinde {parcalar[2]} saatinde deprem gerçekleşti! #deprem #{parcalar[3].ToLower()}",
                    Uri = "https://bit.ly/depremalarmi",
                    Title = "Deprem Bilgisini Paylaş!"
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

                    var location = new Location(Convert.ToDouble(parcalar[2]), Convert.ToDouble(parcalar[1]));
                    var options = new MapLaunchOptions { Name = parcalar[0] };

                    try
                    {
                        await Map.OpenAsync(location, options);
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Harita açılırken hata oluştu!", ex.Message, "Tamam");
                    }
                      
                    progress.Report(100);
                });
            }
            catch (Exception m)
            {
                await Application.Current.MainPage.DisplayAlert("Harita açılırken hata oluştu!", m.Message, "Tamam");
            }
        }

        private void searchBar_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
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
            }
            catch (Exception ex) { DependencyService.Get<IMessage>().LongMessage("Hooaydaağğ..\n" + ex.Message); }
        }

        private void SearchSizeButton_Clicked(System.Object sender, System.EventArgs e)
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
            }
            catch (Exception ex) { DependencyService.Get<IMessage>().LongMessage("Hooaydaağğ..\n" + ex.Message); }
        }
    }
}
