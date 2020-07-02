using System;
namespace DepremAlarmi.Models
{
    public class EarthQuake
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Depth { get; set; }
        public string Md { get; set; }
        public string Ml { get; set; }
        public string Mw { get; set; }
        public string Location { get; set; }
        public string Revize { get; set; }
    }
}
