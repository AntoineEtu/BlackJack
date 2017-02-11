using ModeleClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BlackJack.ViewModel
{
    public class UtilisateurViewModel : INotifyPropertyChanged
    {

        //Pour les messages d'erreur
       // MessageDialog message = new MessageDialog(" ");
        //Frame actualFrame { get { return Window.Current.Content as Frame; } }
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


        public async void Inscription()
        {
            if (Username == null || Firstname == null || Lastname == null)
            {
                var message = new MessageDialog("Vérifier vos noms !");
                await message.ShowAsync();
            }
            if (Password != PasswordConfirm || Password == null || PasswordConfirm == null)
            {
                var message = new MessageDialog("Verrifier vos mots de passe !");
                await message.ShowAsync();
            }
            if(Email == null || !VerificationEmail(Email))
            {
                var message = new MessageDialog("Vérifier la syntaxe de votre email !");
                await message.ShowAsync();
            }
           
            else
            {
                User user = new User();
                user.Lastname = Lastname;
                user.Username = Username;
                user.Firstname = Firstname;
                user.Email = Email;
                user.Password = Password;


                ajoutUtilisateur(user);
            }
          
        }


        public async void ajoutUtilisateur(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re");

                var json = JsonConvert.SerializeObject(new { user = user });
                var itemJson = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("/api/auth/register", itemJson);

                if (response.IsSuccessStatusCode)
                {
                    var message = new MessageDialog("Votre inscription a été réussite!");
                    await message.ShowAsync();
                    string res = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine(res);
                }
                else
                {
                    var message = new MessageDialog("Votre inscription n'a pas marcher!", json);
                    await message.ShowAsync();
                    string res = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine(res);
                }
            }
        }
        public bool VerificationEmail(string _email)
        {
            return Regex.IsMatch(_email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

    }

        
    
}
