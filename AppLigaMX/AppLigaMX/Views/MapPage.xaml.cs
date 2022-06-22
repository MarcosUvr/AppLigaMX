using AppLigaMX.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AppLigaMX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
        }

        public MapPage(Location location, PartidoModel partido)
        {
            InitializeComponent();
            partido.Latitud = location.Latitude;
            partido.Longitud = location.Longitude;

            MapControl.Partido = partido;

            MapControl.MoveToRegion(
               MapSpan.FromCenterAndRadius(
                   new Position(
                       location.Latitude,
                       location.Longitude
                       ), Distance.FromMiles(.5)
                   )
               ); 

            MapControl.Pins.Add(
                new Pin
                {
                    Type = PinType.Place,
                    Label = partido.Teams,
                    Position = new Position(
                        partido.Latitud,
                        partido.Longitud
                        )
                }
             );

            //Se ponen los valores de la marca y el Nombre de la sucursal
            EquiposLabel.Text = partido.Teams;
            HoraLabel.Text = partido.Hour;

        }

        
    }
}