using IoT_Project_Food_Ordering.DTOs;
using IoT_Project_Food_Ordering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IoT_Project_Food_Ordering.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsView : ContentPage
    {
        public ProductsView()
        {
            InitializeComponent();
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var category = e.CurrentSelection.FirstOrDefault() as Category;
            if (category == null)
                return;

            await Navigation.PushModalAsync(new CategoryView(category));

            ((CollectionView)sender).SelectedItem = null;
        }

        private async void CollectionView_SelectionChanged_FoodItem(object sender, SelectionChangedEventArgs e)
        {
            var selectedRecomendation = e.CurrentSelection.FirstOrDefault() as FoodItemWithCategoryImage;

            if (selectedRecomendation == null)
                return;

            ((CollectionView)sender).SelectedItem = null;

            await Navigation.PushModalAsync(new ProductDetailsView(new FoodItem
            {
                ProductID = selectedRecomendation.ProductID,
                ImageUrl = selectedRecomendation.ImageUrl,
                Name = selectedRecomendation.Name,
                Description = selectedRecomendation.Description,
                Rating = selectedRecomendation.Rating,
                HomeSelected = selectedRecomendation.HomeSelected,
                Price = selectedRecomendation.Price,
                CategoryID = selectedRecomendation.CategoryID
            }));
        }

        /*private void Button_Clicked(object sender, EventArgs e)
        {
            var selectedRecomendation = e. as FoodItem;

            if (selectedRecomendation == null)
                return;

            ((CollectionView)sender).SelectedItem = null;

            await Navigation.PushModalAsync(new ProductDetailsView(selectedRecomendation));
        }*/
    }
}