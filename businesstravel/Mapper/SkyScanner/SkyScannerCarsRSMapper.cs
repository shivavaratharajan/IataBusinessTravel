using Provider.SkyScanner.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelApp.Model.Orders;

namespace businesstravel.Mapper.SkyScanner
{
    public static class SkyScannerCarsRSMapper
    {

        public static RentalCarsResponse Map(CarCollection response)
        {
            //https://www.google.com/search?q=hertz+logo&tbm=isch&source=iu&ictx=1&fir=n0iDQzzKdolXSM%253A%252CiVtHVwm7oNJmZM%252C_&usg=__OkrVPO57e0GcB8g-3gyFXs088go%3D&sa=X&ved=0ahUKEwiLvKeu0cfbAhXQzlMKHZpCCFkQ9QEIOzAA#imgrc=n0iDQzzKdolXSM:
            var rentalCars = new List<RentalCars>();
            foreach (var car in response.cars)
            {
                var imageurl = response.images.FirstOrDefault(img => img.id == car.image_id);
                var carClass= response.car_classes.FirstOrDefault(cls => cls.id == car.car_class_id);

                var operatorname = "HERTZ";
                    
                if(car.price_all_days < 100)
                {
                    operatorname = "Budget";
                }else if (car.price_all_days > 100 && car.price_all_days > 120)
                {
                    operatorname = "Avis";
                }
                else if (car.price_all_days > 120 && car.price_all_days > 150)
                {
                    operatorname = "Enterprise";
                }

                if (imageurl == null)
                {
                    continue;
                }
                rentalCars.Add(new RentalCars
                {
                    Type = carClass == null? "Compact": carClass.name,
                    Address = car.location?.pick_up?.address,
                    PricePerDay = car.price_all_days,
                    Url = car.deeplink_url,
                    CarImagePath = imageurl.url,
                    CarName = car.vehicle,
                    IsBestOption = true,
                    OperatorName = operatorname,
                    Amenities = new RentalCarsAmenities
                    {
                        bags = car.bags,
                        seats = car.seats,
                        isFreeCancellation = car.value_add != null && car.value_add.free_cancellation.GetValueOrDefault(),
                        isShuttle = car.mandatory_chauffeur
                    }
                });
            }

            return new RentalCarsResponse
            {
                RentalCars = rentalCars.Count > 20 ? rentalCars.Take(20).ToList() : rentalCars
            };
        }

        public static HotelOffers Map(HotelCollection response)
        {
            var dict = new Dictionary<int, string>()
            {
                {0,"Hamilton Hotel - Washington DC." },
                {1,"Hilton Garden Inn Washington DC Downtown" },
                {2,"The Madison Washington, DC, a Hilton Hotel" },
                {3,"Fairfield Inn & Suites by Marriott Washington, DC/Downtown" },
                 {4,"Embassy Suites by Hilton Washington DC Convention Center" },
                  {5,"Hyatt Place Washington DC/National Mall" },
                   {6,"Courtyard by Marriott Washington Embassy Row" },


            };
            //https://www.google.com/search?q=hertz+logo&tbm=isch&source=iu&ictx=1&fir=n0iDQzzKdolXSM%253A%252CiVtHVwm7oNJmZM%252C_&usg=__OkrVPO57e0GcB8g-3gyFXs088go%3D&sa=X&ved=0ahUKEwiLvKeu0cfbAhXQzlMKHZpCCFkQ9QEIOzAA#imgrc=n0iDQzzKdolXSM:
            var hotels = new List<TravelApp.Model.Orders.Hotel>();
            foreach (var hotel in response.results.hotels)
            {
                if(hotel.amenities == null)
                {
                    hotel.amenities = new List<string>();
                }
                if(hotel.rating == null)
                {
                    hotel.rating = new Rating
                    {
                        value = 5.0
                    };
                }
                if (hotel.offers== null || hotel.offers.Count == 0 || hotel.offers[0] == null)
                {
                    hotel.offers = new List<Offer>
                    {
                        new Offer
                        {
                            price = 117,
                            cancellation ="non_refundable"
                        }
                    };
                }
                hotels.Add(new TravelApp.Model.Orders.Hotel
                {
                    address = "Washington,DC", //hotel.city,
                    distance = $"{hotel.rating.value} miles",
                    isBestHotel = hotel.rating.value > 8.7,
                    name = dict[int.Parse(hotel.hotel_id)%5] ,// hotel.name,
                    offerId = hotel.hotel_id,
                    price = $"${hotel.offers[0].price}",
                    amenities = new HotelAmenities
                    {
                        isBarAvailable = hotel.amenities.Contains("Bar"),
                        isBreakfastIncluded = hotel.amenities.Contains("Restaurant"),
                        isPetFriendly = hotel.amenities.Contains("Pool"),
                        isRefundable = !string.Equals(hotel.offers[0].cancellation, "non_refundable", StringComparison.OrdinalIgnoreCase),
                        isSmokingAllowed = !hotel.amenities.Contains("NonSmokingService"),
                        isTV = hotel.amenities.Contains("AirConditioning"),
                        isWifi = hotel.amenities.Contains("WifiService")
                    },
                    imagePath = hotel.images[0].thumbnail,
                    starRating = hotel.rating.value,
                    starType = hotel.stars,
                    
                });
                
                
            }

            return new HotelOffers
            {
                Offers = hotels.Count > 20 ? hotels.Take(20).ToList() : hotels
            };
        }
    }
}
