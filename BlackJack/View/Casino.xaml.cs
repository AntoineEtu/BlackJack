﻿using BlackJack.ViewModel;
using ModeleClasses;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace BlackJack.View
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Casino : Page
    {
        
        public CasinoViewModel CasinoViewModel;
        public Casino()
        {
            this.InitializeComponent();
        }
       
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CasinoViewModel = new CasinoViewModel((ConnexionApi)e.Parameter);
            
            this.DataContext = CasinoViewModel;
        }

    }
}
