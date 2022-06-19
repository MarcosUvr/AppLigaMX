using AppLigaMX.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppLigaMX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartidosListPage : ContentPage
    {
        public PartidosListPage()
        {
            InitializeComponent();

            BindingContext = new PartidosListViewModel(); //Se determina donde se hace el binding
        }
    }
}