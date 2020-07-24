using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AiForms.Dialogs;
using AiForms.Dialogs.Abstractions;
using DepremAlarmi.Controls.Helpers;
using DepremAlarmi.Controls.Services;
using DepremAlarmi.Models;
using FreshMvvm;
using Xamarin.Forms;

namespace DepremAlarmi.PageModels
{
    public class MainPageModel : FreshBasePageModel, INotifyPropertyChanged
    {
        #region | CTOR |

        public MainPageModel()
        {
            Task.Run(async () =>
            {
                await RefreshData();
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
            set { earthQuakeList = value; }
        }

        #endregion

        #region | Variable Types / Encapsulation |

        private bool _isRefreshing = false;
        public bool IsRefreshing { get { return _isRefreshing; } set { _isRefreshing = value; OnPropertyChanged(nameof(IsRefreshing)); } }

        private string topLocation;
        public string TopLocation { get { return topLocation; } set { topLocation = value; OnPropertyChanged("TopLocation"); } }

        private string _topShortLocation;
        public string TopShortLocation { get { return _topShortLocation; } set { _topShortLocation = value; OnPropertyChanged(nameof(TopShortLocation)); } }

        private string topMl;
        public string TopMl { get { return topMl; } set { topMl = value; OnPropertyChanged("TopMl"); } }

        private string topDepth;
        public string TopDepth { get { return topDepth; } set { topDepth = value; OnPropertyChanged("TopDepth"); } }

        private string topDate;
        public string TopDate { get { return topDate; } set { topDate = value; OnPropertyChanged("TopDate"); } }

        private string topShareInformation;
        public string TopShareInformation { get { return topShareInformation; } set { topShareInformation = value; OnPropertyChanged(nameof(TopShareInformation)); } }

        private string todayHighestEarthQuake;
        public string TodayHighestEarthQuake { get { return todayHighestEarthQuake; } set { todayHighestEarthQuake = value; OnPropertyChanged(nameof(TodayHighestEarthQuake)); } }

        #endregion

        #region | Commands |

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await RefreshData();

                    IsRefreshing = false;
                });
            }
        }

        #endregion

        #region | Voids |

        public async Task RefreshData()
        {
            Configurations.LoadingConfig = new LoadingConfig
            {
                IndicatorColor = Color.White,
                OverlayColor = Color.Black,
                Opacity = 0.4,
                DefaultMessage = "Deprem verileri getiriliyor.."
            };

            await Loading.Instance.StartAsync(async progress =>
            {
                try
                {

                    earthQuakeList.Clear();
                    EarthQuake data = await EarthQuakeService.InfoEarthQuake(null);
                    foreach (var item in data.result)
                    {
                        if (item.Location == "- (-)")
                        {
                            char[] _ayrac = { '(' };
                            item.Other = item.Other + "(";
                            var newValue = item.Other.Split(_ayrac);
                            item.Location = newValue[1].ToString().Replace(")", "").Replace(" ", "");
                        }

                        EarthQuakeList.Add(new Result
                        {
                            Location = item.Location,
                            Date = item.Date,
                            Ml = item.Ml,
                            Depth = item.Depth,
                            Latitude = item.Latitude,
                            Longitude = item.Longitude,
                            ShareButton = item.Location + "@" + item.Ml + "@" + item.Date,
                            LocationButton = item.Location + "@" + item.Longitude + "@" + item.Latitude
                        });
                    }
                    TopLocation = data.result[0].Location;
                    TopMl = data.result[0].Ml;
                    TopDepth = data.result[0].Depth;
                    TopDate = data.result[0].Date;
                    TopShareInformation = data.result[0].Location + "@" + data.result[0].Ml + "@" + data.result[0].Date;

                    char[] ayrac = { '(' };
                    var shortLocation = data.result[0].Location.Split(ayrac);
                    if (shortLocation.Length > 0)
                    {
                        TopShortLocation = shortLocation[1].ToString().Replace(")", "").Replace(" ", "");
                    }
                    else
                    {
                        TopShortLocation = data.result[0].Location;
                    }

                    //var res = (from s in EarthQuakeList
                    //           where Convert.ToDateTime(s.Date) >= DateTime.Now.AddDays(-1)
                    //           select s).OrderByDescending(c => c.Ml).First();

                    //TodayHighestEarthQuake = "Bugün yaşanan en büyük deprem : " + res.Location + " - Şiddet " + res.Ml + "";

                    // TODO : bug / check..
                    //DependencyService.Get<IMessage>().LongMessage("Deprem verileri güncellendi...");

                    //await CoreMethods.PushPopupPageModel<PlayStoreVotingPageModel>(1);


                }
                catch (System.Exception ex)
                {

                }
            });
        }
        #endregion
    }
}
