using BlackJack.View;
using ModeleClasses;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class JeuxViewModel : ViewModel
    {
        Frame actualFrame { get { return Window.Current.Content as Frame; } }
        public event PropertyChangedEventHandler PropertyChanged;


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


        private ICommand exitTable;
        public ICommand ExitTable
        {
            get
            {
                if (exitTable == null)
                {

                    exitTable = exitTable ?? (exitTable = new RelayCommand(obj => { QuitterT(); }));

                }
                return exitTable;
            }
        }

        public JeuxViewModel(ConnexionApi connexionApi)
        {
            this.connexionApi = connexionApi;
          


        }

        public void QuitterT()
        {
            
            QuitterTable(connexionApi.tableUtiliser.Id);


        }
        //Mettre à jour le stack de l'utilisateur
        public async void UpdateStackUser(double stack)
        {
            ///api/user/{email}/stack/{amount}

            using (var client = new HttpClient())
            {


                client.BaseAddress = new Uri("http://demo.comte.re/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.connexionApi.token.access_token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseAPI = await client.GetAsync("/api/" + connexionApi.user.email + "/stack/" + stack);



                if (responseAPI.IsSuccessStatusCode)
                {
                    var reponse = await responseAPI.Content.ReadAsStringAsync();
                    var objectJson = JObject.Parse(reponse);

                    var messageObject = objectJson["message"];
                    var userObject = objectJson["user"];

                    connexionApi.user.stack = double.Parse(userObject["stack"].ToString());


                    var message = new MessageDialog("Vous avez quitter la table");
                    await message.ShowAsync();

                    



                }
                else
                {
                   
                    var dialog = new MessageDialog("Vous ne pouvez pas quitter la table");
                    await dialog.ShowAsync();
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
                    connexionApi.tableUtiliser = new Table();
                    actualFrame.Navigate(typeof(Casino), this.connexionApi);
                }
                else
                {
                    var message = new MessageDialog("Nous n'arrivons pas vous trouver une place a cette table");
                    await message.ShowAsync();

                }

            }
        }

    }
}
