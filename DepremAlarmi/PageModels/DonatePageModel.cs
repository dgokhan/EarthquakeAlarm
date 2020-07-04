using System;
using System.ComponentModel;
using FreshMvvm;

namespace DepremAlarmi.PageModels
{
    public class DonatePageModel : FreshBasePageModel, INotifyPropertyChanged
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

        public DonatePageModel()
        {

        } 
    }
}
