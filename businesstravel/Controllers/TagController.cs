using businesstravel.Provider;
using EventbriteNET;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Provider.NDCApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TravelApp.Model.DStore;
using TravelApp.Model.Orders;
using TravelApp.Model.Tag;

namespace businesstravel.Controllers
{
    [Route("api/[controller]")]
    // [Authorize]
    public class TagController : Controller
    {

        [HttpGet("profileId/companyId/location")]
        public TagResponse Get(string profileId, string companyId, string location)
        {
            return new TagResponse
            {
                Offers = Enumerable.Range(0, 20).Select(i =>
                         new TagOffer
                         {
                             Name = "Offer" + i,
                             Description = "Description of Offer" + 1,
                             Price = i % 3 == 0 ? "$12" : "$20",
                         }).ToList()

            };
        }
        // POST api/values
        [HttpPost]
        public TagResponse Post([FromBody]TagRequest request)
        {
            return new TagResponse
            {

                Offers = Enumerable.Range(0, 20).Select(i =>
                         new TagOffer
                         {
                             Name = "Offer" + i,
                             Description = "Description of Offer" + 1,
                             Price = i % 3 == 0 ? "$12" : "$20",
                         }).ToList()

            };

    }
    }

    [Route("api/[controller]")]
  //  // [Authorize]
    public class ShopController : Controller
    {
        // GET api/values/5
        [HttpGet("Cars")]
        public RentalCarsResponse GetCars()
        {
            return ApiProvider.Process(new RentalCarsRequest());
        }

        [HttpGet("hotels")]
        public HotelOffers GetHotels()
        {
            return ApiProvider.Process(new HotelsRequest());
        }
        [HttpGet("restaurants")]
        public RestaurantOffers GetRestaurant()
        {
            return ApiProvider.Process(new LocalRestaurantsRequest());
        }

