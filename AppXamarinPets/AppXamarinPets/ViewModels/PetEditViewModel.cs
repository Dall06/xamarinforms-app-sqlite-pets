using AppXamarinPets.Models;
using AppXamarinPets.Services;
using AppXamarinPets.Views;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppXamarinPets.ViewModels
{
    public class PetEditViewModel : BaseViewModel
    {
        /*********************COMMANDS***********************/
        Command saveCommand;
        public Command SaveCommand => saveCommand ?? (saveCommand = new Command(SaveAction));

        Command _TakePictureCommand;
        public Command TakePictureCommand => _TakePictureCommand ?? (_TakePictureCommand = new Command(TakePictureAction));

        Command _SelectPictureCommand;
        public Command SelectPictureCommand => _SelectPictureCommand ?? (_SelectPictureCommand = new Command(SelectPictureAction));

        Command cancelCommand;
        public Command CancelCommand => cancelCommand ?? (cancelCommand = new Command(CancelAction));

        Command _GetLocationCommand;
        public Command GetLocationCommand => _GetLocationCommand ?? (_GetLocationCommand = new Command(GetLocationAction));

        /*********************BINDABLE PROPS***********************/
        PetsModel petSelected;
        public PetsModel PetSelected
        {
            get => petSelected;
            set => SetProperty(ref petSelected, value);
        }

        string _ImageUrl;
        public string ImageUrl
        {
            get => _ImageUrl;
            set => SetProperty(ref _ImageUrl, value);
        }

        double _Latitude;
        public double Latitude
        {
            get => _Latitude;
            set => SetProperty(ref _Latitude, value);
        }

        double _Longitude;
        public double Longitude
        {
            get => _Longitude;
            set => SetProperty(ref _Longitude, value);
        }

        /*********************CONSTRUCTORS***********************/
        public PetEditViewModel()
        {
            PetSelected = new PetsModel();
        }

        public PetEditViewModel(PetsModel petSelected)
        {
            PetSelected = petSelected;
            ImageUrl = petSelected.PictureFileBase64;
            Latitude = petSelected.Latitude;
            Longitude = petSelected.Longitude;
        }

        /*********************ACTIONS***********************/
        private async void SaveAction()
        {

            PetSelected.Latitude = Latitude;
            PetSelected.Longitude = Longitude;

            if(ValidationFunction() == false)
            {
                await Application.Current.MainPage.DisplayAlert("No Data", "Insert a valid Information", "OK");
            }
            else
            {
                await App.PetsDatabase.SavePetsAsync(PetSelected);

                PetsViewModel.GetInstance().LoadPets();

                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }

        private async void CancelAction()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void TakePictureAction()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                await CrossMedia.Current.Initialize();
            }
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null) return;

            //Se hace la conversion de la ruta temporal de la imagen a base 64 para obtener el archivo
            if (!string.IsNullOrEmpty(file.Path))
            {
                petSelected.PictureFileBase64 = await new ImageService().ConvertImageFileToBase64(file.Path);
                ImageUrl = petSelected.PictureFileBase64;
            }
        }

        private async void SelectPictureAction()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                await CrossMedia.Current.Initialize();
            }
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Not supported", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });

            if (file == null) return;

            //Se hace la conversion de la ruta del archivo local a base 64 para almacenar
            if (!string.IsNullOrEmpty(file.Path))
            {
                petSelected.PictureFileBase64 = await new ImageService().ConvertImageFileToBase64(file.Path);
                ImageUrl = petSelected.PictureFileBase64;
            }
        }

        private async void GetLocationAction()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    Latitude = location.Latitude;
                    Longitude = location.Longitude;
                }
            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException)
            {
                // Handle permission exception
            }
            catch (Exception)
            {
                // Unable to get location
            }
        }

        /*********************FUNCTIONS***********************/
        private bool ValidationFunction()
        {
            if(
                (PetSelected.Latitude == 0) || (PetSelected.Longitude == 0) ||
                (PetSelected.Name == "" || PetSelected.Name == null) ||
                (PetSelected.Gender == "" || PetSelected.Gender == null)
              )
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
