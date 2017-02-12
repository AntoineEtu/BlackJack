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
        public int indexActif { get; set; }
        public Deck deck { get; set; }
        public List<Card> usedDeck { get; set; }

        public PlateauJeu(Table table)
        {
            this.bank = null;
            this.table = table;
            this.joueurs = null;
            this.deck = new Deck();
            this.indexActif = 0;
            this.usedDeck = new List<Card>();
        }

        public void initPartie(UserGame joueur)//un joueur en paramètre car c'est un face à face avec la banque
        {
            this.bank = new Bank();
            this.joueurs = new List<UserGame>();
            this.joueurs.Add(joueur);
            this.joueurs.Add(this.bank);//la banque est ajoutée en dernière pour l'odre des tours
            this.indexActif = 0;
        }

        public void launchGame(int mise)
        {
            //on considère uniquement le joueur actif qui est le seul à jouer face à la banque
            this.joueurs[0].bet = mise;
            distribueCartes();
            //on attend l'action du premier joueur
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

        //attribue une carte à un joueur
        public bool donneCarte(UserGame user)
        {
            user.main.Add(this.deck.nextCard());
            return this.deck.needShuffle;
        }

        //fonction à lancer après le tour de la banque
        public void gameOver()
        {

            //on détermine le,les gagnants
            determineWinners();
            //on récompense le,les gagnants
            recompenseUsers();


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
        }

        //détermine le,les gagnants et le coefficient de récompense (mise*coef)
        public void determineWinners()
        {
            //on prend le score de la banque
            int scoreBank = this.bank.calculScore();
            //on met le coefficient de gain de tous les utilisateurs à 0
            this.joueurs.ForEach(delegate (UserGame user)
            {
                user.winMulti = 0;
            });

            //début des tests

            if (scoreBank > 21)//si la banque saute
            {
                this.joueurs.ForEach(delegate (UserGame user)
                {
                    //tout le monde gagne s'il a moins de 22
                    if (user.calculScore() <= 21)
                    {
                        user.winMulti = 2f;//double ses gains
                        if(user.calculScore() == 21)
                        {
                            user.winMulti = 2.5f;//Black user
                        }
                        user.isWinner = true;
                    }
                });
            }
            else
            {
                //Si la banque a un score valide
                if (scoreBank <= 21)
                {
                    //Si la banque a 21
                    if (scoreBank == 21)
                    {
                        //Si la banque fait BlackJack
                        if (this.bank.main.Count == 2)
                        {
                            int score1 = 0;
                            //on cherche les joueurs qui ont pareils
                            this.joueurs.ForEach(delegate (UserGame user)
                            {
                                score1 = user.calculScore();
                                if (score1 == 21 && user.main.Count == 2)
                                {
                                    user.winMulti = 1f;//Black jack égalité
                                    user.isWinner = true;
                                    //checkAssurance
                                }
                                //sinon tout le monde perd
                                //checkAssurance
                            });
                        }
                        else//si la banque a plus de 2 cartes et 21
                        {
                            int score = 0;
                            //on cherche les joueurs qui ont pareil soit 21
                            this.joueurs.ForEach(delegate (UserGame user)
                            {
                                score = user.calculScore();
                                if (score == 21)
                                {
                                    if (user.main.Count == 2 && this.bank.main.Count > 2)//la banque a 21 avec plus de 2 cartes
                                    {
                                        user.winMulti = 2.5f;//Black jack 2.5x la mise
                                    }
                                    else
                                    {
                                        user.winMulti = 1f;//21 égalité
                                    }
                                }
                            });
                        }
                    }
                    else
                    {
                        //on regarde si quelqu'un a mieux
                        int score = 0;
                        this.joueurs.ForEach(delegate (UserGame user)
                        {
                            score = user.calculScore();
                            //si un joueur a un score > à la banque et <= 21
                            if (score > bank.calculScore() && score <= 21)
                            {
                                if (user.main.Count == 2 && score == 21)//BlackJack pour joueur
                                {
                                    user.winMulti = 2.5f;//Black jack 2.5x la mise
                                }
                                else
                                {
                                    user.winMulti = 2f;// 2x sa mise
                                }
                            }
                        });
                    }
                }
            }
        }
        //récompense les joueurs étant authentifier comme gagnant
        public void recompenseUsers()
        {
            this.joueurs.ForEach(delegate (UserGame user)
            {
                if (user.userDetails.username != "Bank" && user.isWinner)//on vérifie que c'est un gagnant
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
            this.indexActif= (this.indexActif + 1)%(nbJoueurs());
            UserGame curr = this.joueurs[indexActif];
            if (curr.userDetails.username == "Bank")
            {
                tourBanque();
            }
            else
            {
                //pas besoin de coder la fonction est lancé après le tour du joueur réel
            }
        }

        public void tourBanque()
        {
            if (this.bank.calculScore() < 16)
            {
                donneCarte(this.bank);
            }
            else
            {
                gameOver();//termine la partie
            }
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
