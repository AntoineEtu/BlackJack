using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleClasses
{
    public class PlateauJeu
    {
        public Bank bank { get; set; }
        public Table table { get; set; }
        public List<UserGame> joueurs { get; set; }
        public UserGame userActif { get; set; }
        public Deck deck { get; set; }
        public List<Card> usedDeck { get; set; }

        public PlateauJeu(Table table)
        {
            this.bank = null;
            this.table = table;
            this.joueurs = null;
            this.deck = new Deck();
            this.userActif = null;
            this.usedDeck = new List<Card>();
        }

        public void initPartie(UserGame joueur)//un joueur en paramètre car c'est un face à face avec la banque
        {
            this.bank = new Bank();
            this.joueurs = new List<UserGame>();
            this.joueurs.Add(joueur);
            this.joueurs.Add(this.bank);//la banque est ajoutée en dernière pour l'odre des tours
            this.userActif = this.joueurs[0];
        }

        public void launchGame(int mise)
        {
            //on considère uniquement le joueur actif qui est le seul à jouer face à la banque
            this.joueurs[0].bet = mise;
            distribueCartes();

        }

        public bool distribueCartes()//le booleen permet de savoir si un mélange est nécessaire
        {
            //calcul nb cartes à distribuer
            int  nbCartes = 2*(this.nbJoueurs());
            UserGame ug = null;
            //on donne 2 fois une carte à tous les joueurs
            for (int i = 1; i <= nbCartes; i++)
            {
                ug = this.joueurs[(i%this.nbJoueurs())];
                if(!ug.isUser && i==nbCartes)//si c'est la dernière carte à donner et que le joueur est la banque
                {
                    Card card = this.deck.nextCard();
                    card.isVisible = false;//la carte apparait face cachée
                    ug.main.Add(card);
                }else
                {
                    ug.main.Add(this.deck.nextCard());//sinon ajout normal
                }
                i++;
            }
            return this.deck.needShuffle;

        }

        public bool donneCarte(UserGame user)
        {
            user.main.Add(this.deck.nextCard());
            return this.deck.needShuffle;
        }

        public bool gameOver()
        {
            //on mélange le paquet si nécessaire
            if (this.deck.needShuffle)
            {
                this.deck.melangeCartes();
            }

            //on vide la main de chaque joueur
            this.joueurs.ForEach(delegate (UserGame user)
            {
                user.main.Clear();
            });

            //on détermine le,les gagnants
            determineWinners();
            //on récompense le,les gagnants
            recompenseUsers();
            return this.deck.needShuffle;
        }

        public void determineWinners()
        {
            //on prend le score de la banque

            //on regarde si quelqu'un a mieux
            int score = 0;
            this.joueurs.ForEach(delegate (UserGame user){
                score = user.calculScore();
                if(score == 21)
                {
                    if (user.main.Count == 2)
                    {
                        user.winMulti = 2.5f;//Black jack 2.5 la mise
                    }
                }
            });
        }

        public void recompenseUsers()
        {
            this.joueurs.ForEach(delegate (UserGame user)
            {
                if (user.userDetails.username != "Bank" && user.isWinner)
                {
                    double m = user.winMulti*user.bet;
                    user.giveMoney(m);
                }
            });
        }

        private int nbJoueurs()
        {
            return this.joueurs.Count;

        }

        public void tourSuivant()
        {
        }

        public UserGame JoueurSuivant()
        {
            UserGame actif = null;
            /*
            foreach j : joueurs{

            }
            */
            return actif;
        }
    }
}
