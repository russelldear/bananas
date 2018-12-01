
using Newtonsoft.Json;

namespace bnaas
{
    public class BandNameResult
    {
        public BandNameResult(string bandName)
        {
            BandName = bandName;
        }

        [JsonProperty("bandname")]
        public string BandName { get; set; }
    }
}
