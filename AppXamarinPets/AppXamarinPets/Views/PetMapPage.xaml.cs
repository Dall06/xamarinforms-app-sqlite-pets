using AppXamarinPets.Models;
using AppXamarinPets.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AppXamarinPets.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PetMapPage : ContentPage
    {
        public PetMapPage(PetsModel petSelected)
        {
            InitializeComponent();

            MapPet.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(petSelected.Latitude, petSelected.Longitude),
                    Distance.FromMiles(.5)
            ));

            string imagePath = new ImageService().SaveImageFromBase64(petSelected.PictureFileBase64, petSelected.ID);
            petSelected.PictureFileBase64 = imagePath;

            MapPet.Pet = petSelected;

            MapPet.Pins.Add(
                new Pin
                {
                    Type = PinType.Place,
                    Label = petSelected.Name,
                    Position = new Position(petSelected.Latitude, petSelected.Longitude)
                }
            );

            Name.Text = petSelected.Name;
            Gender.Text = petSelected.Gender;
            DogBreed.Text = $"Dog Breed: {petSelected.DogBreed}";
            Comments.Text = petSelected.Comments;
        }
    }
}