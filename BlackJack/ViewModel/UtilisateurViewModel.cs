using ModeleClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace BlackJack.ViewModel
{
    public class UtilisateurViewModel : INotifyPropertyChanged
    {

        //Pour les messages d'erreur
        MessageDialog dialog = new MessageDialog(" ");

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string str = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(str));
            }
        }
       
        

        private ICommand inscriptionUtilisateur;
        public ICommand InscriptionUtilisateur
        {
            get
            {
                if (inscriptionUtilisateur == null)
                {
                    
                    inscriptionUtilisateur = new RelayCommand<object>((obj) => { Inscription(); });
                    
                }
                return inscriptionUtilisateur;
            }
        }



        public void Inscription()
        {
            //ici la fonction qui envoie les donnée a l'api
        }
    }
}
