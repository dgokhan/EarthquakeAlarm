using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DepremAlarmi.Models
{
    public class Result
    {
        [JsonProperty("time")]
        public string Date { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("depth")]
        public string Depth { get; set; }

        [JsonProperty("magnitude")]
        public string Ml { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        public string ShareButton { get; set; }
        public string LocationButton { get; set; }
    }

    public class EarthQuake
    {
        public IList<Result> result { get; set; }
    }
}
