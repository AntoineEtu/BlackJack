using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleClasses
{
    public class UserGame
    {
        public List<Card> cardList { get; set; }
        public int bet { get; set; }
        public int total { get; set; }
        public bool isTurnOver { get; set; }

        public UserGame(int bet)
        {
            this.cardList = null;
            this.bet = bet;
            this.isTurnOver = false;
        }
    }
}
