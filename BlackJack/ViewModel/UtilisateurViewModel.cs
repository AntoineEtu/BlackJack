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

        private string _username;
        public string Username
        {
            get { return _username; }
            set {
                if (value != _username)
                {
                    _username = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("_username"));
                    }
                }
            }
        }

        private string _firstname;
        public string Firstname
        {
            get { return _firstname; }
            set {
                if (value != _firstname)
                {
                    _firstname = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("_firstname"));
                    }
                }
            }
        }

        private string _lastname;
        public string Lastname
        {
            get { return _lastname; }
            set
            {
                if (value != _lastname)
                {
                    _lastname = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("_lastname"));
                    }
                }
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set {
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
            set {
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

        private string _passwordConfirm;
        public string PasswordConfirm
        {
            get { return _passwordConfirm; }
            set {
                if (value != _passwordConfirm)
                {
                    _passwordConfirm = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("_passwordConfirm"));
                    }
                }
            }
        }




        private ICommand inscriptionUtilisateur;
        public ICommand InscriptionUtilisateur
        {
            get
            {
                if (inscriptionUtilisateur == null)
                {

                    inscriptionUtilisateur = inscriptionUtilisateur ?? (inscriptionUtilisateur = new RelayCommand(obj => { Inscription(); }));

                }
                return inscriptionUtilisateur;
            }
        }



        public void Inscription()
        {
            
        }
    }
}
