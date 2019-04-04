using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TravelApp.Model.Orders
{
    public class ShopRequest
    {
        public string RequestId { get; set; }
        public string CompanyId { get; set; }
        public string ProfileId { get; set; }
        public Itineary Itineary { get; set; }
    }

    public class OneOrderRequest
    {
        public string RequestId { get; set; }
        public string CompanyId { get; set; }
        public string ProfileId { get; set; }
        public FlightResults FlightResults { get; set; }
        public Hotel Hotel { get; set; }
        public RentalCars RentalCar { get; set; }
    }
    public class Itineary
    {
        public string ShopResponseId { get; set; }
        public List<Trip> Trips { get; set; }

        public List<string> Preferences { get; set; }

        public string Cabin { get; set; }

      //  public List<Passenger> Passengers { get; set; }
    }

    public class Passenger
    {
        public string Id { get; set; }
        public string PTC { get; set; }
       
    }

    public class Trip
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string DepartureDate { get; set; }
        public string SelectedFlightId { get; set; }
    }

    public class Event
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Venue { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public string Category { get; set; }

        public string Url { get; set; }

    }

    public class LocalDeal
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Venue { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public string Category { get; set; }

        public string Url { get; set; }

    }

    public class HotelRequest
    {
        public string RequestId { get; set; }
        public string CompanyId { get; set; }
        public string ProfileId { get; set; }
        public string Location { get; set; }
    }
}
