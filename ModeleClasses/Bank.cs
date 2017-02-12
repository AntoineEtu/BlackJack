

using System.Collections.Generic;

namespace ModeleClasses
{
    public class Bank : UserGame
    {
        public Bank()
        {
            this.main = new List<Card>();
            this.isTurnOver = false;
            this.userDetails = new User();
            this.userDetails.Username = "Bank";
            this.isUser = false;            
        }
    }
}