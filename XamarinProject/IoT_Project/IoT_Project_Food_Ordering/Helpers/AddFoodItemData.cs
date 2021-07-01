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
    public class AddFoodItemData
    {
        FirebaseClient client;

        public List<FoodItem> FoodItems { get; set; }

        public AddFoodItemData()
        {
            client = new FirebaseClient("https://iotprojectfoodordering-default-rtdb.europe-west1.firebasedatabase.app/");
            FoodItems = new List<FoodItem>()
            {
                new FoodItem
                {
                    ProductID = 1,
                    CategoryID = 1,
                    ImageUrl = "MainDish",
                    Name = "Pierogi with Meat",
                    Description = "If you are hungry, check this out!",
                    Rating = " 4.8",
                    RatingDetail = "(121 raitings)",
                    HomeSelected = "CompleteHeart",
                    Price = 45
                },
                new FoodItem
                {
                    ProductID = 2,
                    CategoryID = 1,
                    ImageUrl = "MainDish",
                    Name = "Pierogi with Vegetables",
                    Description = "Suitable for vegans.",
                    Rating = " 4.2",
                    RatingDetail = " (121 raitings)",
                    HomeSelected = "CompleteHeart",
                    Price = 45
                },
                new FoodItem
                {
                    ProductID = 3,
                    CategoryID = 4,
                    ImageUrl = "MainPizza",
                    Name = "Pizza",
                    Description = "A crispy stone-baked base is the foundation for velvety tomato sauce and melted mozzarella cheese.",
                    Rating = " 4.4",
                    RatingDetail = " (120 raitings)",
                    HomeSelected = "CompleteHeart",
                    Price = 45
                },
                new FoodItem
                {
                    ProductID = 4,
                    CategoryID = 2,
                    ImageUrl = "MainDessert2",
                    Name = "Cupcakes",
                    Description = "Cupcakes with a bit of honey and strawberries.",
                    Rating = " 4.8",
                    RatingDetail = " (156 raitings)",
                    HomeSelected = "EmptyHeart",
                    Price = 45
                },
                new FoodItem
                {
                    ProductID = 5,
                    CategoryID = 2,
                    ImageUrl = "MainDessert",
                    Name = "Muffin",
                    Description = "This banana muffin recipe goes over so well with kids.",
                    Rating = " 4.4",
                    RatingDetail = " (120 raitings)",
                    HomeSelected = "CompleteHeart",
                    Price = 45
                },
                new FoodItem
                {
                    ProductID = 6,
                    CategoryID = 3,
                    ImageUrl = "MainSoup",
                    Name = "Potato Soup",
                    Description = "Potato Soup loaded with chunks of tender, hearty potatoes and made with a rich and creamy soup base.",
                    Rating = " 4.8",
                    RatingDetail = " (156 raitings)",
                    HomeSelected = "EmptyHeart",
                    Price = 45
                }
             };
        }

        public async Task AddFoodItemAsync()
        {
            try
            {
                foreach (var item in FoodItems)
                {
                    await client.Child("FoodItems").PostAsync(new FoodItem()
                    {
                        CategoryID = item.CategoryID,
                        ProductID = item.ProductID,
                        Description = item.Description,
                        HomeSelected = item.HomeSelected,
                        ImageUrl = item.ImageUrl,
                        Name = item.Name,
                        Price = item.Price,
                        Rating = item.Rating,
                        RatingDetail = item.RatingDetail
                    });
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
