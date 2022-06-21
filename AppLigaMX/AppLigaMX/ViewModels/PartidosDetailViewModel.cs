using AppLigaMX.Models;
using AppLigaMX.Services;
using AppLigaMX.Views;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppLigaMX.ViewModels
{
    public class PartidosDetailViewModel : BaseViewModel
    {
        // VARIABLES GLOBALES 
        public readonly PartidosListViewModel ListViewModel;
        static PartidosDetailViewModel instance = new PartidosDetailViewModel();

        //COMANDOS
        private Command _CancelCommand;
        public Command CancelCommand => _CancelCommand ?? (_CancelCommand = new Command(CancelAction));

        private Command _SaveCommand;
        public Command SaveCommand => _SaveCommand ?? (_SaveCommand = new Command(SaveAction));

        private Command _DeleteCommand;
        public Command DeleteCommand => _DeleteCommand ?? (_DeleteCommand = new Command(DeleteAction));

        private Command _TakePictureCommand;
        public Command TakePictureCommand => _TakePictureCommand ?? (_TakePictureCommand = new Command(TakePictureAction));

        private Command _SelectPictureCommand;
        public Command SelectPictureCommand => _SelectPictureCommand ?? (_SelectPictureCommand = new Command(SelectPictureAction));

        Command _GetLocationCommand;
        public Command GetLocationCommand => _GetLocationCommand ?? (_GetLocationCommand = new Command(GetLocationAction));

        private Command _MapCommand;
        public Command MapCommand => _MapCommand ?? (_MapCommand = new Command(MapAction));

        //PROPIEDADES
        private PartidoModel _PartidoSelected;
        public PartidoModel PartidoSelected
        {
            get => _PartidoSelected;
            set => SetProperty(ref _PartidoSelected, value);
        }

        private int _PartidoID;
        public int PartidoID
        {
            get => _PartidoID;
            set => SetProperty(ref _PartidoID, value);
        }

        private string _PartidoTeams;
        public string PartidoTeams
        {
            get => _PartidoTeams;
            set => SetProperty(ref _PartidoTeams, value);
        }

        private string _PartidoPicture;
        public string PartidoPicture
        {
            get => _PartidoPicture;
            set => SetProperty(ref _PartidoPicture, value);
        }

        private string _PartidoHour;
        public string PartidoHour
        {
            get => _PartidoHour;
            set => SetProperty(ref _PartidoHour, value);
        }

        private double _Latitud;
        public double Latitud
        {
            get => _Latitud;
            set => SetProperty(ref _Latitud, value);
        }

        private double _Longitud;
        public double Longitud
        {
            get => _Longitud;
            set => SetProperty(ref _Longitud, value);
        }

        //MÉTODOS CONSTRUCTORES
        public PartidosDetailViewModel()
        {
            //Si se hace un nuevo registro de un partido, se instancía
            PartidoSelected = new PartidoModel();
            
        }

        public PartidosDetailViewModel(PartidoModel partidoSelected)
        {
            //Se carga el registro del partido si ya existe
            PartidoSelected = partidoSelected;
            PartidoPicture = partidoSelected.Picture64;
            
        }

        //public PartidosDetailViewModel(PartidosListViewModel partidoListViewModel, PartidoModel partido)
        //{
        //    this.ListViewModel = partidoListViewModel;

        //    PartidoID = partido.ID;
        //    PartidoTeams = partido.Teams;
        //    PartidoPicture = partido.Picture64;
        //    PartidoHour = partido.Hour;
        //}

        //MÉTODOS
        private async void SaveAction()
        {
            GetLocationCommand.Execute(instance);
            ApiResponse response;
            try
            {
                // Iniciamos el spinner
                IsBusy = true;

                // Creamos el modelo con los datos de los controles
                PartidoModel model = new PartidoModel
                {
                    ID = PartidoID,
                    Teams = PartidoSelected.Teams,
                    Picture64 = PartidoPicture,
                    Hour = PartidoSelected.Hour,
                    Latitud = instance.Latitud,
                    Longitud = instance.Longitud
                };

                if (model.ID == 0)
                {
                    // Crear un nuevo producto
                    response = await new ApiService().PostDataAsync("Partido", model);
                }
                else
                {
                    // Actualizar un producto existente
                    response = await new ApiService().PutDataAsync("Partido", model);
                }

                // Si no fue satisfactorio enviamos un mensaje y terminamos el método
                if (response == null || !response.IsSuccess)
                {
                    IsBusy = false;
                    await Application.Current.MainPage.DisplayAlert("AppLigaMX", response.Message, "Ok");
                    return;
                }

                // Actualizamos el listado de partidos
                PartidosListViewModel.GetInstance().RefreshPartidos();

                // Cerramos la vista actual
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("AppProducts", $"Se generó un error al guardar el producto: {ex.Message}", "Ok");
            }
        }

        private async void DeleteAction()
        {
            ApiResponse response;
            try
            {
                // Iniciamos el spinner
                IsBusy = true;
                // Creamos el modelo con los datos de los controles
                PartidoModel model = new PartidoModel
                {
                    ID = PartidoID,
                };
                // Actualizar un producto existente
                response = await new ApiService().DeleteDataAsync("Partido", model.ID);

                // Si no fue satisfactorio enviamos un mensaje y terminamos el método
                if (response == null || !response.IsSuccess)
                {
                    IsBusy = false;
                    await Application.Current.MainPage.DisplayAlert("AppLigaMX", response.Message, "Ok");
                    return;
                }

                // Actualizamos el listado de productos
                PartidosListViewModel.GetInstance().RefreshPartidos();

                // Cerramos la vista actual
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("AppProducts", $"Se generó un error al borrar el producto: {ex.Message}", "Ok");
            }
        }

        private async void CancelAction()
        {
            //Volvemmos a el listado inicial
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void TakePictureAction()
        {
            //Se inicializa la cámara
            await CrossMedia.Current.Initialize();

            //Se manda un mensaje en dado caso que la cámara no se encuentre disponible
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            //Se toma la fotografía y nos refresan el file
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "AppLigaMX",
                Name = "fotografia_partido.jpg"
            });

            if (file == null) return;

            //Se asigna la ruta de la fotografía
            PartidoPicture = PartidoSelected.Picture64 = await new ImageService().ConvertImageFileToBase64(file.Path);
        }

        private async void SelectPictureAction()
        {
            //Se inicializa la cámara
            await CrossMedia.Current.Initialize();

            //Se manda un mensaje en dado caso que la cámara no se encuentre disponible
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Pick Photo", ":( No pick photo available.", "OK");
                return;
            }

            //Se selecciona la fotografía y nos refresan el file
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });

            if (file == null) return;

            //Se asigna la ruta de la fotografía
            PartidoPicture = PartidoSelected.Picture64 = await new ImageService().ConvertImageFileToBase64(file.Path);
        }

        private async void GetLocationAction()
        {
            try
            {
                var location = await Geolocation.GetLocationAsync();
                if (location != null)
                {
                    //Se guarda la latitud y la longitud
                    Latitud = location.Latitude;
                    Longitud = location.Longitude;
                }
                else
                {
                    //Se manda un mensaje en dado caso de que no se pudo obtener la locación
                    Application.Current.MainPage.DisplayAlert("AppLigaMX", "No se pudo obtener la ubicación", "Ok");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

        private async void MapAction()
        {
            //Se abre la pestaña donde se muestra el mapa
            try
            {
                var partido = new PartidoModel();
                var location = await Geolocation.GetLocationAsync(); //GetLastKnownLocationAsync();
                if (location != null)
                {

                    Application.Current.MainPage.Navigation.PushAsync(new MapPage(location, PartidoSelected));
                }
                else
                {
                    //Se manda un mensaje en dado caso de que no se pudo obtener la locación
                    Application.Current.MainPage.DisplayAlert("AppLigaMX", "No se pudo obtener la ubicación", "Ok");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
    }
}
