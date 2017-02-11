using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleClasses
{
    class Card
    {
        public int id { get; set; }// 1 = as, ..., 11 = valet, 12=dame, 13=roi
        public int val { get; set; }//de 1 à 11
        public int sign { get; set; }//1 carré, 2 coeur, 3 treffle, 4 pique
        public bool isCutCard { get; set; }
        public String pictURL { get; set; }

        public Card()
        {
            this.id = 0;
            this.val = 0;
            this.sign = 0;
            this.isCutCard = false;
            pictURL = "";
        }

        public Card(int id, int val, int sign, bool isCutCard, String url)
        {
            this.id = id;
            this.val = val;
            this.sign = sign;
            this.isCutCard = isCutCard;
            this.pictURL = url;
        }
    }
}
