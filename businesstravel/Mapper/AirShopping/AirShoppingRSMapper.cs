using Provider.UAApi.Schema;
using Provider.UAApi.Schema.AirShopping;
using System.Collections.Generic;
using System.Linq;
using TravelApp.Model.Orders;

namespace Provider.NDCApi.Mapper.AirShopping
{
    public static class AirShoppingRSMapper
    {
        public static ShopResponse Map(this AirShoppingRS airShoppingRS)
        {
            var offersGroups = airShoppingRS.Items.FirstOrDefault( itm => itm as AirShoppingRSOffersGroup != null);

            if(offersGroups == null)
            {
                return new ShopResponse();
            }
            var offersGroup = offersGroups as AirShoppingRSOffersGroup;

            if (offersGroup == null)
            {
                return new ShopResponse();
            }

            var airlineOffers = offersGroup.AirlineOffers;

            var dataListCheck = airShoppingRS.Items.FirstOrDefault(itm => itm as AirShoppingRSDataLists != null);
            if (dataListCheck == null)
            {
                return new ShopResponse();
            }

            var dataList = dataListCheck as AirShoppingRSDataLists; ;
           
            var serviceDefinitionDict = dataList.ServiceDefinitionList.ToDictionary(k => k.ServiceDefinitionID, v => v);
            var flightDict = dataList.FlightList.ToDictionary(k => k.FlightKey, v => v);
            var flightSegmentDict = dataList.FlightSegmentList.ToDictionary(k => k.SegmentKey, v => v);
            var originDestinationDict = dataList.OriginDestinationList.ToDictionary(k => k.OriginDestinationKey, v => v);
            var priceClassDict = dataList.PriceClassList.ToDictionary(k => k.PriceClassID, v => v);

            var shopResponse = new ShopResponse
            {
                FlightResults = new FlightResults()
            };
            var trips = new List<TravelApp.Model.Orders.TripInfo>();
            foreach (var airlineOffer in airlineOffers)
            {
                var flightsOverview = airlineOffer.Offer.Select(o => o.FlightsOverview).ToList();
                var offerItems = airlineOffer.Offer.SelectMany(o => o.OfferItem).ToList();
                var idx = 0;
                var flights = new List<TravelApp.Model.Orders.Flight>();
                foreach (var flightOverview in flightsOverview)
                {
                    var lof = flightDict[flightOverview.FlightRef[0].Value];
                    var segments = lof.SegmentReferences.Value.Split(' ', System.StringSplitOptions.RemoveEmptyEntries).ToList();

                    TravelApp.Model.Orders.Flight flight = new TravelApp.Model.Orders.Flight();
                   
                    for (var i = 0; i < segments.Count; i++)
                    {
                       var priceToPay =  (idx > 6) ? 150 : 0;
                    var sss = flightSegmentDict[segments[0]];
                        if (i == 0)
                        {
                            flight = new TravelApp.Model.Orders.Flight
                            {
                                Destination = sss.Arrival.AirportCode.Value,
                                Origin = sss.Departure.AirportCode.Value,
                                DepartureDate = sss.Departure.Date.ToString("dd-MMM"),
                                MarketingCarrier = sss.MarketingCarrier.AirlineID.Value,
                                CarrierCode = sss.OperatingCarrier.AirlineID.Value,
                                FlightNumber = sss.OperatingCarrier.FlightNumber.Value,
                                TravelTime = sss.FlightDetail.FlightDuration.Value.Replace("P0DT", string.Empty).Replace("0S", string.Empty).Replace("H","H ").Replace("M", "M "),
                                ArrivalDate = sss.Arrival.Date.ToString("dd-MMM"),
                                DepartureTime = sss.Departure.Date.ToString("hh:mm tt"),
                                ArrivalTime = sss.Arrival.Date.ToString("hh:mm tt"),
                                TotalPrice = $"$ {((SimpleCurrencyPriceType)offerItems[0].TotalPriceDetail.TotalAmount.Item).Value}",
                                PriceToCollect = $"$ { priceToPay}",
                                Amenities = new Amenities
                                {
                                    IsLegroom = idx % 2 == 0,
                                    IsLounge = idx % 3 == 0,
                                    IsOnDemandVideo = true,
                                    IsUSB = true,
                                    IsWifi = idx % 2 == 0 
                                }
                            };
                            continue;
                            }
                        if (flight.Connections == null)
                        {
                            flight.Connections = new List<TravelApp.Model.Orders.Flight>();
                        }
                        flight.Connections.Add(new TravelApp.Model.Orders.Flight
                        {
                            Destination = sss.Arrival.AirportCode.Value,
                            Origin = sss.Departure.AirportCode.Value,
                            DepartureDate = sss.Departure.Date.ToString("dd-MMM"),
                            MarketingCarrier = sss.MarketingCarrier.AirlineID.Value,
                            CarrierCode = sss.OperatingCarrier.AirlineID.Value,
                            FlightNumber = sss.OperatingCarrier.FlightNumber.Value,
                            TravelTime = sss.FlightDetail.FlightDuration.Value.Replace("P0DT", string.Empty).Replace("0S", string.Empty).Replace("H", "H ").Replace("M", "M "),
                            ArrivalDate = sss.Arrival.Date.ToString("dd-MMM"),
                            DepartureTime = sss.Departure.Date.ToString("hh:mm tt"),
                            ArrivalTime = sss.Arrival.Date.ToString("hh:mm tt"),
                            TotalPrice = $"$ {((SimpleCurrencyPriceType)offerItems[0].TotalPriceDetail.TotalAmount.Item).Value}",
                            PriceToCollect = $"$ { priceToPay}",
                        });
                   }
                    flights.Add(flight);
                    idx++;
                }
                trips.Add(new TripInfo
                {
                    Flights = flights
                });
              
            }

            shopResponse.FlightResults.Trips = trips;
            return shopResponse;
        }
    }
}
