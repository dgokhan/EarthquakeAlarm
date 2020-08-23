using System;
using System.Threading;
using System.Threading.Tasks;
using DepremAlarmi.Models;
using Xamarin.Forms;

namespace DepremAlarmi.Controls.Services.AndroidEarthQuakeServices
{
    public class Checker
    {
        public async Task RunChecker(CancellationToken token)
        {
        tryAgain:
            try
            {
                await Task.Run(async () =>
                {
                    for (long i = 0; i < long.MaxValue; i++)
                    {
                        token.ThrowIfCancellationRequested();
                        await Task.Delay(120000);

                        var message = await EarthQuakeService.InfoEarthQuake();
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            MessagingCenter.Send<EarthQuake>(message, "TickedMessage");
                        });
                    }
                }, token);
            }
            catch (Exception)
            {
                goto tryAgain;
            }
        }
    }
}
