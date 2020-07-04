using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using DepremAlarmi.Controls.Services;
using DepremAlarmi.Models;
using FreshMvvm;

namespace DepremAlarmi.PageModels
{
    public class MainPageModel : FreshBasePageModel, INotifyPropertyChanged
    {
        #region | CTOR |

        public MainPageModel()
        {
            Task.Run(async () =>
            {
                var data = await EarthQuakeService.InfoEarthQuake(null);
                try
                {
                    foreach (var item in data.result)
                    {
                        EarthQuakeList.Add(new Result
                        {
                            Location = item.Location,
                            Date = item.Date,
                            Ml = item.Ml,
                            Depth = item.Depth,
                            ShareButton = item.Location + "@" + item.Ml + "@" + item.Date,
                            LocationButton = item.Location + "@" + item.Longitude + "@" + item.Latitude
                        });
                    }

                    TopLocation = data.result[0].Location;
                    TopMl = data.result[0].Ml;
                    TopDepth = data.result[0].Depth;
                    TopDate = data.result[0].Date;
                    char[] ayrac = { '(' };
                    var shortLocation = data.result[0].Location.Split(ayrac);
                    TopShortLocation = data.result[0].Location.Split(ayrac)[1].ToString().Replace(")", "");
                }
                catch (System.Exception ex)
                {
                    TopShortLocation = data.result[0].Location;
                }
            });
        }

        #endregion

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

        ObservableCollection<Result> earthQuakeList = new ObservableCollection<Result>();
        public ObservableCollection<Result> EarthQuakeList
        {
            get { return earthQuakeList; }
            set { earthQuakeList = value; OnPropertyChanged("EarthQuakeList"); }
        }

        #endregion

        #region | Variable Types |

        private string topLocation;
        public string TopLocation { get { return topLocation; } set { topLocation = value; OnPropertyChanged("TopLocation"); } }

        private string topShortLocation;
        public string TopShortLocation { get { return topShortLocation; } set { topShortLocation = value; OnPropertyChanged("TopShortLocation"); } }

        private string topMl;
        public string TopMl { get { return topMl; } set { topMl = value; OnPropertyChanged("TopMl"); } }

        private string topDepth;
        public string TopDepth { get { return topDepth; } set { topDepth = value; OnPropertyChanged("TopDepth"); } }

        private string topDate;
        public string TopDate { get { return topDate; } set { topDate = value; OnPropertyChanged("TopDate"); } }

        #endregion

        #region | Commands |

        //public ICommand ShareSocialButton => new Command(InstagramLogin);

        #endregion

        #region | Voids |



        #endregion
    }
}
