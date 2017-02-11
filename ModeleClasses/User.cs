using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleClasses
{
    public class User : INotifyPropertyChanged
    {
        #region variables
        [JsonProperty("id")]
        public int id;

        [JsonProperty("username")]
        public string username;

        [JsonProperty("firstname")]
        public string firstname;

        [JsonProperty("lastname")]
        public string lastname;

        [JsonProperty("email")]
        public string email;

        [JsonProperty("password")]
        public string password;

        [JsonProperty("status")]
        public int status;

        [JsonProperty("is_connected")]
        public int is_connected;

        [JsonProperty("stack")]
        public int stack;

        [JsonProperty("last_refill")]
        public DateTime last_refill;

        [JsonProperty("created_at")]
        public DateTime created_at;

        [JsonProperty("updated_at")]
        public DateTime updated_at;

        [JsonProperty("secret")]
        public string secret;

        [JsonProperty("access_token")]
        public string access_token;

        #endregion variables 

        public string Access_token
        {
            get { return access_token; }
            set
            {
                if (value != access_token)
                {
                    access_token = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("access_token"));
                    }
                }
            }
        }

        public int Id
        {
            get { return id; }
            set
            {
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
       
        public string Username
        {
            get { return username; }
            set {
                if (value != username)
                {
                    username = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("username"));
                    }
                }
            }
        }
       
        public string Firstname
        {
            get { return firstname; }
            set {
                if (value != firstname)
                {
                    firstname = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("firstname"));
                    }
                }
            }
        }

        public string Lastname
        {
            get { return lastname; }
            set {
                if (value != lastname)
                {
                    lastname = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("lastname"));
                    }
                }
            }
        }

        public string Email
        {
            get { return email; }
            set {
                if (value != email)
                {
                    email = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("email"));
                    }
                }
            }
        }

        public string Password
        {
            get { return password; }
            set {
                if (value != password)
                {
                    password = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("password"));
                    }
                }
            }
        }

        public int Status
        {
            get { return status; }
            set {
                if (value != status)
                {
                    status = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("status"));
                    }
                }
            }
        }

        public int Is_connected
        {
            get { return is_connected; }
            set {
                if (value != is_connected)
                {
                    is_connected = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("is_connected"));
                    }
                }
            }
        }

        public int Stack
        {
            get { return stack; }
            set {
                if (value != stack)
                {
                    stack = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("stack"));
                    }
                }
            }
        }

        public DateTime Last_refill
        {
            get { return last_refill; }
            set {
                if (value != last_refill)
                {
                    last_refill = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("last_refill"));
                    }
                }
            }
        }

        public DateTime Created_at
        {
            get { return created_at; }
            set {
                if (value != last_refill)
                {
                    last_refill = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("last_refill"));
                    }
                }
            }
        }

        public DateTime Updated_at
        {
            get { return updated_at; }
            set {
                if (value != updated_at)
                {
                    updated_at = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("updated_at"));
                    }
                }
            }
        }

        public string Secret
        {
            get { return secret; }
            set {
                if (value != secret)
                {
                    secret = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("secret"));
                    }
                }
            }
        }


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
            this.username = username;
            this.firstname = firstname;
            this.lastname = lastname;
            this.email = email;
            this.password = password;
        }

        ~User()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
