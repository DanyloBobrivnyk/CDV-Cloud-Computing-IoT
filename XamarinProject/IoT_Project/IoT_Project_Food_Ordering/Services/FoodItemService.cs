using Firebase.Database;
using Firebase.Database.Query;
using IoT_Project_Food_Ordering.DTOs;
using IoT_Project_Food_Ordering.DTOs.API_Dto;
using IoT_Project_Food_Ordering.Helpers;
using IoT_Project_Food_Ordering.Models;
using Lab1_bobrivnyk.Rest.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IoT_Project_Food_Ordering.Services
{
    public class FoodItemService
    {
        FirebaseClient dbClient;
        HttpClient httpClient;

        public FoodItemService()
        {
            dbClient = new FirebaseClient("https://iotprojectfoodordering-default-rtdb.europe-west1.firebasedatabase.app/");
            httpClient = new HttpClient();
        }

        public async Task<List<FoodItem>> GetFoodItemsAsync()
        {
            /*Uri uri = new Uri(ConnectionClass.connectionString + "/FoodItems");

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                
                var responce = ConnectionClass.GetResponseFromRequest(uri.ToString(), ref httpWebRequest);

                var data = JsonConvert.DeserializeObject<List<FoodItem>>(responce);
                
                return data;
            }
            else
            {
                return null;
            }*/

            var products = (await dbClient.Child("FoodItems")
                 .OnceAsync<FoodItem>())
                 .Select(f => new FoodItem
                 {
                     CategoryID = f.Object.CategoryID,
                     Description = f.Object.Description,
                     HomeSelected = f.Object.HomeSelected,
                     ImageUrl = f.Object.ImageUrl,
                     Name = f.Object.Name,
                     Price = f.Object.Price,
                     ProductID = f.Object.ProductID,
                     Rating = f.Object.Rating,
                     RatingDetail = f.Object.RatingDetail
                 }).ToList();
            return products;
        }

        public async Task<ObservableCollection<FoodItem>> GetFoodItemsByCategoryAsync(int categoryID)
        {
            var foodItemsByCategory = new ObservableCollection<FoodItem>();
            var items = (await GetFoodItemsAsync()).Where(p => p.CategoryID == categoryID).ToList();
            foreach (var item in items)
            {
                foodItemsByCategory.Add(item);
            }
            return foodItemsByCategory;
        }
        public async Task<ObservableCollection<FoodItem>> GetLatestFoodItemsAsync()
        {
            var latestFoodItems = new ObservableCollection<FoodItem>();
            var items = (await GetFoodItemsAsync()).OrderByDescending(f => f.ProductID).Take(5);
            foreach (var item in items)
            {
                latestFoodItems.Add(item);
            }
            return latestFoodItems;
        }

        public async Task<ObservableCollection<FoodItemWithCategoryImage>> GetLatestFoodItemsWithCategoryAsync()
        {
            var latestFoodItemsWithCategoryImage = new ObservableCollection<FoodItemWithCategoryImage>();

            var latestFoodItems = new ObservableCollection<FoodItem>();
            var items = (await GetFoodItemsAsync()).OrderByDescending(f => f.ProductID).Take(5);
            foreach (var item in items)
            {
                var currentImage = (await dbClient.Child("Categories").OnceAsync<Category>())
                        .Where(o => o.Object.CategoryID.Equals(item.CategoryID))
                        .Select(o => new Category
                        {
                            CategoryID = o.Object.CategoryID,
                            CategoryName = o.Object.CategoryName,
                            CategoryPoster = o.Object.CategoryPoster,
                            ImageUrl = o.Object.ImageUrl
                        }).ToList();

                var image = currentImage.Last().ImageUrl;

                latestFoodItemsWithCategoryImage.Add(new FoodItemWithCategoryImage
                {
                    ProductID = item.ProductID,
                    ImageUrl = item.ImageUrl,
                    Name = item.Name,
                    Description = item.Description,
                    Rating = item.Rating,
                    RatingDetail = item.RatingDetail,
                    HomeSelected = item.HomeSelected,
                    Price = item.Price,
                    CategoryID = item.CategoryID,
                    CategoryImageUrl = image       
                });
            }

            return latestFoodItemsWithCategoryImage;
        }

        public async Task<ObservableCollection<FoodItem>> GetFoodItemsByQueryAsync(string searchText)
        {
            var foodItemsByQuery = new ObservableCollection<FoodItem>();
            var items = (await GetFoodItemsAsync()).Where(p => p.Name.Contains(searchText)).ToList();
            foreach (var item in items)
            {
                foodItemsByQuery.Add(item);
            }
            return foodItemsByQuery;
        }

    }
}
