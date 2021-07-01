using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IoT_Project_Food_Ordering.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeolocationMapView : ContentPage
    {
        public GeolocationMapView()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var result = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));

            resultLocation.Text = $"Lat: { result.Latitude}, {result.Longitude}"; 
        }
    }
}