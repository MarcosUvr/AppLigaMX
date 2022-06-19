using AppLigaMX.Models;
using AppLigaMX.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppLigaMX
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Asignamos colores desde los recursos de App.xaml y abrimos la página del listado de tareas
            NavigationPage nav = new NavigationPage(new PartidosListPage());
            nav.BarBackgroundColor = (Color)App.Current.Resources["ColorAzul"];
            nav.BarTextColor = (Color)App.Current.Resources["ColorBlanco"];
            MainPage = nav;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        internal static Task SavePartidosAsync(PartidoModel partidoSelected)
        {
            throw new NotImplementedException();
        }
    }
}
