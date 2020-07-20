using AppXamarinPets.Models;
using AppXamarinPets.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppXamarinPets.ViewModels
{
    public class PetsViewModel : BaseViewModel
    {
        /*********************VARIABLES/INSTANCES***********************/
        static PetsViewModel instance;

        /*********************COMMANDS***********************/
        Command newPetCommand;
        public Command NewPetCommand => newPetCommand ?? (newPetCommand = new Command(NewPetAction));

        /*********************BINDABLE PROPS***********************/
        List<PetsModel> pets;
        public List<PetsModel> Pets
        {
            get => pets;
            set => SetProperty(ref pets, value);
        }

        PetsModel petSelected;
        public PetsModel PetSelected
        {
            get => petSelected;
            set
            {
                if (SetProperty(ref petSelected, value))
                {
                    SelectPetAction();
                }
            }
        }

        /*********************CONSTRUCTORS***********************/
        public PetsViewModel()
        {
            instance = this;

            LoadPets();
        }

        /*********************FUNCTIONS***********************/
        public static PetsViewModel GetInstance()
        {
            if (instance == null) instance = new PetsViewModel();
            return instance;
        }

        public async void LoadPets()
        {
            Pets = await App.PetsDatabase.GetAllPetsAsync();
        }

        /*********************ACTIONS***********************/
        private void NewPetAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new PetEditPage());
        }

        private void SelectPetAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new PetsDetailPage(PetSelected));
        }
    }
}
