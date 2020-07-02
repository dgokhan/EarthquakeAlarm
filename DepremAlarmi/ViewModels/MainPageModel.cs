using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using DepremAlarmi.Controls.Services;
using DepremAlarmi.Models;
using FreshMvvm;

namespace DepremAlarmi.ViewModels
{
    public class MainPageModel : FreshBasePageModel, INotifyPropertyChanged
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

        #region | Collections |

        ObservableCollection<EarthQuake> earthQuakeList = new ObservableCollection<EarthQuake>();
        public ObservableCollection<EarthQuake> EarthQuakeList
        {
            get { return earthQuakeList; }
            set { earthQuakeList = value; OnPropertyChanged("EarthQuakeList"); }
        }

        #endregion

        public MainPageModel()
        {
            Task.Run(async () =>
            {
                var data = await EarthQuakeService.InfoEarthQuake(null);
                foreach (var item in data)
                {
                    EarthQuakeList.Add(new EarthQuake {
                        Location = item.Location,
                        Date = item.Date + "-" +item.Time,
                        Ml = item.Ml,
                        Depth = item.Depth });
                }
            });
        }
    }
}
