using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleClasses
{
    class GameController
    {
        public PlateauJeu plateau;
        public User userActif;

        public GameController(PlateauJeu plateau)
        {
            this.plateau = plateau;
        }

        public User JoueurSuivant() {
            User actif = this.userActif;

            return actif;
        }
    }
}
