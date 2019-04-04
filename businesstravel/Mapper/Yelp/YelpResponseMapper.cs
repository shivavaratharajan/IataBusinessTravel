using Provider.SkyScanner.Schema;
using Provider.Yelp.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelApp.Model.Orders;

namespace businesstravel.Mapper.Yelp
{

    public static class YelpResponseMapper
    {

        public static RestaurantOffers Map(YelpResult response)
        {
            //https://www.google.com/search?q=hertz+logo&tbm=isch&source=iu&ictx=1&fir=n0iDQzzKdolXSM%253A%252CiVtHVwm7oNJmZM%252C_&usg=__OkrVPO57e0GcB8g-3gyFXs088go%3D&sa=X&ved=0ahUKEwiLvKeu0cfbAhXQzlMKHZpCCFkQ9QEIOzAA#imgrc=n0iDQzzKdolXSM:
            var restaurants = new List<Restaurant>();
            foreach (var deal in response.businesses)
            {
                if (deal.categories == null || deal.categories.Count == 0 || deal.categories[0] == null)
                {
                    deal.categories = new List<Category>
                    {
                        new Category
                        {
                            title= "Breakfast & Brunch"
                        }
                    };
                }
                restaurants.Add(new Restaurant
                {
                    category = string.Join(",", deal.categories.Select(c => c.title)),
                    offerId = deal.id,
                    name = deal.name,
                    url = deal.url,
                    imagePath = deal.image_url,
                    starRating = deal.rating,
                    location = deal.location.address1,
                    address = string.Join(",", deal.location.display_address),
                    isBest = deal.rating > 4.2 && deal.review_count > 1000
                });
            }

            return new RestaurantOffers
            {
                Restaurants = restaurants.Count > 20 ? restaurants.Take(20).ToList() : restaurants,
            };

        }
    }
}
