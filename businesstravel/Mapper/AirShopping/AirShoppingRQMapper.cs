
using Provider.UAApi.Schema;
using Provider.UAApi.Schema.AirShopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelApp.Model.Orders;

namespace Provider.NDCApi.Mapper.AirShopping
{
    public static class AirShoppingRQMapper
    {
        public static AirShoppingRQ Map(this ShopRequest shopRequest)
        {
            /*
             * <AirShoppingRQ EchoToken="{{$guid}}" Version="IATA2017.2" xmlns="http://www.iata.org/IATA/EDIST/2017.2">
  <Document>
    <Name>KRONOS NDC GATEWAY</Name>
    <ReferenceVersion>1.0</ReferenceVersion>
  </Document>
  <Party>
    <Sender>
      <TravelAgencySender>
        <Name>JR TECHNOLOGIES</Name>
        <IATA_Number>20200154</IATA_Number>
        <AgencyID>00010080</AgencyID>
      </TravelAgencySender>
    </Sender>
  </Party>
  <CoreQuery>
    <OriginDestinations>
      <OriginDestination>
        <Departure>
          <AirportCode>LHR</AirportCode>
          <Date>2018-12-25</Date>
        </Departure>
        <Arrival>
          <AirportCode>BCN</AirportCode>
        </Arrival>
      </OriginDestination>
      <OriginDestination>
        <Departure>
          <AirportCode>BCN</AirportCode>
          <Date>2018-12-29</Date>
        </Departure>
        <Arrival>
          <AirportCode>LHR</AirportCode>
        </Arrival>
      </OriginDestination>
    </OriginDestinations>
  </CoreQuery>
  <Preference>
    <CabinPreferences>
      <CabinType>
        <Code>C</Code>
      </CabinType>
    </CabinPreferences>
  </Preference>
  <DataLists>
    <PassengerList>
      <Passenger PassengerID="SH1">
        <PTC>ADT</PTC>
      </Passenger>
    </PassengerList>
  </DataLists>
</AirShoppingRQ>
             */
            return new AirShoppingRQ
            {
                EchoToken = shopRequest.RequestId ?? Guid.NewGuid().ToString(),
                Version= "17.2",
                PointOfSale = new PointOfSaleType
                {
                    Location = new PointOfSaleTypeLocation
                    {
                        CityCode = new CityCode { Value = "ORD" },
                        CountryCode = new CountryCode { Value = "US" },
                    },
                },
                Document = new MsgDocumentType
                {
                    Name = "UNITED NDC GATEWAY",
                    ReferenceVersion = "17.2"
                },
                Party = new MsgPartiesType
                {

                    Sender = new MsgPartiesTypeSender
                    {
                        Item = new TravelAgencySenderType1
                        {
                            Name = "United Airlines",
                            PseudoCity = new AgencyCoreRepTypePseudoCity1 { Value = "AJJB" },
                            IATA_Number = "20200154",
                            AgencyID = new AgencyID_Type1
                            {
                                Value = "00101603"
                            },
                            AgentUser = new AgentUserType1
                            {
                                Name = "falmeida",
                                AgentUserID = new AgentUserTypeAgentUserID1 { Value = "falmeida" },
                                UserRole = "Ticketing Agent",
                            }
                        }
                    },
                },
                    
                CoreQuery = new AirShoppingRQCoreQuery
                {
                    Item =  new AirShopReqAttributeQueryType
                    {
                        OriginDestination = shopRequest.Itineary.Trips.Select(t=>  new AirShopReqAttributeQueryTypeOriginDestination
                        {
                            OriginDestinationKey = "OD" + shopRequest.Itineary.Trips.IndexOf(t) + 1,
                            
                            ShoppingResponseID = string.IsNullOrWhiteSpace(t.SelectedFlightId) ? null : new ShoppingResponseID_Type
                            {
                                ResponseID = new ShoppingResponseID_TypeResponseID
                                {
                                    Value = t.SelectedFlightId
                                }
                            },
                           
                           Departure = new Departure
                           {
                               AirportCode = new FlightDepartureTypeAirportCode
                               {
                                   Value = t.Origin,
                               },
                               Date = DateTime.Parse(t.DepartureDate)
                           
                           },
                            Arrival = new FlightArrivalType
                            {
                                AirportCode = new FlightArrivalTypeAirportCode
                                {
                                    Value = t.Destination,
                                },
                                Date = DateTime.Parse(t.DepartureDate)
                            },
                            
                            
                        }).ToList()
                    }
                },
                Preference = new AirShoppingRQPreference
                {
                    CabinPreferences = new CabinPreferencesType
                    {
                        CabinType = new List<CabinType>
                        {
                            new CabinType
                            {
                                Code = "C"
                            }
                        },
                    },
                },
                DataLists = new AirShoppingRQDataLists
                { 
                    PassengerList = new List<PassengerType>
                    {
                        new PassengerType
                        {
                            PassengerID = "P1",
                            PTC = "ADT",
                        }
                    }
                }
            };
        }
    }
}
