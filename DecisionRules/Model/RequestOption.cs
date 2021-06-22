using System;

namespace DecisionRules
{
    public class RequestOption
    {
        private readonly String geoloc;
        private readonly String apiKey;

        public RequestOption(String apiKey, String geoloc = default)
        {
            this.apiKey = apiKey;
            this.geoloc = geoloc;
        }

        public String Geoloc
        {
            get => geoloc;
        }

        public String ApiKey
        {
            get => apiKey;
        }
    }
}
