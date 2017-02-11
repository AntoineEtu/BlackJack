using ModeleClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BlackJack.ViewModel
{
    public class CasinoViewModel
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
                if (value != _listeDeTable)
                {
                    _listeDeTable = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("_listeDeTable"));
                    }
                }
            }
        }


        // définition de la command déconexion
        private ICommand deconexionUtilisateur;
        public ICommand DeconexionUtilisateur
        {
            get
            {
                if (deconexionUtilisateur == null)
                {
                    deconexionUtilisateur = deconexionUtilisateur ?? (deconexionUtilisateur = new RelayCommand(obj => { Deconexion(); }));
                }
                return deconexionUtilisateur;
            }
        }

        public async void Deconexion()
        {

        }
        public async void UserDeconexion(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re");

                var json = JsonConvert.SerializeObject(user.email);
                var itemJson = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/api/auth/logout", itemJson);
                if (response.IsSuccessStatusCode)
                {
                    var message = new MessageDialog("Vous avez été déconnecté");
                    await message.ShowAsync();


                    actualFrame.Navigate(typeof(MainPage));


                }
            }
        }

    }
}
