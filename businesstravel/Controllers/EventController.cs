using businesstravel.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TravelApp.Model.Orders;

namespace businesstravel.Controllers
{
    [Route("api/[controller]")]
   // // [Authorize]
    public class EventController : Controller
    {


        // GET api/values/5
        [HttpGet("{location}/{preferences}")]
        public List<Event> Get(string location, string preferences)
        {
            return ApiProvider.Process(location, preferences);
          return  new List<Event>
            {
              new Event
              {
                  Title = "Big Data Day LA 2018",
                  Description = "We are thrilled to open registration for Big Data Day LA 2018, tentatively scheduled for Saturday, August 11th, 2018 at University of Southern California",
                  Time = "Saturday, August 11th, 2018",
                  Venue = "University of Southern California",
                  ImageUrl = "https://img.evbuc.com/https%3A%2F%2Fcdn.evbuc.com%2Fimages%2F41503594%2F1961717567%2F1%2Foriginal.jpg?w=800&auto=compress&rect=583%2C389%2C1944%2C972&s=e068139d3d0c3fcb38c8385afd60109f",
                  Id = "43642378529"
              },
               new Event
              {
                  Title = "Big Data Day LA 2018",
                  Description = "We are thrilled to open registration for Big Data Day LA 2018, tentatively scheduled for Saturday, August 11th, 2018 at University of Southern California",
                  Time = "Saturday, September 11th, 2018",
                  Venue = "University of Southern California",
                  ImageUrl = "https://img.evbuc.com/https%3A%2F%2Fcdn.evbuc.com%2Fimages%2F41503594%2F1961717567%2F1%2Foriginal.jpg?w=800&auto=compress&rect=583%2C389%2C1944%2C972&s=e068139d3d0c3fcb38c8385afd60109f",
                  Id = "43642378529"
              },
                new Event
              {
                  Title = "Big Data Day LA 2018",
                  Description = "We are thrilled to open registration for Big Data Day LA 2018, tentatively scheduled for Saturday, August 11th, 2018 at University of Southern California",
                  Time = "Saturday, October 11th, 2018",
                  Venue = "University of Southern California",
                  ImageUrl = "https://img.evbuc.com/https%3A%2F%2Fcdn.evbuc.com%2Fimages%2F41503594%2F1961717567%2F1%2Foriginal.jpg?w=800&auto=compress&rect=583%2C389%2C1944%2C972&s=e068139d3d0c3fcb38c8385afd60109f",
                  Id = "43642378529"
              }
            };
        }
    }

    [Route("api/[controller]")]
    public class OffersController : Controller
    {
        // GET api/values/5
        [HttpGet("{location}/{preferences}")]
        public List<Event> Get(string location, string preferences)
        {
            return new List<Event>
            {
              new Event
              {
                  Title = "StarBucks",
                  Description = "We are thrilled to open registration for Big Data Day LA 2018, tentatively scheduled for Saturday, August 11th, 2018 at University of Southern California",
                  Time = "Saturday, August 11th, 2018",
                  Venue = "University of Southern California",
                  ImageUrl = "https://img.evbuc.com/https%3A%2F%2Fcdn.evbuc.com%2Fimages%2F41503594%2F1961717567%2F1%2Foriginal.jpg?w=800&auto=compress&rect=583%2C389%2C1944%2C972&s=e068139d3d0c3fcb38c8385afd60109f",
                  Id = "43642378529"
              },
               new Event
              {
                  Title = "Big Data Day LA 2018",
                  Description = "We are thrilled to open registration for Big Data Day LA 2018, tentatively scheduled for Saturday, August 11th, 2018 at University of Southern California",
                  Time = "Saturday, September 11th, 2018",
                  Venue = "University of Southern California",
                  ImageUrl = "https://img.evbuc.com/https%3A%2F%2Fcdn.evbuc.com%2Fimages%2F41503594%2F1961717567%2F1%2Foriginal.jpg?w=800&auto=compress&rect=583%2C389%2C1944%2C972&s=e068139d3d0c3fcb38c8385afd60109f",
                  Id = "43642378529"
              },
                new Event
              {
                  Title = "Big Data Day LA 2018",
                  Description = "We are thrilled to open registration for Big Data Day LA 2018, tentatively scheduled for Saturday, August 11th, 2018 at University of Southern California",
                  Time = "Saturday, October 11th, 2018",
                  Venue = "University of Southern California",
                  ImageUrl = "https://img.evbuc.com/https%3A%2F%2Fcdn.evbuc.com%2Fimages%2F41503594%2F1961717567%2F1%2Foriginal.jpg?w=800&auto=compress&rect=583%2C389%2C1944%2C972&s=e068139d3d0c3fcb38c8385afd60109f",
                  Id = "43642378529"
              }
            };
        }
    }

    [Route("api/[controller]")]
    public class DealsController : Controller
    {
        // GET api/values/5
        [HttpGet("{location}/{preferences}")]
        public List<LocalDeal> Get(string location, string preferences)
        {
            return ApiProvider.Process(new LocalDealsRequest());
            return new List<LocalDeal>
            {
              new LocalDeal
              {
                  Title = "StarBucks",
                  Description = "We are thrilled to open registration for Big Data Day LA 2018, tentatively scheduled for Saturday, August 11th, 2018 at University of Southern California",
                  Time = "Saturday, August 11th, 2018",
                  Venue = "University of Southern California",
                  ImageUrl = "https://img.evbuc.com/https%3A%2F%2Fcdn.evbuc.com%2Fimages%2F41503594%2F1961717567%2F1%2Foriginal.jpg?w=800&auto=compress&rect=583%2C389%2C1944%2C972&s=e068139d3d0c3fcb38c8385afd60109f",
                  Id = "43642378529"
              },
               new LocalDeal
              {
                  Title = "Big Data Day LA 2018",
                  Description = "We are thrilled to open registration for Big Data Day LA 2018, tentatively scheduled for Saturday, August 11th, 2018 at University of Southern California",
                  Time = "Saturday, September 11th, 2018",
                  Venue = "University of Southern California",
                  ImageUrl = "https://img.evbuc.com/https%3A%2F%2Fcdn.evbuc.com%2Fimages%2F41503594%2F1961717567%2F1%2Foriginal.jpg?w=800&auto=compress&rect=583%2C389%2C1944%2C972&s=e068139d3d0c3fcb38c8385afd60109f",
                  Id = "43642378529"
              },
                new LocalDeal
              {
                  Title = "Big Data Day LA 2018",
                  Description = "We are thrilled to open registration for Big Data Day LA 2018, tentatively scheduled for Saturday, August 11th, 2018 at University of Southern California",
                  Time = "Saturday, October 11th, 2018",
                  Venue = "University of Southern California",
                  ImageUrl = "https://img.evbuc.com/https%3A%2F%2Fcdn.evbuc.com%2Fimages%2F41503594%2F1961717567%2F1%2Foriginal.jpg?w=800&auto=compress&rect=583%2C389%2C1944%2C972&s=e068139d3d0c3fcb38c8385afd60109f",
                 Id = "43642378529"
              }
            };
        }
    }
}
