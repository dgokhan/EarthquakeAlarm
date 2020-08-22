﻿using System;
using System.Windows.Input;
using DepremAlarmi.Controls.Helpers;
using FreshMvvm;
using Shiny;
using Shiny.Jobs;
using Xamarin.Forms;

namespace DepremAlarmi.PageModels
{
    public class PlayStoreVotingPageModel : FreshBasePageModel
    {
        #region | Commands |

        public ICommand OpenPlayStore
        {
            get
            {
                return new Command(async () =>
                {
                    Device.OpenUri(new Uri("market://details?id=net.f8filter.depremalarmi"));
                });
            }
        }

        public ICommand ClosePopupCommand
        {
            get
            {
                return new Command(async () =>
                {

                    //await ShinyHost.Resolve<IJobManager>().RequestAccess();
                    //await ShinyHost.Resolve<IJobManager>().Run(MainPageModel.mJobName);

                    await CoreMethods.PopPopupPageModel();
                    await CoreMethods.PopToRoot(false);
                });
            }
        }

        #endregion
    }
}
