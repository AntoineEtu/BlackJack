using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleClasses
{
    public class PlateauJeu
    {
        public List<User> listeJoueurs { get; set; }
        public List<Card> bankHand { get; set; }

        public PlateauJeu()
        {
            listeJoueurs = null;
            bankHand = null;
        }
    }
}
