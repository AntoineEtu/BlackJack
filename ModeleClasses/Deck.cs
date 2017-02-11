using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleClasses
{
    public class Deck
    {
       public List<Card> cardList { get; set; }

        public Deck()
        {
            this.cardList = null;
            create6Decks();
        }

        private List<Card> createDeck()//création d'un jeu de cartes en dur
        {
            List<Card> deck = new List<Card>();
            Card newCard = null;
            bool verif = false;

            for (int color = 1; color <= 4 ; color++)
            {
                for(int id = 1; id <= 13; id++)
                {
                    newCard = new Card(id, color, false, "");
                    verif = newCard.calculValeurAndName();
                    if (verif)
                    {
                        this.cardList.Add(newCard);
                    }else
                    {
                        return null;
                    }
                }
            }
            return deck;
        }

        private void create6Decks()
        {
            List<Card> deck = new List<Card>();
            //les 6 différents jeux sont rassemblés dans dans une seule Liste
            List<Card> list = createDeck();
            deck.Concat(list).ToList();
            list = createDeck();
            deck.Concat(list).ToList();
            list = createDeck();
            deck.Concat(list).ToList();
            list = createDeck();
            deck.Concat(list).ToList();
            list = createDeck();
            deck.Concat(list).ToList();
            list = createDeck();
            deck.Concat(list).ToList();
            //on affilie le deck à l'objet this : necessaire pour les autres méthodes
            this.cardList = deck;
            //Le deck est mélangé
            melangeCartes();
            //deux fois :p
            melangeCartes();
            //la cut card est ajouté
            addCutCard();
        }

        public void melangeCartes()
        {
            List<Card> deck = this.cardList;
            Random r = new Random();
            int n = deck.Count;
            while (n > 1)
            {
                //on echange la position de deux cartes n fois où n = nombre de cartes
                n--;
                int k = r.Next(n + 1);
                Card echange = deck[k];
                deck[k] = deck[n];
                deck[n] = echange;
            }
            this.cardList = deck;
        }

        public void addCutCard()
        {
            List<Card> deck = this.cardList;
            int nbCartes = deck.Count();
            int fiftyP = nbCartes / 2;//index à 50%
            int eightyP = (nbCartes / 5)*4;//index à 80%
            Random r = new Random();
            int indexInsert = r.Next(fiftyP, eightyP);
            Card remove = deck[indexInsert];
            deck[indexInsert] = new Card(true);//constructeur de la cutCard
            deck.Add(remove);//ajout de la carte en fin de paquet
            this.cardList = deck;
        }

        public void removeCutCard()
        {
            List<Card> deck = this.cardList;
            //parcours du deck
            for(int i = 0; i < deck.Count; i++)
            {
                if (deck[i].isCutCard)
                {
                    deck.Remove(deck[i]);//enlève la cutCard
                }
            }
        }
    }
}
