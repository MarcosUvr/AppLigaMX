using AppLigaMX.Models;
using AppLigaMX.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppLigaMX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartidosDetailPage : ContentPage
    {
        public PartidosDetailPage()
        {
            InitializeComponent();


            BindingContext = new PartidosDetailViewModel(); //Se determina donde se hace el binding
            PartidosDetailViewModel mapas = new PartidosDetailViewModel();
            mapas.GetLocationCommand.Execute(mapas);
            Console.WriteLine(mapas.Latitud + " " + mapas.Longitud);
        }

        public PartidosDetailPage(PartidoModel partidoSelected)
        {
            InitializeComponent();

            BindingContext = new PartidosDetailViewModel(partidoSelected); //Se determina donde se hace el binding
        }
    }
}