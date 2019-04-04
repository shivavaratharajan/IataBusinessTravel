using businesstravel.Mapper.EventBrite;
using businesstravel.Mapper.Groupon;
using businesstravel.Mapper.SkyScanner;
using businesstravel.Mapper.Yelp;
using Newtonsoft.Json;
using Provider.EventbriteApi;
using Provider.EventbriteApi.Entities;
using Provider.Groupon;
using Provider.NDCApi;
using Provider.NDCApi.Mapper.AirShopping;
using Provider.SkyScanner;
using Provider.SkyScanner.Schema;
using Provider.UAApi.Invokers;
using Provider.Yelp;
using Provider.Yelp.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelApp.Model.Orders;
using Event = TravelApp.Model.Orders.Event;

namespace businesstravel.Provider
{
    public class ApiProvider
    {
        public static ShopResponse Process(ShopRequest shopRequest)
        {
            var ss = AirShoppingRQMapper.Map(shopRequest);
          //  var req = XmlUtil.Serialize(ss);
            var ssds = JsonConvert.SerializeObject(ss);
           // var response = new NDCProvider().Invoke<AirShoppingRQ, AirShoppingRS>(req);
            var response = new AirShoppingInvoker().Invoke(ss);
            
            return AirShoppingRSMapper.Map(response);
        }

        public static List<Event> Process(string location, string preferences)
        {
            preferences = preferences ?? "101,102,109";
            location = location ?? "washington,DC";
            var queryParam = $"events/search/?categories={preferences}&location.address={location}";
            var response = new EventBriteProvider().Invoke<string, Events>(location,queryParams: queryParam);

            return EventsRSMapper.Map(response);
        }
        public static RentalCarsResponse Process(RentalCarsRequest rentalCarsRequest)
        {
            var response = new SkyScannerProvider().GetCars();

            return SkyScannerCarsRSMapper.Map(response);
        }

        public static HotelOffers Process(HotelsRequest rentalCarsRequest)
        {

            var response = new SkyScannerProvider().GetHotels();

            return SkyScannerCarsRSMapper.Map(response);
        }

        public static List<LocalDeal> Process(LocalDealsRequest localDealsRequest)
        {

            var response = new GrouponProvider().GetDeals();

            return GrouponDealsRSMapper.Map(response);
        }

        

        public static RestaurantOffers Process(LocalRestaurantsRequest localDealsRequest)
        {

            var response = new YelpProvider().GetResturants();

            return YelpResponseMapper.Map(response);
        }
    }
}
