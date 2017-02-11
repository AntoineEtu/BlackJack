using BlackJack.View;
using ModeleClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BlackJack.ViewModel
{
    public class ConnexionViewModel : INotifyPropertyChanged
    {
        
       
        Frame actualFrame { get { return Window.Current.Content as Frame; } }

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


        public async void Connexion()
        {
            if(Email == null || !VerificationEmail(Email))
            {
                var message = new MessageDialog("Vérifier votre Email !");
                await message.ShowAsync();
            }
            if(Password == null)
            {
                var message = new MessageDialog("Vérifier votre mot de passe !");
                await message.ShowAsync();
            }
            else
            {
                User user = new User();
                user.Email = Email;
                user.Password = Password;
                user.Secret = EncodeToMd5(Password);

                utilisateurConnexion(user);
            }
           
        }

        public async void utilisateurConnexion(User user)
        {
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re");
                var json = JsonConvert.SerializeObject(user);
                var itemJson = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage responseAPI = await client.PostAsync("/api/auth/login", itemJson);



                if (responseAPI.IsSuccessStatusCode)
                {
                    //Récupération des données envoyer par l'API
                    var res = await responseAPI.Content.ReadAsStringAsync();
                    var objectJson = JObject.Parse(res);
                    var tokenObject = objectJson["tokens"];
                    var userObject = objectJson["user"];
                    

                    user.access_token = tokenObject["access_token"].ToString();
                    user.firstname = userObject["firstname"].ToString();
                    user.lastname = userObject["lastname"].ToString();
                    user.email = userObject["email"].ToString();
                    user.stack = int.Parse(userObject["stack"].ToString());

                    var message = new MessageDialog("Bienvenue");
                    await message.ShowAsync();

                    actualFrame.Navigate(typeof(Casino));

                }
                else
                {
                    var res = await responseAPI.Content.ReadAsStringAsync();
                    var dialog = new MessageDialog("Connexion refuser", res);
                    await dialog.ShowAsync();
                }
            }
        }

        // Methode qui vérifie la bonne écriture de l'email
        public bool VerificationEmail(string _email)
        {
            return Regex.IsMatch(_email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        //Methode qui encode le mot de passe 
        public string EncodeToMd5(string pass)
        {
            string hash = null;

            if (pass != null)
            {
                
                string algo = HashAlgorithmNames.Md5;
                IBuffer buffUtf8Msg = CryptographicBuffer.ConvertStringToBinary(pass, BinaryStringEncoding.Utf8);

                HashAlgorithmProvider objAlgProv = HashAlgorithmProvider.OpenAlgorithm(algo);
                hash = objAlgProv.AlgorithmName;

                IBuffer buffHash = objAlgProv.HashData(buffUtf8Msg);

                hash = CryptographicBuffer.EncodeToHexString(buffHash);
                hash = Convert.ToBase64String(Encoding.UTF8.GetBytes(hash));

            }


            return hash;
        }


        #endregion command


    }
}
