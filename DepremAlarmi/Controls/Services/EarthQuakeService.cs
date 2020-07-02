using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DepremAlarmi.Models;
using Newtonsoft.Json;

namespace DepremAlarmi.Controls.Services
{
    public class EarthQuakeService
    {
        public static async Task<List<EarthQuake>> InfoEarthQuake(string regionName)
        {
        tekrarDene:
            try
            {
                #region // JSON URL HAZIRLAMA
                string url = "https://gokhandogru.net/earthquake/api.php";
                if (regionName != null)
                    url = "http://54.36.38.150:8080/?location=" + regionName + "";
                #endregion

                #region // JSON VERİ ÇEKME İŞLEMLERİ
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string jsonVerisi = reader.ReadToEnd();

                    if (jsonVerisi.Length > 0)
                    {
                        return JsonConvert.DeserializeObject<List<EarthQuake>>(jsonVerisi);
                    }
                    else
                        return null;
                }
                #endregion
            }
            catch (Exception)
            {
                await Task.Delay(10000);
                goto tekrarDene;
            }
        }
    }
}
