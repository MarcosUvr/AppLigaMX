using Android.Gms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms;
using Android.Gms.Maps.Model;
using AppLigaMX.Renders;
using AppLigaMX.Droid.Renders;
using AppLigaMX.Models;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Maps;
using Android.Widget;
using Android.Graphics;
using Android.Content;
using AppLigaMX.ViewModels;

[assembly: ExportRenderer(typeof(MyMap), typeof(MyMapRenderer))]
namespace AppLigaMX.Droid.Renders
{
    public class MyMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        PartidoModel Partido; public MyMapRenderer(Context context) : base(context)
        { }
        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e); if (e.NewElement != null)
            {
                //Atrapamos el objeto de la mascota y lo dejamos en memoria
                this.Partido = (e.NewElement as MyMap).Partido;
            }
        }
        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map); NativeMap.SetInfoWindowAdapter(this);
        }
        protected override MarkerOptions CreateMarker(Pin pin)
        {
            //return base.CreateMarker(pin); //Agregar el pin personalizado
            var marker = new MarkerOptions();
            PartidosDetailViewModel mapas = new PartidosDetailViewModel();
            mapas.GetLocationCommand.Execute(mapas);
            //Console.WriteLine(mapas.Latitud + " " + mapas.Longitud);
            marker.SetPosition(new LatLng(mapas.Latitud, mapas.Longitud));
            marker.SetTitle(Partido.Teams);
            marker.SetSnippet(Partido.Hour);
            return marker;
        }
        public Android.Views.View GetInfoContents(Marker marker)
        {
            //Atrapamos el inyector
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            if (inflater != null)
            {
                //Inyectamo el control MapWindow
                Android.Views.View view;
                view = inflater.Inflate(Resource.Layout.MapWindow, null); //Apuntamos a los controle
                var infoImage = view.FindViewById<ImageView>(Resource.Id.MapWindowImage);
                var infoName = view.FindViewById<TextView>(Resource.Id.MapWindowTeams);
                var infoBreedAge = view.FindViewById<TextView>(Resource.Id.MapWindowHour); //Asignamos los valores correspondientes de la mascota a cada control
                if (infoImage != null) infoImage.SetImageBitmap(BitmapFactory.DecodeFile(Partido.Picture));
                if (infoName != null) infoName.Text = Partido.Teams;
                if (infoBreedAge != null) infoBreedAge.Text = Partido.Hour; //Regresamos la vista
                return view;
            }
            return null;
        }
        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }
    }
}

