using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppXamarinPets.Droid.Renderers;
using AppXamarinPets.Models;
using AppXamarinPets.Renderers;
using Java.IO;
using Java.Lang;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace AppXamarinPets.Droid.Renderers
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        PetsModel Pet;

        public CustomMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                this.Pet = ((CustomMap)e.NewElement).Pet;
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            NativeMap.SetInfoWindowAdapter(this);
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var marker = new MarkerOptions();
            marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin));
            marker.SetPosition(new LatLng(Pet.Latitude, Pet.Longitude));
            marker.SetTitle(Pet.Name);
            return marker;
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            if (inflater != null)
            {
                Android.Views.View view;
                view = inflater.Inflate(Resource.Layout.MarkerWindow, null);
                var infoImage = view.FindViewById<ImageView>(Resource.Id.MarkerWindowImage);
                var infoTitle = view.FindViewById<TextView>(Resource.Id.MarkerWindowName);

                if (infoImage != null) infoImage.SetImageBitmap(BitmapFactory.DecodeFile(Pet.PictureFileBase64));
                if (infoTitle != null) infoTitle.Text = Pet.Name;

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