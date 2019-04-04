using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Model.Orders
{
    public class RentalCarsResponse
    {
        public List<RentalCars> RentalCars { get; set; }
    }


    

    public class RentalCars
    {
        public string OfferId { get; set; }
        public string Type { get; set; }
        public string CarName { get; set; }
        public string CarImagePath { get; set; }

        public string OperatorName { get; set; }
        public string OperatorLogoImagePath { get; set; }
        public string Url { get; set; }
        public string DropOffDate { get; set; }
        public string Address { get; set; }
        public double PricePerDay { get; set; }

        public double starRating { get; set; }
        public bool IsBestOption { get; set; }

        public RentalCarsAmenities Amenities { get; set; }


    }

    public class RentalCarsAmenities
    {

        public bool isFreeCancellation { get; set; }

        public bool isShuttle { get; set; }

        public int seats { get; set; }

        public int bags { get; set; }

    }

}
