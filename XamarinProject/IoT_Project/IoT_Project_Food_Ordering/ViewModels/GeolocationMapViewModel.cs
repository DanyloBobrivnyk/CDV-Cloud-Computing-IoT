using System;
using System.Collections.Generic;
using System.Text;

namespace IoT_Project_Food_Ordering.ViewModels
{
    public class GeolocationMapViewModel : BaseViewModel
    {
        private double Longtitude;

        public double _Longtitude
        {
            get { return Longtitude; }
            set { Longtitude = value; }
        }


    }
}
