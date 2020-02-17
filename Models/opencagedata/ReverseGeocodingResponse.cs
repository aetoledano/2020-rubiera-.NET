using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace rubiera.opencagedata
{
    public class ReverseGeocodingResponse
    {
        
        Rate rate;
        List<LocationData> results;

        public ReverseGeocodingResponse()
        {
        }

        public Rate Rate
        {
            get => rate;
            set => rate = value;
        }

        public List<LocationData> Results
        {
            get => results;
            set => results = value;
        }
    }
}