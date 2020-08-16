using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AiForms.Dialogs;
using AiForms.Dialogs.Abstractions;
using DepremAlarmi.Controls.Helpers;
using DepremAlarmi.Controls.Interfaces;
using DepremAlarmi.Controls.Services;
using DepremAlarmi.Models;
using FreshMvvm;
using Xamarin.Essentials;
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
                try
                {
                    Configurations.LoadingConfig = new LoadingConfig
                    {
                        IndicatorColor = Color.White,
                        OverlayColor = Color.Black,
                        Opacity = 0.4,
                        DefaultMessage = "Deprem verileri yükleniyor.."
                    };

                    await Loading.Instance.StartAsync(async progress =>
                    {
                        //EarthQuakeList.Add(new Result
                        //{
                        //    Location = "BUCA (İZMİR)",
                        //    Date = "2020.05.02 11:11:11",
                        //    Ml = "2.7",
                        //    Depth = "3.2",
                        //    Latitude = "",
                        //    Longitude = "",  
                        //});

                        RequestPermissionsHelpers req = new RequestPermissionsHelpers();
                        var permissionLocation = await req.RequestLocationPermission();
                        if (permissionLocation)
                        {
                            var request = new GeolocationRequest(GeolocationAccuracy.High);
                            var locationInformation = await Geolocation.GetLocationAsync(request);
                            if (locationInformation != null)
                            {
                                Latitude = locationInformation.Latitude;
                                Longitude = locationInformation.Longitude;
                                Altitude = locationInformation.Altitude;
                            }

                            await RefreshData();

                            await CoreMethods.PushPopupPageModel<PlayStoreVotingPageModel>(1);
                        }
                    });
                }
                catch (System.Exception ex)
                {
                    await RefreshData();
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

        private bool _switchEarthQuakeInformation = false;
        public bool SwitchEarthQuakeInformation { get { return _switchEarthQuakeInformation; } set { _switchEarthQuakeInformation = value; OnPropertyChanged(nameof(SwitchEarthQuakeInformation)); } }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Altitude { get; set; }

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

        public ICommand SelectionChangedCommand
        {
            get
            {
                return new Command<Result>(item =>
                {
                    #region | En üstteki deprem verilerini değiştirme işlemi |

                    TopInformation(item);

                    #endregion
                });
            }
        }

        public ICommand SwitchOff
        {
            get
            {
                return new Command(() =>
                {
                    SwitchEarthQuakeInformation = false;
                });
            }
        }

        public ICommand SwitchOn
        {
            get
            {
                return new Command(async () =>
                {
                    SwitchEarthQuakeInformation = true;
                });
            }
        }

        #endregion

        #region | Voids |

        public async Task RefreshData()
        {
            try
            {

                #region | Veri Çekimi İşlemi |

                earthQuakeList.Clear();

                EarthQuake data = await EarthQuakeService.InfoEarthQuake(null);

                var res = (from s in data.result
                           where s.Other == "-" && s.Country == "Türkiye"
                           select s).ToList();

                #endregion

                foreach (var item in res)
                {
                    #region | Boş gelen Location değerini doldurma işlemi |

                    if (item.Location == "- (-)")
                    {
                        char[] _ayrac = { '(' };
                        item.Other = item.Other + "(";
                        var newValue = item.Other.Split(_ayrac);
                        if (newValue.Length > 1)
                        {
                            /*char[] _ayracTwo = { '.' }; 
                            var newValueTwo = item.Other.Split(_ayracTwo);
                            var _countyValue = newValueTwo[1].ToString();
                              
                            string _city = newValue[1].ToString().Replace(")", "").Replace(" ", "").Replace(",", "");

                            string _county = _countyValue.Replace((_city),"").Replace(")", "").Replace("(", "");*/
                            item.Location = newValue[1].ToString().Replace(")", "").Replace(" ", "").Replace(",", "");
                        }

                        if (newValue[1].ToString() == "")
                            item.Location = newValue[0].ToString().Replace(")", "").Replace(" ", "").Replace(",", "");
                    }
                    #endregion

                    #region | Veri basma işlemi |

                    Location userLocation = new Location(Latitude, Longitude);
                    Location earthquakeLocation = new Location(Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude));
                    double distanceKM = Location.CalculateDistance(userLocation, earthquakeLocation, DistanceUnits.Kilometers);

                    EarthQuakeList.Add(new Result
                    {
                        Location = item.Location,
                        Date = item.Date,
                        Ml = item.Ml,
                        Depth = item.Depth,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        ShareButton = item.Location + "@" + item.Ml + "@" + item.Date + "@" + item.City,
                        LocationButton = item.Location + "@" + item.Longitude + "@" + item.Latitude,
                        Distance = Convert.ToInt32(distanceKM).ToString() + "km"
                    });

                    #endregion
                }

                #region | Bugün yaşanan en büyük deprem |

                var todayRes = (from s in res
                                where DateTime.ParseExact(s.Date, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture) >= DateTime.Now.AddDays(-1)
                                select s).OrderByDescending(c => c.Ml).First();

                TodayHighestEarthQuake = "Bugün yaşanan en büyük deprem : " + todayRes.Location + " - Şiddet " + todayRes.Ml + "";
                #endregion

                #region | En üstteki deprem verilerini gösterme işlemi |

                TopInformation(res[0]);

                #endregion

                DependencyService.Get<IMessage>().LongMessage("Deprem verileri güncellendi...");
            }
            catch (System.Exception ex)
            {

            }
        }

        public void TopInformation(Result EarthQuakeInformation)
        {
            TopLocation = EarthQuakeInformation.Location;
            TopMl = EarthQuakeInformation.Ml;
            TopDepth = EarthQuakeInformation.Depth;
            TopDate = EarthQuakeInformation.Date;
            TopShareInformation = EarthQuakeInformation.Location + "@" + EarthQuakeInformation.Ml + "@" + EarthQuakeInformation.Date + "@" + EarthQuakeInformation.City;

            char[] ayrac = { '(' };
            var shortLocation = EarthQuakeInformation.Location.Split(ayrac);
            if (shortLocation.Length > 1)
            {
                TopShortLocation = shortLocation[1].ToString().Replace(")", "").Replace(" ", "");
            }
            else
            {
                TopShortLocation = EarthQuakeInformation.Location;
            }
        }

        #endregion
    }
}
