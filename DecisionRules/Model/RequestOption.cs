using System;
using static DecisionRules.Model.GeoLocationsEnum;

namespace DecisionRules
{
    public class RequestOption
    {
        private readonly GeoLocations geoloc;
        private readonly String apiKey;

        public RequestOption(String apiKey, GeoLocations geoloc = GeoLocations.DEFAULT)
        {
            this.apiKey = apiKey;
            this.geoloc = geoloc;
        }

        public GeoLocations Geoloc
        {
            get => geoloc;
        }

        public String ApiKey
        {
            get => apiKey;
        }
    }
}
