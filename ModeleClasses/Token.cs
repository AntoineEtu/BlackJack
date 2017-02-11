using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleClasses
{
    public class Token
    {
        /*
         "token_type" : "XXXX",
         "expires_in" : "XXXX",
         "access_token" : "XXXX",
         "refresh_token" : "XXXX"
         */
        [JsonProperty("token_type")]
        public string token_type { get; set; }

        [JsonProperty("expires_in")]
        public double expires_in { get; set; }

        [JsonProperty("access_token")]
        public string access_token { get; set; }

        [JsonProperty("refresh_token")]
        public string refresh_token { get; set; }

        public Token()
        {
            this.token_type = null;
            this.expires_in = 0;
            this.access_token = null;
            this.refresh_token = null;
        }

        ~Token()
        {

        }


    }
}
