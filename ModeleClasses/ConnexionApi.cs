using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleClasses
{
    public class ConnexionApi
    {
        [JsonProperty("user")]
        public User user { get; set; }

        [JsonProperty("token")]
        public Token token { get; set; }

        [JsonProperty("table")]
        public Table tableUtiliser { get; set; }


        // public int etatCo { get; set; } // 0 = dédo, 1=  connecter 


        //constructeur de la connexionApi avec donc Un utilisateur et un token qui prendron des valeurs renvoyer par l'api au moment de la connexion
        public ConnexionApi()
        {
            user = new User();
            token = new Token();
            tableUtiliser = new Table();
        }

        ~ConnexionApi()
        {

        }
    }
}
