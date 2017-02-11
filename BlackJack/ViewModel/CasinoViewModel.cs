using ModeleClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


    }
}
