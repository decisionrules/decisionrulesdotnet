using System;
using static DecisionRules.Model.GeoLocationsEnum;

namespace DecisionRules
{
    public class RequestOption
    {
        private readonly GeoLocations geoloc;
        private readonly String apiKey;
        private readonly CustomDomain customDomain;
        private readonly String publicApikey;

        public RequestOption(String apiKey = default, String publicApiKey = default, GeoLocations geoloc = GeoLocations.DEFAULT, CustomDomain customDomain = default)
        {
            this.apiKey = apiKey;
            this.publicApikey = publicApiKey;
            this.geoloc = geoloc;
            this.customDomain = customDomain;

        }

        public GeoLocations Geoloc
        {
            get => geoloc;
        }

        public String ApiKey
        {
            get => apiKey;
        }

        public CustomDomain CustomDomain
        {
            get => customDomain;
        }

        public String PublicApiKey
        {
            get => publicApikey;
        }
    }
}
