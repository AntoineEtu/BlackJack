using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleClasses
{
    public class User
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("username")]
        public string username { get; set; }

        [JsonProperty("firstname")]
        public string firstname { get; set; }

        [JsonProperty("lastname")]
        public string lastname { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("password")]
        public string password { get; set; }

        [JsonProperty("status")]
        public int status { get; set; }

        [JsonProperty("is_connected")]
        public int is_connected { get; set; }

        [JsonProperty("stack")]
        public int stack { get; set; }

        [JsonProperty("last_refill")]
        public DateTime last_refill { get; set; }

        [JsonProperty("created_at")]
        public DateTime created_at { get; set; }

        [JsonProperty("updated_at")]
        public DateTime updated_at { get; set; }

        [JsonProperty("secret")]
        public string secret { get; set; }


        public User()
        {
            this.id = 0;
            this.username = null;
            this.firstname = null;
            this.lastname = null;
            this.email = null;
            this.password = null;
            this.status = 0;
            this.created_at = new DateTime();
            this.updated_at = new DateTime();
            this.stack = 0;
            this.is_connected = 0;
            this.status = 0;
            this.last_refill = new DateTime();
            

        }

        public User(string username, string firstname, string lastname, string email, string password)
        {
            this.id = 0;
            this.username = username;
            this.firstname = firstname;
            this.lastname = lastname;
            this.email = email;
            this.password = password;
            this.status = 0;
            this.created_at = new DateTime();
            this.updated_at = new DateTime();
            this.stack = 0;
            this.is_connected = 0;
            this.status = 0;
            this.last_refill = new DateTime();
        }

        ~User()
        {

        }

    }
}
