using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Model.Orders
{
    public class ShopResponse
    {

        #region Properties

        public string CountryCode { get; set; }

        public List<KeyValuePair<string, string>> Characteristics { get; set; }

        public FlightResults FlightResults { get; set; }

        #endregion
    }
    
    public class Amenity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }

    public class PriceDetail
    {
        public Fare TotalPrice { get; set; }
        public Fare Base { get; set; }
        public List<Fare> Taxes { get; set; }

        public Fare PriceToPay { get; set; }
    }

    public class Fare
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
    public class FlightResults
    {

        public string ShopResponseId { get; set; }
        public List<TripInfo> Trips { get; set; }
        public List<Flight> SelectedFlights { get; set; }
    }

    public class TripInfo
    {
        public string OriginDestinationKey { get; set; }
        public List<Flight> Flights { get; set; }
        public string PriceToCollect { get; set; }
        public string TotalPrice { get; set; }
    }

    public class Flight
    {
        public string FlightKey { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string DepartureDate { get; set; }
        public string DepartureDateFormatted { get; set; }
        public string ArrivalDate { get; set; }
        public string ArrivalDateFormatted { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string CarrierCode { get; set; }
        public string FlightNumber { get; set; }
        public string MarketingCarrier { get; set; }
        public string IsNonStop { get; set; }
        public string TravelTime { get; set; }
        public Amenities Amenities { get; set; }
        public List<Flight> Connections { get; set; }
        public string PriceToCollect { get; set; }
        public string TotalPrice { get; set; }
    }
    public class Amenities
    {
        public bool IsWifi { get; set; }
        public bool IsLegroom { get; set; }
        public bool IsUSB { get; set; }
        public bool IsOnDemandVideo { get; set; }
        public bool IsLounge { get; set; }
    }

    public sealed class Flight123
    {
        public string DepartDate { get; set; }

        public PriceDetail PriceDetail { get; set; }

        public List<Amenity> Amenities { get; set; }

        public string DepartDateTime { get; set; }

        public string SegRefId { get; set; }

        public string CabinType { get; set; }

        public string Destination { get; set; }

        public string FlightNumber { get; set; }


        public string MarketingCarrier { get; set; }

        public string OperatingCarrier { get; set; }

        public string Origin { get; set; }

        public string TravelMinutes { get; set; }
        public string TripIndex { get; set; }

        public List<Flight> Connections { get; set; }
    }

    //Hotel Response

    public class HotelOffers
    {
        public  List<Hotel> Offers { get; set; }
    }

    public class Hotel
    {
         public string offerId { get; set; }
        public string imagePath { get; set; }
        public string name { get; set; }
        public string distance { get; set; }
        public string price { get; set; }
        public string address { get; set; }
        public double starRating { get; set; }
        public string starType { get; set; }
        public bool isBestHotel { get; set; }
        public HotelAmenities amenities { get; set; }
    }

    public class HotelAmenities
    {
        public bool isBreakfastIncluded { get; set; }
        public bool isRefundable { get; set; }
        public bool isPetFriendly { get; set; }
        public bool isWifi { get; set; }
        public bool isSmokingAllowed { get; set; }
        public bool isTV { get; set; }
        public bool isBarAvailable { get; set; }
    }

    public class RestaurantOffers
    {
        public List<Restaurant> Restaurants { get; set; }
    }

    public class Restaurant
    {
        public string category { get; set; }
        public string location { get; set; }
        public string contact { get; set; }
        public string offerId { get; set; }
        public string imagePath { get; set; }
        public string name { get; set; }
        public string distance { get; set; }
        public string price { get; set; }
        public string address { get; set; }
        public string url { get; set; }
        public double starRating { get; set; }
        public string starType { get; set; }
        public bool isBest { get; set; }
        public HotelAmenities amenities { get; set; }
    }
}
