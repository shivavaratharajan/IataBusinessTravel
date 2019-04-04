using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Model.Orders
{
    public class RentalCarsRequest
    {

        public string PickUp { get; set; }
        public string DropOff { get; set; }
        public string PickUpDate { get; set; }
        public string DropOffDate { get; set; }
    }

    public class HotelsRequest
    {

        public string PickUp { get; set; }
        public string DropOff { get; set; }
        public string PickUpDate { get; set; }
        public string DropOffDate { get; set; }
    }
    public class LocalDealsRequest
    {

        public string Location { get; set; }
        public List<string> Preferences { get; set; }

    }

    public class LocalRestaurantsRequest
    {

        public string Location { get; set; }
        public List<string> Preferences { get; set; }

    }
}
