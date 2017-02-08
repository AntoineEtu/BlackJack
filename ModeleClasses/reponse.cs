using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleClasses 
{
    public class Reponse
    {
        [JsonProperty("user")]
        public User user { get; set; }
        [JsonProperty("status")]
        public String status { get; set; }
        [JsonProperty("message")]
        public String message { get; set; }
        [JsonProperty("error_code")]
        public String error_code { get; set; }
        [JsonProperty("error")]
        public String error { get; set; }
        /*
        [JsonProperty("tokens")]
        public Token token { get; set; }
        public Reponse()
        {
            user = new User();
            this.token = new Token();
        }*/

        ~Reponse()
        {
        }
    }
}
