using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleClasses
{
    public class Table : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        [JsonProperty("id")]
        private int id;

        public int Id
        {
            get { return id; }
            set {
                if (value != id)
                {
                    id = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("id"));
                    }
                }
            }
        }

       

        [JsonProperty("max_seat")]
       
        private int max_seat;

        public int Max_seat
        {
            get { return max_seat; }
            set {
                if (value != max_seat)
                {
                    max_seat = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("max_seat"));
                    }
                }
            }
        }

        [JsonProperty("seats_available")]
        private int seats_available;
        public int Seats_available
        {
            get { return seats_available; }
            set {
                if (value != seats_available)
                {
                    seats_available = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("seats_available"));
                    }
                }
            }
        }

        [JsonProperty("min_bet")]
        private Double min_bet;
        public Double Min_bet
        {
            get { return min_bet; }
            set {
                min_bet = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("min_bet"));
                }
            }
        }

        [JsonProperty("last_activity")]
        private DateTime last_activity;
        public DateTime Last_activity
        {
            get { return last_activity; }
            set {
                last_activity = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("last_activity"));
                }
            }
        }

        [JsonProperty("is_closed")]
        private Double is_closed;
        public Double Is_closed
        {
            get { return is_closed; }
            set {
                is_closed = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("is_closed"));
                }
            }
        }

        [JsonProperty("created_at")]
        private DateTime created_at;
        public DateTime Created_at
        {
            get { return created_at; }
            set {
                created_at = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("created_at"));
                }
            }
        }


        [JsonProperty("updated_at")]
        private DateTime updated_at;

        public DateTime Updated_at
        {
            get { return updated_at; }
            set
            {
                updated_at = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("updated_at"));
                }
            }
        }



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
