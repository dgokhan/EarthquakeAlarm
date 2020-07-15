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
    public class MessagePageModel : FreshBasePageModel, INotifyPropertyChanged
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

         
    }
}
