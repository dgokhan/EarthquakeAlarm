using System.Collections.ObjectModel;
using System.ComponentModel;
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
            EarthQuakeList.Add(new EarthQuake {  CityInformation = "İzmir - Konak" , Date = "2020.07.01 - 19:01", Size = "4.3", Depth = "2.3"});
            EarthQuakeList.Add(new EarthQuake { CityInformation = "Manisa - Konak", Date = "2020.07.01 - 20:06", Size = "6.4", Depth = "3.3" });
            EarthQuakeList.Add(new EarthQuake { CityInformation = "Aydın - Konak", Date = "2020.07.01 - 21:05", Size = "2.4", Depth = "4.1" });
            EarthQuakeList.Add(new EarthQuake { CityInformation = "İzmir - Konak", Date = "2020.07.01 - 19:01", Size = "5.3", Depth = "2.3" });
            EarthQuakeList.Add(new EarthQuake { CityInformation = "İzmir - Konak", Date = "2020.07.01 - 19:01", Size = "5.3", Depth = "2.3" });
            EarthQuakeList.Add(new EarthQuake { CityInformation = "İzmir - Konak", Date = "2020.07.01 - 19:01", Size = "5.3", Depth = "2.3" });
            EarthQuakeList.Add(new EarthQuake { CityInformation = "İzmir - Konak", Date = "2020.07.01 - 19:01", Size = "5.3", Depth = "2.3" });
            EarthQuakeList.Add(new EarthQuake { CityInformation = "İzmir - Konak", Date = "2020.07.01 - 19:01", Size = "5.3", Depth = "2.3" });
        }
    }
}
