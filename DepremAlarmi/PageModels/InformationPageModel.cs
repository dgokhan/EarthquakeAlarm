using AiForms.Dialogs;
using AiForms.Dialogs.Abstractions;
using FreshMvvm;
using Rg.Plugins.Popup.Extensions;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DepremAlarmi.PageModels
{
    public class InformationPageModel : FreshBasePageModel, INotifyPropertyChanged
    {
        #region | PropertyChanged |

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public ICommand GithubClickCommand => new Command(async () => await Browser.OpenAsync("https://github.com/dgokhan/EarthquakeAlarm", BrowserLaunchMode.SystemPreferred));

        public InformationPageModel()
        {
        }
    }
}
