using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleClasses
{
    public class Table
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("max_seat")]
        public int max_seat { get; set; }
        [JsonProperty("seats_available")]
        public int seats_available { get; set; }
        [JsonProperty("min_bet")]
        public Double min_bet { get; set; }
        [JsonProperty("last_activity")]
        public DateTime last_activity { get; set; }
        [JsonProperty("is_closed")]
        public Double is_closed { get; set; }
        [JsonProperty("created_at")]
        public DateTime created_at { get; set; }
        [JsonProperty("updated_at")]
        public DateTime updated_at { get; set; }

        public Table()
        {
            this.id = 0;
            this.max_seat = 0;
            this.seats_available = 0;
            this.min_bet = 0;
            this.last_activity = new DateTime();
            this.is_closed = 0;
            this.created_at = new DateTime();
            this.updated_at = new DateTime();
        }
    }
}
