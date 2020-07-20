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
    public partial class PetsDetailPage : ContentPage
    {
        public PetsDetailPage()
        {
            InitializeComponent();

            BindingContext = new PetsDetailViewModel();
        }

        public PetsDetailPage(PetsModel petSelected)
        {
            InitializeComponent();

            BindingContext = new PetsDetailViewModel(petSelected);
        }
    }
}