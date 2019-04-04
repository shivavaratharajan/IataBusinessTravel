using Provider.Groupon.Schema;
using Provider.SkyScanner.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelApp.Model.Orders;

namespace businesstravel.Mapper.Groupon
{
    public static class GrouponDealsRSMapper
    {

        public static List<LocalDeal> Map(GrouponDeals response)
        {
            //https://www.google.com/search?q=hertz+logo&tbm=isch&source=iu&ictx=1&fir=n0iDQzzKdolXSM%253A%252CiVtHVwm7oNJmZM%252C_&usg=__OkrVPO57e0GcB8g-3gyFXs088go%3D&sa=X&ved=0ahUKEwiLvKeu0cfbAhXQzlMKHZpCCFkQ9QEIOzAA#imgrc=n0iDQzzKdolXSM:
            var localDeals = new List<LocalDeal>();
            foreach (var deal in response.deals)
            {
                if (deal.tags == null || deal.tags.Count == 0 || deal.tags[0] == null)
                {
                    deal.tags = new List<Tag>
                    {
                        new Tag
                        {
                            name = "Caf̩e / Bar Offerings"
                        }
                    };
                }
                localDeals.Add(new LocalDeal
                {
                    Category = deal.tags[0].name,
                    Id = deal.id,
                    Description = deal.title,
                    Title = deal.announcementTitle,
                    Url = deal.dealUrl,
                    ImageUrl = deal.grid4ImageUrl,
                    Time = deal.endAt.ToString("yyyy-MM-dd hh:mm:ss tt")
                });
            }

            return localDeals.Count > 20 ? localDeals.Take(20).ToList() : localDeals;

        }

    }
}
