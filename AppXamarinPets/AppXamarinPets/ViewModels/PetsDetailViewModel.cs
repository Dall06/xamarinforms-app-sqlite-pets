using AppXamarinPets.Models;
using AppXamarinPets.Views;
using Xamarin.Forms;

namespace AppXamarinPets.ViewModels
{
    public class PetsDetailViewModel : BaseViewModel
    {
        /*********************COMMANDS***********************/
        Command deleteCommand;
        public Command DeleteCommand => deleteCommand ?? (deleteCommand = new Command(DeleteAction));

        Command editCommand;
        public Command EditCommand => editCommand ?? (editCommand = new Command(EditAction));

        Command cancelCommand;
        public Command CancelCommand => cancelCommand ?? (cancelCommand = new Command(CancelAction));

        Command _mapCommand;
        public Command MapCommand => _mapCommand ?? (_mapCommand = new Command(MapAction));


        /*********************BINDABLE PROPS***********************/
        string _ImageUrl;
        public string ImageUrl
        {
            get => _ImageUrl;
            set => SetProperty(ref _ImageUrl, value);
        }

        PetsModel petSelected;
        public PetsModel PetSelected
        {
            get => petSelected;
            set => SetProperty(ref petSelected, value);
        }

        /*********************CONSTRUCTORS***********************/
        public PetsDetailViewModel()
        {
            PetSelected = new PetsModel();
        }

        public PetsDetailViewModel(PetsModel petSelected)
        {
            PetSelected = petSelected;
            ImageUrl = PetSelected.PictureFileBase64;
        }

        /*********************ACTIONS***********************/
        private async void CancelAction()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void DeleteAction()
        {
            //Se borra tarea
            await App.PetsDatabase.DeletePetsAsync(PetSelected);
            //Refresca pantalla
            PetsViewModel.GetInstance().LoadPets();
            //Cierra ventan
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void EditAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new PetEditPage(PetSelected));
        }

        private void MapAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new PetMapPage(new PetsModel
            {
                ID = PetSelected.ID,
                Name = PetSelected.Name,
                PictureFileBase64 = PetSelected.PictureFileBase64,
                Gender = PetSelected.Gender,
                DogBreed = PetSelected.DogBreed,
                Comments = PetSelected.Comments,
                Latitude = PetSelected.Latitude,
                Longitude = PetSelected.Longitude
            }));
        }
    }
}
