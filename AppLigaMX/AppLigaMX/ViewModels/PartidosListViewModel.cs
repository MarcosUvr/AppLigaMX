﻿using AppLigaMX.Models;
using AppLigaMX.Services;
using AppLigaMX.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppLigaMX.ViewModels
{
    public class PartidosListViewModel : BaseViewModel
    {
        //VARIABLES
        static PartidosListViewModel instance;

        //COMANDOS
        private Command _NewCommand;
        public Command NewCommand => _NewCommand ?? (_NewCommand = new Command(NewAction));

        //PROPIEDADESSe generó un error al crear el partido
        private List<PartidoModel> _ListPartidos;
        public List<PartidoModel> ListPartidos
        {
            get => _ListPartidos;
            set => SetProperty(ref _ListPartidos, value);
        }
        
        private PartidoModel _PartidoSelected;
        public PartidoModel PartidoSelected
        {
            get => _PartidoSelected;
            set
            {
                if (SetProperty(ref _PartidoSelected, value))
                {
                    SelectPartidoAction();
                }
            }
        }

        private string _PartidoPicture;
        public string PartidoPicture
        {
            get => _PartidoPicture;
            set => SetProperty(ref _PartidoPicture, value);
        }

        //MÉTODOS CONSTRUCTORES
        public PartidosListViewModel()
        {
            instance = this;

            //Se cargan los registros de los partidos
            LoadPartidos();
        }

        //MÉTODOS CONSTRUCTORES
        public static PartidosListViewModel GetInstance()
        {
            return instance;
        }

        // Métodos
        public async void LoadPartidos()
        {
            IsBusy = true;
            ListPartidos = null;
            ApiResponse response = await new ApiService().GetDataAsync("Partido");
            if (response == null || !response.IsSuccess)
            {
                // No hubo una respuesta exitosa
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("AppLigaMX", $"Error al cargar los partidos: {response.Message}", "Ok");
                return;
            }
            ListPartidos = JsonConvert.DeserializeObject<List<PartidoModel>>(response.Result.ToString());
            IsBusy = false;
        }

        public void RefreshPartidos()
        {
            LoadPartidos();
        }

        private void NewAction()
        {
            //Se abre la pestaña de detalle si el registro es nuevo
            Application.Current.MainPage.Navigation.PushAsync(new PartidosDetailPage());
        }

        private void SelectPartidoAction()
        {
            //Se abre la pestaña de detalle si se seleccionó un registro
            Application.Current.MainPage.Navigation.PushAsync(new PartidosDetailPage(PartidoSelected));
        }
    }
}
