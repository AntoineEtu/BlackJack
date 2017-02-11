﻿using BlackJack.ViewModel;
using ModeleClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BlackJack
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            //test de création de user
            ApiCreateOrUpdate apiResponse = new ApiCreateOrUpdate();
            User user1 = new User("FishBluff","Antoine","Keribin","antoine.keribin@ynov.com","motdepasse123");
            apiResponse.ApiCreateUser(user1);
            //bonjour
            //salut c'est le commentaire d'Antonio
        }
    }
}
