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
    public class ConnexionViewModel : INotifyPropertyChanged
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

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (value != _email)
                {
                    _email = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("_email"));
                    }
                }
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (value != _password)
                {
                    _password = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("_password"));
                    }
                }
            }
        }


        #region command

        private ICommand connexionUtilisateur;
        public ICommand ConnexionUtilisateur
        {
            get
            {
                if (connexionUtilisateur == null)
                {

                    connexionUtilisateur = connexionUtilisateur ?? (connexionUtilisateur = new RelayCommand(obj => { Connexion(); }));

                }
                return connexionUtilisateur;
            }
        }

        public void Connexion()
        {
            //requete api pour la connexion
        }

        public string EncodeMd5()
        {
            //fonction pour encoder le mdp
            return "";
        }

        #endregion command


    }
}
