using Provider.EventbriteApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event = TravelApp.Model.Orders.Event;

namespace businesstravel.Mapper.EventBrite
{
    public static class EventsRSMapper
    {
        public static List<Event> Map(Events events)
        {
            List<Event> result = new List<Event>();

            foreach(var evnt in events.events)
            {
                result.Add(new Event
                {
                    Description = evnt.description.text,
                    Id = evnt.id,
                    ImageUrl = evnt.logo.url,
                    //Time = evnt.ve
                    Title = evnt.name.text,
                     Url = evnt.vanity_url ?? evnt.url,
                    Category = evnt.category_id,
                });
            }

            var eventCategoryGrp = result.GroupBy(e => e.Category).ToList();

             var final = new List<Event>();
            foreach(var eventCategory in eventCategoryGrp)
            {
                var ss = eventCategory.ToList();
                final.AddRange(ss.Count < 10 ? ss : ss.Take(10));
            }
            return final;
        }
    }
}
