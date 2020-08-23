using System; 
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using DepremAlarmi.Models;
using Newtonsoft.Json;

namespace DepremAlarmi.Controls.Services
{
    public class EarthQuakeService
    {
        public static async Task<EarthQuake> InfoEarthQuake()
        {
        tekrarDene:
            try
            {
                #region // JSON VERİ ÇEKME İŞLEMLERİ

                string url = "http://54.36.38.150:4477/api/Afad";

                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string jsonVerisi = reader.ReadToEnd();

                    if (jsonVerisi.Length > 0)
                    {
                        var value = JsonConvert.DeserializeObject<EarthQuake>(jsonVerisi);
                        Debug.WriteLine("Getirilen deprem sayısı: " + value.result.Count);

                        if (value.result.Count < 500)
                        {
                            goto tekrarDene;
                        }
                        else
                        {
                            return value;
                        }
                    }
                    else
                        return null;
                }
                #endregion
            }
            catch (Exception ex)
            {
                await Task.Delay(10000);
                goto tekrarDene;
            }
        }
    }
}