        [HttpGet("flights/{profileId}/{origin}/{destination}/{departDate}/{arrivalDate}")]
        public ShopResponse Getflights(string profileId, string origin, string destination, string departDate, string arrivalDate)
        {
            /*
            Thread.Sleep(new Random(1).Next(5, 8));
            return new ShopResponse
            {
                FlightResults = new FlightResults
                {
                    Trips = new List<TripInfo>
                    {
                        new TripInfo
                        {
                            OriginDestinationKey = "OD1",
                              TotalPrice = "$225",
                                    PriceToCollect = "+/$0",
                            Flights = new List<Flight>
                            {
                                new Flight
                                {
                                    Origin = "ORD",
                                    Destination = "IAD",
                                    DepartureDate = "2018-06-14",
                                    DepartureTime = "1:05 pm",
                                    ArrivalTime = "3:56 pm",
                                    IsNonStop = "true",
                                    Amenities = new Amenities
                                    {
                                        IsLegroom = true,
                                        IsLounge= false,
                                        IsOnDemandVideo = true,
                                        IsUSB = true,
                                        IsWifi = true,
                                    },
                                    FlightKey = "F1",
                                    CarrierCode = "UA",
                                    MarketingCarrier = "UA",
                                    TotalPrice = "$323",
                                    PriceToCollect = "+/$0",
                                    FlightNumber = "UA 498",
                                    TravelTime = "1h 51m"
                                },

                                 new Flight
                                {
                                    Origin = "IAD",
                                    Destination = "ORD",
                                    DepartureDate = "2018-06-17",
                                    DepartureTime = "12:53 pm",
                                    ArrivalTime = "1:30 pm",
                                    IsNonStop = "true",
                                    Amenities = new Amenities
                                    {
                                        IsLegroom = true,
                                        IsLounge= false,
                                        IsOnDemandVideo = true,
                                        IsUSB = true,
                                        IsWifi = true,
                                    },
                                    FlightKey = "F1",
                                    CarrierCode = "UA",
                                    MarketingCarrier = "UA",
                                    TotalPrice = "$323",
                                    PriceToCollect = "+/$0",
                                    FlightNumber = "UA 221 ",
                                    TravelTime = "1h 53m"
                                },

                            }

                        }
                        ,
                        new TripInfo
                        {
                            OriginDestinationKey = "OD2",
                              TotalPrice = "$225",
                                    PriceToCollect = "+/$0",
                            Flights = new List<Flight>
                            {
                                new Flight
                                {
                                    Origin = "ORD",
                                    Destination = "IAD",
                                    DepartureDate = "2018-06-14",
                                    DepartureTime = "8:57 am",
                                    ArrivalTime = "11:10 pm",
                                    IsNonStop = "true",
                                    Amenities = new Amenities
                                    {
                                        IsLegroom = true,
                                        IsLounge= false,
                                        IsOnDemandVideo = true,
                                        IsUSB = true,
                                        IsWifi = true,
                                    },
                                    FlightKey = "F1",
                                    CarrierCode = "UA",
                                    MarketingCarrier = "UA",
                                    TotalPrice = "$225",
                                    PriceToCollect = "+/$0",
                                    FlightNumber = "UA 225",
                                    TravelTime = "1h 48m"
                                }, new Flight
                                {
                                    Origin = "IAD",
                                    Destination = "ORD",
                                    DepartureDate = "2018-06-17",
                                    DepartureTime = "12:53 pm",
                                    ArrivalTime = "1:30 pm",
                                    IsNonStop = "true",
                                    Amenities = new Amenities
                                    {
                                        IsLegroom = true,
                                        IsLounge= false,
                                        IsOnDemandVideo = true,
                                        IsUSB = true,
                                        IsWifi = true,
                                    },
                                    FlightKey = "F1",
                                    CarrierCode = "UA",
                                    MarketingCarrier = "UA",
                                    TotalPrice = "$323",
                                    PriceToCollect = "+/$0",
                                    FlightNumber = "UA 221 ",
                                    TravelTime = "1h 53m"
                                },


                            }

                        }
                        ,
                        new TripInfo
                        {
                            OriginDestinationKey = "OD3",
                              TotalPrice = "323",
                                    PriceToCollect = "+/$0",
                            Flights = new List<Flight>
                            {
                                new Flight
                                {
                                    Origin = "ORD",
                                    Destination = "IAD",
                                    DepartureDate = "2018-06-14",
                                    DepartureTime = "9:23 pm",
                                    ArrivalTime = "12:09 am",
                                    IsNonStop = "true",
                                    Amenities = new Amenities
                                    {
                                        IsLegroom = true,
                                        IsLounge= false,
                                        IsOnDemandVideo = true,
                                        IsUSB = true,
                                        IsWifi = true,
                                    },
                                    FlightKey = "F1",
                                    CarrierCode = "UA",
                                    MarketingCarrier = "UA",
                                    TotalPrice = "$225",
                                    PriceToCollect = "+/$0",
                                    FlightNumber = "UA 2000",
                                    TravelTime = "1h 52m"
                                },

                                 new Flight
                                {
                                    Origin = "IAD",
                                    Destination = "ORD",
                                    DepartureDate = "2018-06-17",
                                    DepartureTime = "12:53 pm",
                                    ArrivalTime = "1:30 pm",
                                    IsNonStop = "true",
                                    Amenities = new Amenities
                                    {
                                        IsLegroom = true,
                                        IsLounge= false,
                                        IsOnDemandVideo = true,
                                        IsUSB = true,
                                        IsWifi = true,
                                    },
                                    FlightKey = "F1",
                                    CarrierCode = "UA",
                                    MarketingCarrier = "UA",
                                    TotalPrice = "$323",
                                    PriceToCollect = "+/$0",
                                    FlightNumber = "UA 221 ",
                                    TravelTime = "1h 53m"
                                },
                            }

                        }
                        ,new TripInfo
                        {
                            OriginDestinationKey = "OD4",
                              TotalPrice = "323",
                                    PriceToCollect = "+/$0",
                            Flights = new List<Flight>
                            {
                                new Flight
                                {
                                    Origin = "ORD",
                                    Destination = "IAD",
                                    DepartureDate = "2018-06-14",
                                    DepartureTime = "1:05 pm",
                                    ArrivalTime = "3:56 pm",
                                    IsNonStop = "true",
                                    Amenities = new Amenities
                                    {
                                        IsLegroom = true,
                                        IsLounge= false,
                                        IsOnDemandVideo = true,
                                        IsUSB = true,
                                        IsWifi = true,
                                    },
                                    FlightKey = "F1",
                                    CarrierCode = "UA",
                                    MarketingCarrier = "UA",
                                    TotalPrice = "$323",
                                    PriceToCollect = "+/$0",
                                    FlightNumber = "UA 498",
                                    TravelTime = "1h 51m"
                                },

                                 new Flight
                                {
                                    Origin = "IAD",
                                    Destination = "ORD",
                                    DepartureDate = "2018-06-17",
                                    DepartureTime = "10:15 pm",
                                    ArrivalTime = "11:14 pm",
                                    IsNonStop = "true",
                                    Amenities = new Amenities
                                    {
                                        IsLegroom = true,
                                        IsLounge= false,
                                        IsOnDemandVideo = true,
                                        IsUSB = true,
                                        IsWifi = true,
                                    },
                                    FlightKey = "F3",
                                    CarrierCode = "UA",
                                    MarketingCarrier = "UA",
                                    TotalPrice = "$323",
                                    PriceToCollect = "+/$0",
                                    FlightNumber = "UA 552  ",
                                    TravelTime = "1h 53m"
                                },
                            }
                        }
                        ,
                        new TripInfo
                        {
                            OriginDestinationKey = "OD5",
                              TotalPrice = "380",
                                    PriceToCollect = "+/$20",
                            Flights = new List<Flight>
                            {
                                new Flight
                                {
                                    Origin = "ORD",
                                    Destination = "IAD",
                                    DepartureDate = "2018-06-14",
                                    DepartureTime = "8:57 am",
                                    ArrivalTime = "11:10 pm",
                                    IsNonStop = "true",
                                    Amenities = new Amenities
                                    {
                                        IsLegroom = true,
                                        IsLounge= false,
                                        IsOnDemandVideo = true,
                                        IsUSB = true,
                                        IsWifi = true,
                                    },
                                    FlightKey = "F1",
                                    CarrierCode = "UA",
                                    MarketingCarrier = "UA",
                                    TotalPrice = "$225",
                                    PriceToCollect = "+/$0",
                                    FlightNumber = "UA 225",
                                    TravelTime = "1h 48m"
                                },
                                 new Flight
                                {
                                    Origin = "IAD",
                                    Destination = "ORD",
                                    DepartureDate = "2018-06-17",
                                    DepartureTime = "10:15 pm",
                                    ArrivalTime = "11:14 pm",
                                    IsNonStop = "true",
                                    Amenities = new Amenities
                                    {
                                        IsLegroom = true,
                                        IsLounge= false,
                                        IsOnDemandVideo = true,
                                        IsUSB = true,
                                        IsWifi = true,
                                    },
                                    FlightKey = "F3",
                                    CarrierCode = "UA",
                                    MarketingCarrier = "UA",
                                    TotalPrice = "$323",
                                    PriceToCollect = "+/$0",
                                    FlightNumber = "UA 552  ",
                                    TravelTime = "1h 53m"
                                },

                            }

                        }
                        ,
                        new TripInfo
                        {
                            OriginDestinationKey = "OD6",
                              TotalPrice = "400",
                                    PriceToCollect = "+/$40",
                            Flights = new List<Flight>
                            {
                                new Flight
                                {
                                    Origin = "ORD",
                                    Destination = "IAD",
                                    DepartureDate = "2018-06-14",
                                    DepartureTime = "9:23 pm",
                                    ArrivalTime = "12:09 am",
                                    IsNonStop = "true",
                                    Amenities = new Amenities
                                    {
                                        IsLegroom = true,
                                        IsLounge= false,
                                        IsOnDemandVideo = true,
                                        IsUSB = true,
                                        IsWifi = true,
                                    },
                                    FlightKey = "F1",
                                    CarrierCode = "UA",
                                    MarketingCarrier = "UA",
                                    TotalPrice = "$225",
                                    PriceToCollect = "+/$0",
                                    FlightNumber = "UA 2000",
                                    TravelTime = "1h 52m"
                                },

                                 new Flight
                                {
                                    Origin = "IAD",
                                    Destination = "ORD",
                                    DepartureDate = "2018-06-17",
                                    DepartureTime = "10:15 pm",
                                    ArrivalTime = "11:14 pm",
                                    IsNonStop = "true",
                                    Amenities = new Amenities
                                    {
                                        IsLegroom = true,
                                        IsLounge= false,
                                        IsOnDemandVideo = true,
                                        IsUSB = true,
                                        IsWifi = true,
                                    },
                                    FlightKey = "F3",
                                    CarrierCode = "UA",
                                    MarketingCarrier = "UA",
                                    TotalPrice = "$323",
                                    PriceToCollect = "+/$0",
                                    FlightNumber = "UA 552  ",
                                    TravelTime = "1h 53m"
                                },
                            }

                        }
                    }
                }
            };
            */

            var requesttrips = new List<Trip>
                    {
                        new Trip
                        {
                            Origin = origin,
                            Destination = destination,
                            DepartureDate= departDate,
                        }
                    };

            if(!string.IsNullOrWhiteSpace(arrivalDate))
            {
                requesttrips.Add(new Trip
                        {
                            Origin = destination,
                            Destination = origin,
                            DepartureDate= arrivalDate,
                        });
            }
            var shopRs = ApiProvider.Process(new ShopRequest
            {
                ProfileId = profileId,
                Itineary = new Itineary
                {
                    Trips = requesttrips,
                }
            });

            var trips = new List<Flight>();

            if (shopRs.FlightResults.Trips.Count > 7)
            {
                shopRs.FlightResults.Trips.RemoveRange(7, shopRs.FlightResults.Trips.Count - 7);
            }

            return shopRs;
        }

