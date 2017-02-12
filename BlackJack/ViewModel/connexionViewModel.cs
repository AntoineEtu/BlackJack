using BlackJack.View;
using ModeleClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
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
    public class ConnexionViewModel : ViewModel
    {

        public event PropertyChangedEventHandler PropertyChanged;
        Frame actualFrame { get { return Window.Current.Content as Frame; } }



        public ConnexionApi connexionApi;
        public ConnexionApi ConnexionApi
        {
            get { return connexionApi; }
            set {
                connexionApi = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("connexionApi"));
                }
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

        [JsonProperty("tables")]
        private ObservableCollection<Table> _listTable;
        public ObservableCollection<Table> ListTable
        {
            get { return _listTable; }
            set
            {
                SetProperty(ref _listTable, value);

            }
        }

        private ICommand exitUtilisateur;
        public ICommand ExitUtilisateur
        {
            get
            {
                if (exitUtilisateur == null)
                {

                    exitUtilisateur = exitUtilisateur ?? (exitUtilisateur = new RelayCommand(obj => { Quitter(); }));

                }
                return exitUtilisateur;
            }
        }

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

        public void Quitter()
        {

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

                    this.connexionApi = new ConnexionApi();

                    //Récupération des données envoyer par l'API
                    var reponse = await responseAPI.Content.ReadAsStringAsync();
                    var objectJson = JObject.Parse(reponse);

                    var tokenObject = objectJson["tokens"];
                    var userObject = objectJson["user"];


                    connexionApi.token.access_token = tokenObject["access_token"].ToString();
                    connexionApi.user.firstname = userObject["firstname"].ToString();
                    connexionApi.user.lastname = userObject["lastname"].ToString();
                    connexionApi.user.email = userObject["email"].ToString();
                    connexionApi.user.stack = int.Parse(userObject["stack"].ToString());


                    var message = new MessageDialog("Bienvenue");
                    await message.ShowAsync();

                    actualFrame.Navigate(typeof(Casino),this.connexionApi);

                }
                else
                {
                    var res = await responseAPI.Content.ReadAsStringAsync();
                    var dialog = new MessageDialog("Connexion refuser", res);
                    await dialog.ShowAsync();
                }
            }
        }

        

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
        



        


    }
}
