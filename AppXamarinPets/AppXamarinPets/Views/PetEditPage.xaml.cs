using AppXamarinPets.Models;
using AppXamarinPets.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppXamarinPets.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PetEditPage : ContentPage
    {
        public PetEditPage()
        {
            InitializeComponent();

            BindingContext = new PetEditViewModel();
        }

        public PetEditPage(PetsModel petSelected)
        {
            InitializeComponent();

            BindingContext = new PetEditViewModel(petSelected);
        }
    }
}