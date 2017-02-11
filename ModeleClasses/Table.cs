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
        public int Id { get; set; }
        [JsonProperty("max_seat")]
        public int Max_seat { get; set; }
        [JsonProperty("seats_available")]
        public int Seats_available { get; set; }
        [JsonProperty("min_bet")]
        public Double Min_bet { get; set; }
        [JsonProperty("last_activity")]
        public DateTime Last_activity { get; set; }
        [JsonProperty("is_closed")]
        public Double Is_closed { get; set; }
        [JsonProperty("created_at")]
        public DateTime Created_at { get; set; }
        [JsonProperty("updated_at")]
        public DateTime Updated_at { get; set; }

        public Table()
        {
            this.Id = 0;
            this.Max_seat = 0;
            this.Seats_available = 0;
            this.Min_bet = 0;
            this.Last_activity = new DateTime();
            this.Is_closed = 0;
            this.Created_at = new DateTime();
            this.Updated_at = new DateTime();
        }
    }
}
