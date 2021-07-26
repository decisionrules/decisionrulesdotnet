using System;
using static DecisionRules.Model.GeoLocationsEnum;

namespace DecisionRules
{
    public class RequestOption
    {
        private readonly GeoLocations geoloc;
        private readonly String apiKey;
        private readonly String uri;

        public RequestOption(String apiKey, GeoLocations geoloc = GeoLocations.DEFAULT, String uri = default)
        {
            this.apiKey = apiKey;
            this.geoloc = geoloc;
            this.uri = uri;
        }

        public GeoLocations Geoloc
        {
            get => geoloc;
        }

        public String ApiKey
        {
            get => apiKey;
        }

        public String Uri
        {
            get => uri;
        }
    }
}
