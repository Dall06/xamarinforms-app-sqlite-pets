﻿using AppXamarinPets.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace AppXamarinPets.UWP
{
    public sealed partial class MarkerWindow : UserControl
    {
        public MarkerWindow(PetsModel pet)
        {
            this.InitializeComponent();
            
            MarkerWindowImage.Source = new BitmapImage(new Uri(pet.PictureFileBase64));
            MarkerWindowName.Text = pet.Name;
        }
    }
}
