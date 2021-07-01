using IoT_Project_Food_Ordering.Models;
using IoT_Project_Food_Ordering.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IoT_Project_Food_Ordering.Views
{
    public partial class ProductDetailsView : ContentPage
    {
        ProductDetailsViewModel productDetailsVM;
        public ProductDetailsView(FoodItem foodItem)
        {
            InitializeComponent();
            productDetailsVM = new ProductDetailsViewModel(foodItem);
            this.BindingContext = productDetailsVM;
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}