using System;
using System.ComponentModel;
using FreshMvvm;

namespace DepremAlarmi.PageModels
{
    public class SettingPageModel : FreshBasePageModel, INotifyPropertyChanged
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

        #region | CTOR |

        public SettingPageModel()
        {

        }

        #endregion

        #region | Variable Types |

        bool _notifyStatus = true;
        public bool notifyStatus
        {
            get
            {
                return _notifyStatus;
            }
            set
            {
                if (!value)
                {
                    notifyAllTurkeyVisible = false;
                    notifyFilterTurkeyVisible = false;
                    txtNotify = "Bildirimler: KAPALI";
                }
                else
                {
                    notifyAllTurkeyVisible = true;
                    notifyFilterTurkeyVisible = true;
                    txtNotify = "Bildirimler: AÇIK";
                }

                _notifyStatus = value;
                OnPropertyChanged("notifyStatus");
            }
        }

        bool _notifyAllTurkey = true;
        public bool notifyAllTurkey
        {
            get
            {
                return _notifyAllTurkey;
            }
            set
            {
                if (!value && !notifyFilterTurkey)
                {
                    notifyFilterTurkey = true;
                    notifyAllTurkeyEnable = false;
                    notifyFilterTurkeyEnable = true;
                }

                _notifyAllTurkey = value;
                OnPropertyChanged("notifyAllTurkey");
            }
        }
         
        bool _notifyFilterTurkey = false;
        public bool notifyFilterTurkey
        {
            get
            {
                return _notifyFilterTurkey;
            }
            set
            {
                if (!value && !notifyAllTurkey)
                {
                    notifyAllTurkey = true;
                    notifyFilterTurkeyEnable = false;
                    notifyAllTurkeyEnable = true;
                }

                _notifyFilterTurkey = value;
                OnPropertyChanged("notifyFilterTurkey");
            }
        }

        bool _notifyAllTurkeyVisible = true;
        public bool notifyAllTurkeyVisible
        {
            get
            {
                return _notifyAllTurkeyVisible;
            }
            set
            {
                _notifyAllTurkeyVisible = value;
                OnPropertyChanged("notifyAllTurkeyVisible");
            }
        }

        bool _notifyFilterTurkeyVisible = true;
        public bool notifyFilterTurkeyVisible
        {
            get
            {
                return _notifyFilterTurkeyVisible;
            }
            set
            {
                _notifyFilterTurkeyVisible = value;
                OnPropertyChanged("notifyFilterTurkeyVisible");
            }
        }

        bool _notifyAllTurkeyEnable = true;
        public bool notifyAllTurkeyEnable
        {
            get
            {
                return _notifyAllTurkeyEnable;
            }
            set
            {
                _notifyAllTurkeyEnable = value;
                OnPropertyChanged("notifyAllTurkeyEnable");
            }
        }

        bool _notifyFilterTurkeyEnable = true;
        public bool notifyFilterTurkeyEnable
        {
            get
            {
                return _notifyFilterTurkeyEnable;
            }
            set
            {
                _notifyFilterTurkeyEnable = value;
                OnPropertyChanged("notifyFilterTurkeyEnable");
            }
        }

        float sliderValue = 1.0f;
        public float SliderValue
        {
            get => sliderValue;
            set
            {
                var values = Math.Round(value, 1);
                sliderValue = value;
                txtEQNotify = "" + values + "'den büyük depremlerde bildirim gönder!";
                OnPropertyChanged("SliderValue");
            }
        }

        float sliderValueTwo = 5.0f;
        public float SliderValueTwo
        {
            get => sliderValueTwo;
            set
            {
                var values = Math.Round(value, 1);
                sliderValueTwo = value;
                txtEQSound = "" + values + "'den büyük depremlerde ilk yardım sesi çalsın!";
                OnPropertyChanged("SliderValueTwo");
            }
        }

        string _txtEQSound = "5.0'den büyük depremlerde ilk yardım sesi çalsın!)";
        public string txtEQSound
        {
            get => _txtEQSound;
            set
            {
                _txtEQSound = value;
                OnPropertyChanged("txtEQSound");
            }
        }

        string _txtEQNotify = "1.0'den büyük depremlerde bildirim gönder!";
        public string txtEQNotify
        {
            get => _txtEQNotify;
            set
            {
                _txtEQNotify = value;
                OnPropertyChanged("txtEQNotify");
            }
        }

        string _txtNotify = "Bildirimler : AÇIK";
        public string txtNotify
        {
            get => _txtNotify;
            set
            {
                _txtNotify = value;
                OnPropertyChanged("txtNotify");
            }
        }

        string _txtCountryName = string.Empty;
        public string txtCountryName
        {
            get => _txtCountryName;
            set
            {
                _txtCountryName = value;
                OnPropertyChanged("txtCountryName");
            }
        }

        #endregion
    }
}
