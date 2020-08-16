using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DepremAlarmi.Models
{
    public class Result
    {
        [JsonProperty("time")]
        public string Date { get; set; }

        [JsonProperty("lat")]
        private string _Latitude { set { Latitude = value; } }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("lon")]
        private string _Longitude { set { Longitude = value; } }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("depth")]
        public string Depth { get; set; }

        [JsonProperty("m")]
        private string Ml2 { set { Ml = value; } }

        [JsonProperty("magnitude")]
        public string Ml { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("district")]
        private string District { set { Location = value + " (" +City.ToUpper() + ")"; } }

        public string Country { get; set; }

        public string _Location { get; set; }
        [JsonProperty("location")]
        public string Location { get { return _Location; } set { _Location = value.ToUpper(); } }

        [JsonProperty("other")]
        public string Other { get; set; }

        public string ShareButton { get; set; }
        public string LocationButton { get; set; }
        public string Distance { get; set; }

        public string EventId { get; set; }
    }

    public class EarthQuake
    {
        public IList<Result> result { get; set; }
    }
}
