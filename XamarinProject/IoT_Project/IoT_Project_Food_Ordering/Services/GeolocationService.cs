using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace IoT_Project_Food_Ordering.Services
{
    public class GeolocationService
    {

        public async void GetCurrentGeolocationAsync()
        {
            List<double> coordinatesList = new List<double>();
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    coordinatesList.Add(location.Latitude);
                    coordinatesList.Add(location.Longitude);

                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                Console.WriteLine(fnsEx.Message);
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                Console.WriteLine(fneEx.Message);

            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                Console.WriteLine(pEx.Message);

            }
            catch (Exception ex)
            {
                // Unable to get location
                Console.WriteLine(ex.Message);
            }


        }
    }
}
