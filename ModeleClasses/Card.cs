using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleClasses
{
    public class Card
    {
        public int id { get; set; }//0=cutcard, 1 = as, ..., 11 = valet, 12=dame, 13=roi
        public int val { get; set; }//de 1 à 11
        public int sign { get; set; }//1 carré, 2 coeur, 3 treffle, 4 pique
        public bool isCutCard { get; set; }
        public String pictURL { get; set; }
        public String name { get; set; }

        public Card()
        {
            this.id = 0;
            this.val = 0;
            this.sign = 0;
            this.isCutCard = false;
            this.pictURL = "";
            this.name = "";
        }

        public Card(bool isCutCard)
        {
            if (isCutCard)
            {
                this.id = 0;
                this.val = 0;
                this.sign = 0;
                this.isCutCard = isCutCard;
                this.pictURL = "";
                this.name = "";
            }
        }

        public Card(int id, int sign, bool isCutCard, String url)
        {
            this.id = id;
            this.val = 0;
            this.sign = sign;
            this.isCutCard = false;
            this.pictURL = url;
        }

        public bool calculValeurAndName()
        {
            bool ok = false;
            if (this.id == 1)
            {
                this.val = 11;
                this.name = "As";
                ok = true;
            }
            if (this.id == 2)
            {
                this.val = this.id;
                this.name = "Deux";
                ok = true;
            }
            if (this.id == 3)
            {
                this.val = this.id;
                this.name = "Trois";
                ok = true;
            }
            if (this.id == 4)
            {
                this.val = this.id;
                this.name = "Quatre";
                ok = true;
            }
            if (this.id == 5)
            {
                this.val = this.id;
                this.name = "Cinq";
                ok = true;
            }
            if (this.id == 6)
            {
                this.val = this.id;
                this.name = "Six";
                ok = true;
            }
            if (this.id == 7)
            {
                this.val = this.id;
                this.name = "Sept";
                ok = true;
            }
            if (this.id == 8)
            {
                this.val = this.id;
                this.name = "Huit";
                ok = true;
            }
            if (this.id == 9)
            {
                this.val = this.id;
                this.name = "Neuf";
                ok = true;
            }
            if (this.id == 10)
            {
                this.val = this.id;
                this.name = "Dix";
                ok = true;
            }
            if (this.id == 11)
            {
                this.val = 10;
                this.name = "Valet";
                ok = true;
            }
            if (this.id == 12)
            {
                this.val = 10;
                this.name = "Dame";
                ok = true;
            }
            if (this.id == 13)
            {
                this.val = 10;
                this.name = "Roi";
                ok = true;
            }
            return ok;
        }

        public bool changeValeurAs()
        {
            bool ok = false;
            if (this.id == 1)
            {
                if(this.val == 11)
                {
                   this.val = 1;
                }else
                {
                    this.val = 11;
                }
            }
            return ok;
        }
    }
}
