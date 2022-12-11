using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TDBNP_FINAL
{
    class track
    {
        public clsTracks Tracks;
    }

    public class clsTracks
    {
        [JsonProperty("track")]
        public List<TracksData> tracksList { get; set; }
    }

    public class TracksData
    {
        public string name { get; set; }
        public string mbid { get; set; }
        public string url { get; set; }
    }
}
