using Firebase.Database;
using Firebase.Database.Query;
using IoT_Project_Food_Ordering.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IoT_Project_Food_Ordering.Helpers
{
    public class AddCategoryData
    {
        FirebaseClient client;
        public List<Category> Categories { get; set; }

        public AddCategoryData()
        {
            client = new FirebaseClient("https://iotprojectfoodordering-default-rtdb.europe-west1.firebasedatabase.app/");
            Categories = new List<Category>()
             {
                 new Category()
                 {
                     CategoryID=1,
                     CategoryName="Main Dish",
                     CategoryPoster="MainDish",
                     ImageUrl="Dish"
                 },
                 new Category()
                 {
                     CategoryID=2,
                     CategoryName="Dessert",
                     CategoryPoster="MainDessert",
                     ImageUrl="Dessert"
                 },
                 new Category()
                 {
                     CategoryID=3,
                     CategoryName="Soup",
                     CategoryPoster="MainSoup",
                     ImageUrl="Soup"
                 },
                 new Category()
                 {
                     CategoryID=4,
                     CategoryName="Pizza",
                     CategoryPoster="MainPizza",
                     ImageUrl="Pizza"
                 }
             };
        }

        public async Task AddCategoriesAsync()
        {
            try
            {
                foreach (Category item in Categories)
                {
                    await client.Child("Categories").PostAsync(new Category()
                    {
                        CategoryID = item.CategoryID,
                        CategoryName = item.CategoryName,
                        CategoryPoster = item.CategoryPoster,
                        ImageUrl = item.ImageUrl
                    });
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message,"Ok");
            }
        }
    }
}