        // POST api/va
        [HttpPost("flights")]
        public ShopResponse Post([FromBody]ShopRequest request)
        {
            var shopRs = ApiProvider.Process(request);

            var trips = new List<Flight>();

            if (shopRs.FlightResults.Trips.Count > 7)
            {
                shopRs.FlightResults.Trips.RemoveRange(7, shopRs.FlightResults.Trips.Count - 7);
            }

            return shopRs;

        }

        [HttpPost("hotels")]
        public HotelOffers Post([FromBody]HotelRequest request)
        {

            //Sample partial Hotel Data
            
                HotelOffers hOffers = new HotelOffers();
                hOffers.Offers = new List<Hotel>();

            //-------------------------Hoteloffer1---------------------------
            var offer1 = new Hotel
            {
                name = "Sofitel Washington",
                imagePath = "assets/hotel_sofitel.jpg",
                distance = "2 miles",
                price = "$200"
            };
            hOffers.Offers.Add(offer1);
            //---------------------------------------------------------------
            //-------------------------Hoteloffer2---------------------------
            var offer2 = new Hotel
            {
                name = "Hilton Hotel",
                imagePath = "",
                distance = "5 miles",
                price = "$220"
            };
            hOffers.Offers.Add(offer2);
            //---------------------------------------------------------------
            //-------------------------Hoteloffer3---------------------------
            var offer3 = new Hotel
            {
                name = "Mandarin Oriental",
                imagePath = "",
                distance = "3 miles",
                price = "$240"
            };
            hOffers.Offers.Add(offer3);
            //---------------------------------------------------------------
            //-------------------------Hoteloffer4---------------------------
            var offer4 = new Hotel
            {
                name = "Willard InterContinental",
                imagePath = "",
                distance = "5 miles",
                price = "$300"
            };
            hOffers.Offers.Add(offer4);
                //---------------------------------------------------------------
                return hOffers;
               

        }
    }


    [Route("api/[controller]")]
    // [Authorize]
    public class DStoreController : Controller
    {
        // GET api/values
        [HttpGet("profileId/companyId/location")]
            public DStoreResponse Get(string profileId, string companyId, string location)
            {
            return new DStoreResponse
            {
                Offers = Enumerable.Range(0,20).Select(i => 
                        new Offer
                        {
                            Name = "Offer" + i,
                            Description ="Description of Offer" +1,
                            Price = i%3 == 0 ? "$12": "$20",
                       }).ToList()
                    
            };
            }

        [HttpPost]
        public DStoreResponse Post([FromBody]DStoreRequest request)
        {
            return new DStoreResponse
            {

                Offers = Enumerable.Range(0, 20).Select(i =>
                         new Offer
                         {
                             Name = "Offer" + i,
                             Description = "Description of Offer" + 1,
                             Price = i % 3 == 0 ? "$12" : "$20",
                         }).ToList()

            };

        }
    }
    }
