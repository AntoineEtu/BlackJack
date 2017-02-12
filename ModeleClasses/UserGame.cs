using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleClasses
{
    public class UserGame
    {
        public List<Card> main { get; set; }//ses cartes
        public int bet { get; set; }//sa mise
        public bool isTurnOver { get; set; }
        public User userDetails { get; set; }
        public bool isUser { get; set; }
        public bool assurance { get; set; }
        public bool isWinner { get; set; }
        public double winMulti { get; set; }

        public UserGame()//constructeur de la banque
        {

        }
        public UserGame( User user)//constructeur du joueur
        {
            this.main = new List<Card>();
            this.bet = 0;
            this.userDetails = user;
            this.isTurnOver = false;
            this.isUser = true;//joueur réel
            this.assurance = false;
            this.isWinner = false;
            this.winMulti = 1;
        }

        public bool equalsTo(UserGame autre)
        {
            bool ok = false;
            if (this.userDetails.Username == autre.userDetails.Username)
            {
                ok = true;
            }
            return ok;
        }

        public void giveMoney(double amount)
        {
            this.userDetails.stack += amount;
            this.bet = 0;
            this.assurance = false;
            this.isTurnOver = false;
        }

        public int calculScore()
        {
            int score = 0;
            int val = 0;
            this.main.ForEach(delegate (Card card)
            {
                val = card.val;
                score = score + val;
                if(score > 21)
                {
                    //on regarde s'il a un as dans son jeu qui vaut 11
                    this.main.ForEach(delegate (Card carte)
                    {
                        if(carte.id==1 && carte.name == "As" && carte.val==11)
                        {
                            //on change la valeur de l'as
                            carte.val = 1;
                            //on appelle la fonction par récursivité
                            score = calculScore();
                        }
                    });

                    //il a pas d'As ou son score est à jour et il a plus de 21

                }
            });
            return score;
        }
    }
}
