using ModeleClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BlackJack.ViewModel
{
    public class CasinoViewModel : ViewModel
    {
        Frame actualFrame { get { return Window.Current.Content as Frame; } }
        public event PropertyChangedEventHandler PropertyChanged;

        //création d'une liste pour stocker nos tables renvoyer par l'api
        [JsonProperty("tables")]
        private ObservableCollection<Table> _listeDeTable;
        public ObservableCollection<Table> ListeDeTable
        {
            get { return _listeDeTable; }
            set {
                SetProperty(ref _listeDeTable, value);
            }
        }

        [JsonProperty("users")]
        private ObservableCollection<User> _listeUser;
        public ObservableCollection<User> ListeUser
        {
            get { return _listeUser; }
            set
            {
                SetProperty(ref _listeUser, value);

            }
        }

        public ConnexionApi connexionApi;
        public ConnexionApi ConnexionApi
        {
            get { return connexionApi; }
            set
            {
                connexionApi = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("connexionApi"));
                }
            }
        }

        private string reponseJson;
        public string ReponseJson
        {
            get { return reponseJson; }
            set
            {
                SetProperty(ref reponseJson, value);
            }
        }
        private string reponseJsonInfos;
        public string ReponseJsonInfos
        {
            get { return reponseJsonInfos; }
            set
            {
                SetProperty(ref reponseJsonInfos, value);
            }
        }
        private string reponseJsonUser;
        public string ReponseJsonUser
        {
            get { return reponseJsonUser; }
            set
            {
                SetProperty(ref reponseJsonUser, value);
            }
        }

        public CasinoViewModel(ConnexionApi connexionApi)
        {
            this.connexionApi = connexionApi;
            UserListe();
            TableListe();
            

        }
        // définition de la command déconexion
        private ICommand deconexionUtilisateur;
        public ICommand DeconexionUtilisateur
        {
            get
            {
                if (deconexionUtilisateur == null)
                {
                    deconexionUtilisateur = deconexionUtilisateur ?? (deconexionUtilisateur = new RelayCommand(obj => { UserDeconexion(); }));
                }
                return deconexionUtilisateur;
            }
        }

        private ICommand rejoindreTable;
        public ICommand RejoindreTable
        {
            get
            {
                if (rejoindreTable == null)
                {
                    rejoindreTable = rejoindreTable ?? (rejoindreTable = new RelayCommand(obj => { GoTable(obj); }));
                }
                return rejoindreTable;
            }
        }

        private ICommand userInformation;
        public ICommand UserInformation
        {
            get
            {
                if (userInformation == null)
                {
                    userInformation = userInformation ?? (userInformation = new RelayCommand(obj => { UserInfos(); }));
                }
                return userInformation;
            }
        }

        public async void UserDeconexion()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re");

                var json = JsonConvert.SerializeObject(this.connexionApi.user.email);
                json = "{\"email\":" + json + "}";

                var itemJson = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/api/auth/logout", itemJson);

                if (response.IsSuccessStatusCode)
                {
                    var message = new MessageDialog("Vous avez été déconnecté");
                    await message.ShowAsync();


                    actualFrame.Navigate(typeof(MainPage));


                }
                else
                {
                    var message = new MessageDialog(this.connexionApi.user.email);
                    await message.ShowAsync();
                }
            }
        }

        public async void UserListe()
        {
            using (var client = new HttpClient())
            {
                //http://rdonfack.developpez.com/tutoriels/dotnet/decouverte-asp-net-web-api/
                // https://docs.microsoft.com/en-us/dotnet/articles/csharp/tutorials/console-webapiclient
                // api / user / connected

                client.BaseAddress = new Uri("http://demo.comte.re/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.connexionApi.token.access_token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage req = new HttpRequestMessage();
                HttpResponseMessage response = await client.GetAsync("/api/user/connected");

                
                if (response.IsSuccessStatusCode)
                {


                    this.reponseJsonUser = response.Content.ReadAsStringAsync().Result;

                    User _user = new User();
                    JObject user = JObject.Parse(this.reponseJsonUser);
                    IList<JToken> res = user["users"].Children().ToList();

                    this.ListeUser = new ObservableCollection<User>();

                    foreach (JToken result in res)
                    {
                        _user = JsonConvert.DeserializeObject<User>(result.ToString());
                        this.ListeUser.Add(_user);
                    }
                  

                }
                else
                {
                    var message = new MessageDialog("Nous n'arrivons pas à charger les utilisateurs connectes");
                    await message.ShowAsync();
                    actualFrame.Navigate(typeof(MainPage));
                }

            }
        }
        
        public async void TableListe()
        {
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.connexionApi.token.access_token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage req = new HttpRequestMessage();
                HttpResponseMessage response = await client.GetAsync("/api/table/opened");

                
                if (response.IsSuccessStatusCode)
                {


                    this.reponseJson = response.Content.ReadAsStringAsync().Result;

                    Table _table = new Table();
                    JObject table = JObject.Parse(this.reponseJson);

                    IList<JToken> res = table["tables"].Children().ToList();

                    this.ListeDeTable = new ObservableCollection<Table>();

                    foreach (JToken result in res)
                    {
                        _table = JsonConvert.DeserializeObject<Table>(result.ToString());
                        this.ListeDeTable.Add(_table);
                    }


                }
                else
                {
                    var message = new MessageDialog("Nous n'arrivons pas à charger les tables disponibles");
                    await message.ShowAsync();
                    actualFrame.Navigate(typeof(MainPage));
                }

            }
        }

        //S'assoir à une table
        public async void GoTable(Object objetTable)
        {
            int idTable = (int)objetTable;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.connexionApi.token.access_token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage req = new HttpRequestMessage();
                HttpResponseMessage response = await client.GetAsync("/api/user/" + this.ConnexionApi.user.email + "/table/" + idTable + "/sit");


                if (response.IsSuccessStatusCode)
                {
                    var message = new MessageDialog("Bienvenue a la table " + idTable);
                    await message.ShowAsync();
                    //TableDeJeux
                    actualFrame.Navigate(typeof(MainPage));

                }
                else
                {
                    var message = new MessageDialog("Nous n'arrivons pas vous trouver une place a cette table");
                    await message.ShowAsync();
                   
                }

            }
        }

        //Obtenir les details de l'utilisateur
        public async void UserInfos()
        {
            //api/user/{email}
           
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.connexionApi.token.access_token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage req = new HttpRequestMessage();
                HttpResponseMessage response = await client.GetAsync("/api/user/" + this.ConnexionApi.user.email);


                if (response.IsSuccessStatusCode)
                {
                    this.reponseJsonInfos = response.Content.ReadAsStringAsync().Result;

                    var message = new MessageDialog(this.reponseJsonInfos);
                    await message.ShowAsync();
                    
                }
                else
                {
                    var message = new MessageDialog("Nous n'arrivons pas vous trouver vos informations");
                    await message.ShowAsync();

                }

            }
        }

        //Quitter une table
        public async void QuitterTable(int idTable)
        {
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.connexionApi.token.access_token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage req = new HttpRequestMessage();
                HttpResponseMessage response = await client.GetAsync("/api/user/" + this.ConnexionApi.user.email + "/table/" + idTable + "/leave");

                //api/user/{email}/table/{id}/leave
                if (response.IsSuccessStatusCode)
                {
                    var message = new MessageDialog("Vous avez quitter la table");
                    await message.ShowAsync();


                }
                else
                {
                    var message = new MessageDialog("Nous n'arrivons pas vous trouver une place a cette table");
                    await message.ShowAsync();

                }

            }
        }

        //Supprimer un utilisateur
        public async void SuprimerUtilisateur()
        {
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.connexionApi.token.access_token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage req = new HttpRequestMessage();
                HttpResponseMessage response = await client.DeleteAsync("/api/user/" + this.ConnexionApi.user.email);


                if (response.IsSuccessStatusCode)
                {
                    var message = new MessageDialog("Vous avez supprimer votre compte");
                    await message.ShowAsync();
                    actualFrame.Navigate(typeof(MainPage));

                }
                else
                {
                    var message = new MessageDialog("Nous n'avons pas réussit a supprimer votre compte");
                    await message.ShowAsync();

                }

            }
        }

        //Mettre à jour le stack de l'utilisateur
        public async void UpdateStackUser()
        {

        }
    }
}
