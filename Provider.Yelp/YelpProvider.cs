using Newtonsoft.Json;
using Provider.Yelp.Schema;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace Provider.Yelp
{
    public class YelpProvider
    {
        private static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };
        public YelpResult GetResturants()
        {
            var url = @"https://partner-api.groupon.com/deals.json?tsToken=US_AFF_0_201236_212556_0&division_id=chicago&filters=category:food-and-drink&offset=0&limit=50";

            YelpResult list;
            try
            {
                list = Invoke<string, YelpResult>("");
            }
            catch (Exception ex)
            {
                list = JsonConvert.DeserializeObject<YelpResult>(StubbedResponse);
            }

            return list;
        }
        //https://api.yelp.com/v3/businesses/search?location=washington,Dc&zip=20005 
        public R Invoke<T, R>(string request, string url = @"https://api.yelp.com/v3/businesses/search", string queryParam = "?location=washington,Dc&zip=20005") where R : class
        {
            try
            {
                if (string.IsNullOrWhiteSpace(url))
                    return default(R);
                var httprequest = WebRequest.Create($"{url}{queryParam}") as HttpWebRequest;
                httprequest.Timeout = 90000;
                httprequest.Method = "GET";
                httprequest.Headers.Add("Content-Type", "application/json");
                httprequest.Headers.Add("Authorization", "Bearer O8q3J6SeFEGYgEHdxoIR3JYNuwDkHsYQz-UziTSPDOHOZPShGSdeyby6zgZqQG2_MChN1hXa6XqV2WSkVOjwXYXp4JnwAeKtY4NEriMmsECBqSQFOj8RLu-zsAscW3Yx");


                var st = new Stopwatch();
                st.Start();

                var response = httprequest.GetResponse() as HttpWebResponse;

                var svcresponse = GetResponse<R>(response);
                if (svcresponse != null)
                {
                    return svcresponse;
                }
            }
            catch (WebException wex)
            {
                bool throwError = true;
                var httpResponse = wex.Response as HttpWebResponse;

                if (httpResponse != null)
                {
                    using (var responseStream = httpResponse.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (var reader = new StreamReader(responseStream))
                            {
                                var failureReason = reader.ReadToEnd();
                                throw new Exception("Web Exception occurred," + failureReason, wex);
                            }
                        }
                    }
                }
                if (throwError)
                    throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }


        private static T GetResponse<T>(HttpWebResponse response)
        {
            if (response != null)
            {
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (var strRead = new StreamReader(responseStream))
                        {
                            var responseText = new StringBuilder();

                            while (!strRead.EndOfStream)
                            {
                                var line = strRead.ReadLine();
                                if (!string.IsNullOrWhiteSpace(line))
                                {
                                    responseText.AppendLine(line);
                                }
                            }

                            return JsonConvert.DeserializeObject<T>(responseText.ToString(), settings);
                        }
                    }
                }
            }
            return default(T);
        }

        private static string StubbedResponse = @"{'businesses': [{'id': 'VA8aPObRynlwR1TGzbzraQ', 'alias': 'founding-farmers-dc-washington-4', 'name': 'Founding Farmers - DC', 'image_url': 'https://s3-media2.fl.yelpcdn.com/bphoto/AWpidG8KMEEIRmqiKEnZHA/o.jpg', 'is_closed': false, 'url': 'https://www.yelp.com/biz/founding-farmers-dc-washington-4?adjust_creative=rg3aCatDNB7OzJpIIvzl1A&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_search&utm_source=rg3aCatDNB7OzJpIIvzl1A', 'review_count': 10850, 'categories': [{'alias': 'tradamerican', 'title': 'American (Traditional)'}, {'alias': 'coffee', 'title': 'Coffee & Tea'}, {'alias': 'breakfast_brunch', 'title': 'Breakfast & Brunch'}], 'rating': 4.0, 'coordinates': {'latitude': 38.900313, 'longitude': -77.044533}, 'transactions': [], 'price': '$$', 'location': {'address1': '1924 Pennsylvania Ave NW', 'address2': '', 'address3': '', 'city': 'Washington, DC', 'zip_code': '20006', 'country': 'US', 'state': 'DC', 'display_address': ['1924 Pennsylvania Ave NW', 'Washington, DC 20006']}, 'phone': '+12028228783', 'display_phone': '(202) 822-8783', 'distance': 1713.489882921938}, {'id': 'iyBbcXtQSBfiwFQZwVBNaQ', 'alias': 'old-ebbitt-grill-washington', 'name': 'Old Ebbitt Grill', 'image_url': 'https://s3-media2.fl.yelpcdn.com/bphoto/KBCezpuAIUVaMH3wGcMCKQ/o.jpg', 'is_closed': false, 'url': 'https://www.yelp.com/biz/old-ebbitt-grill-washington?adjust_creative=rg3aCatDNB7OzJpIIvzl1A&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_search&utm_source=rg3aCatDNB7OzJpIIvzl1A', 'review_count': 6572, 'categories': [{'alias': 'bars', 'title': 'Bars'}, {'alias': 'tradamerican', 'title': 'American (Traditional)'}, {'alias': 'breakfast_brunch', 'title': 'Breakfast & Brunch'}], 'rating': 4.0, 'coordinates': {'latitude': 38.898005, 'longitude': -77.033362}, 'transactions': [], 'price': '$$', 'location': {'address1': '675 15th St NW', 'address2': '', 'address3': '', 'city': 'Washington, DC', 'zip_code': '20005', 'country': 'US', 'state': 'DC', 'display_address': ['675 15th St NW', 'Washington, DC 20005']}, 'phone': '+12023474800', 'display_phone': '(202) 347-4800', 'distance': 1496.279982474307}, {'id': 'Es64Je53efmpWh-BXHjguQ', 'alias': 'baked-and-wired-washington', 'name': 'Baked & Wired', 'image_url': 'https://s3-media2.fl.yelpcdn.com/bphoto/Z2rqsW-2Hu4H5_HQPk42sg/o.jpg', 'is_closed': false, 'url': 'https://www.yelp.com/biz/baked-and-wired-washington?adjust_creative=rg3aCatDNB7OzJpIIvzl1A&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_search&utm_source=rg3aCatDNB7OzJpIIvzl1A', 'review_count': 3760, 'categories': [{'alias': 'bakeries', 'title': 'Bakeries'}, {'alias': 'coffee', 'title': 'Coffee & Tea'}, {'alias': 'cupcakes', 'title': 'Cupcakes'}], 'rating': 4.5, 'coordinates': {'latitude': 38.9039128490789, 'longitude': -77.0602477800762}, 'transactions': [], 'price': '$$', 'location': {'address1': '1052 Thomas Jefferson St NW', 'address2': null, 'address3': '', 'city': 'Washington, DC', 'zip_code': '20007', 'country': 'US', 'state': 'DC', 'display_address': ['1052 Thomas Jefferson St NW', 'Washington, DC 20007']}, 'phone': '+12023332500', 'display_phone': '(202) 333-2500', 'distance': 2690.1563144017964}, {'id': 'GBkFa8TJwkaUJsJXXGkTTg', 'alias': 'zaytinya-washington', 'name': 'Zaytinya', 'image_url': 'https://s3-media2.fl.yelpcdn.com/bphoto/qf8Mc1iHho1dN-CJFItEwQ/o.jpg', 'is_closed': false, 'url': 'https://www.yelp.com/biz/zaytinya-washington?adjust_creative=rg3aCatDNB7OzJpIIvzl1A&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_search&utm_source=rg3aCatDNB7OzJpIIvzl1A', 'review_count': 3957, 'categories': [{'alias': 'greek', 'title': 'Greek'}, {'alias': 'turkish', 'title': 'Turkish'}, {'alias': 'lebanese', 'title': 'Lebanese'}], 'rating': 4.0, 'coordinates': {'latitude': 38.89904, 'longitude': -77.02349}, 'transactions': [], 'price': '$$$', 'location': {'address1': '701 9th St NW', 'address2': '', 'address3': '', 'city': 'Washington, DC', 'zip_code': '20001', 'country': 'US', 'state': 'DC', 'display_address': ['701 9th St NW', 'Washington, DC 20001']}, 'phone': '+12026380800', 'display_phone': '(202) 638-0800', 'distance': 1481.365422299578}, {'id': 'CwdlygqT4cWwOtQGsYdoBw', 'alias': 'rasika-washington', 'name': 'Rasika', 'image_url': 'https://s3-media4.fl.yelpcdn.com/bphoto/rkzs8Jh88L7SiUYbiD_4Lw/o.jpg', 'is_closed': false, 'url': 'https://www.yelp.com/biz/rasika-washington?adjust_creative=rg3aCatDNB7OzJpIIvzl1A&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_search&utm_source=rg3aCatDNB7OzJpIIvzl1A', 'review_count': 2635, 'categories': [{'alias': 'indpak', 'title': 'Indian'}], 'rating': 4.5, 'coordinates': {'latitude': 38.8950080871582, 'longitude': -77.0212860107422}, 'transactions': [], 'price': '$$$', 'location': {'address1': '633 D St NW', 'address2': '', 'address3': '', 'city': 'Washington, DC', 'zip_code': '20004', 'country': 'US', 'state': 'DC', 'display_address': ['633 D St NW', 'Washington, DC 20004']}, 'phone': '+12026371222', 'display_phone': '(202) 637-1222', 'distance': 1982.3501381732194}, {'id': 'j9qYRR8HCXm_GEnetijOGA', 'alias': 'le-diplomate-washington', 'name': 'Le Diplomate', 'image_url': 'https://s3-media2.fl.yelpcdn.com/bphoto/2EljPz-cdiTQa6wTsIbI7Q/o.jpg', 'is_closed': false, 'url': 'https://www.yelp.com/biz/le-diplomate-washington?adjust_creative=rg3aCatDNB7OzJpIIvzl1A&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_search&utm_source=rg3aCatDNB7OzJpIIvzl1A', 'review_count': 2460, 'categories': [{'alias': 'brasseries', 'title': 'Brasseries'}, {'alias': 'french', 'title': 'French'}, {'alias': 'cafes', 'title': 'Cafes'}], 'rating': 4.5, 'coordinates': {'latitude': 38.911359, 'longitude': -77.031575}, 'transactions': [], 'price': '$$$', 'location': {'address1': '1601 14th St NW', 'address2': '', 'address3': '', 'city': 'Washington, DC', 'zip_code': '20009', 'country': 'US', 'state': 'DC', 'display_address': ['1601 14th St NW', 'Washington, DC 20009']}, 'phone': '+12023323333', 'display_phone': '(202) 332-3333', 'distance': 88.94181251452841}, {'id': 'a-5Jd4irVqkaxRs-AQ-HGw', 'alias': 'roses-luxury-washington', 'name': 'Rose's Luxury', 'image_url': 'https://s3-media3.fl.yelpcdn.com/bphoto/AwhITdGBJkXIYG_ylabsQg/o.jpg', 'is_closed': false, 'url': 'https://www.yelp.com/biz/roses-luxury-washington?adjust_creative=rg3aCatDNB7OzJpIIvzl1A&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_search&utm_source=rg3aCatDNB7OzJpIIvzl1A', 'review_count': 1679, 'categories': [{'alias': 'newamerican', 'title': 'American (New)'}, {'alias': 'italian', 'title': 'Italian'}], 'rating': 4.5, 'coordinates': {'latitude': 38.88063, 'longitude': -76.99534}, 'transactions': [], 'price': '$$$', 'location': {'address1': '717 8th St SE', 'address2': null, 'address3': '', 'city': 'Washington, DC', 'zip_code': '20003', 'country': 'US', 'state': 'DC', 'display_address': ['717 8th St SE', 'Washington, DC 20003']}, 'phone': '+12025808889', 'display_phone': '(202) 580-8889', 'distance': 4577.759949213042}, {'id': 'EIenjUykvfWCNFtPMSHCsw', 'alias': 'lincoln-memorial-washington-2', 'name': 'Lincoln Memorial', 'image_url': 'https://s3-media4.fl.yelpcdn.com/bphoto/jMgApybamWU7NlPiUiGWiw/o.jpg', 'is_closed': false, 'url': 'https://www.yelp.com/biz/lincoln-memorial-washington-2?adjust_creative=rg3aCatDNB7OzJpIIvzl1A&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_search&utm_source=rg3aCatDNB7OzJpIIvzl1A', 'review_count': 822, 'categories': [{'alias': 'landmarks', 'title': 'Landmarks & Historical Buildings'}], 'rating': 5.0, 'coordinates': {'latitude': 38.8893459813715, 'longitude': -77.0502001422584}, 'transactions': [], 'location': {'address1': 'Lincoln Memorial Cir NW', 'address2': '', 'address3': '', 'city': 'Washington, DC', 'zip_code': '20002', 'country': 'US', 'state': 'DC', 'display_address': ['Lincoln Memorial Cir NW', 'Washington, DC 20002']}, 'phone': '+12024266841', 'display_phone': '(202) 426-6841', 'distance': 2970.0232854170877}, {'id': 'MVP0Mta7rmF-Nbc1cIyIrQ', 'alias': 'amsterdam-falafelshop-washington-4', 'name': 'Amsterdam Falafelshop', 'image_url': 'https://s3-media1.fl.yelpcdn.com/bphoto/jQinDUgIGDqJWZzw4XqKaA/o.jpg', 'is_closed': false, 'url': 'https://www.yelp.com/biz/amsterdam-falafelshop-washington-4?adjust_creative=rg3aCatDNB7OzJpIIvzl1A&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_search&utm_source=rg3aCatDNB7OzJpIIvzl1A', 'review_count': 1789, 'categories': [{'alias': 'vegetarian', 'title': 'Vegetarian'}, {'alias': 'mideastern', 'title': 'Middle Eastern'}, {'alias': 'falafel', 'title': 'Falafel'}], 'rating': 4.5, 'coordinates': {'latitude': 38.921199798584, 'longitude': -77.041862487793}, 'transactions': ['delivery', 'pickup'], 'price': '$', 'location': {'address1': '2425 18th St NW', 'address2': null, 'address3': '', 'city': 'Washington, DC', 'zip_code': '20009', 'country': 'US', 'state': 'DC', 'display_address': ['2425 18th St NW', 'Washington, DC 20009']}, 'phone': '+12022341969', 'display_phone': '(202) 234-1969', 'distance': 1468.3729796611433}, {'id': 'zeTqaVpvssznHt_1IxChXQ', 'alias': 'blue-duck-tavern-washington', 'name': 'Blue Duck Tavern', 'image_url': 'https://s3-media2.fl.yelpcdn.com/bphoto/_U3d8g-JyZ3RgkhBvXCVgg/o.jpg', 'is_closed': false, 'url': 'https://www.yelp.com/biz/blue-duck-tavern-washington?adjust_creative=rg3aCatDNB7OzJpIIvzl1A&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_search&utm_source=rg3aCatDNB7OzJpIIvzl1A', 'review_count': 2022, 'categories': [{'alias': 'newamerican', 'title': 'American (New)'}, {'alias': 'breakfast_brunch', 'title': 'Breakfast & Brunch'}, {'alias': 'cocktailbars', 'title': 'Cocktail Bars'}], 'rating': 4.0, 'coordinates': {'latitude': 38.9055917636612, 'longitude': -77.0511932944776}, 'transactions': [], 'price': '$$$', 'location': {'address1': '1201 24th St NW', 'address2': '', 'address3': '', 'city': 'Washington, DC', 'zip_code': '20037', 'country': 'US', 'state': 'DC', 'display_address': ['1201 24th St NW', 'Washington, DC 20037']}, 'phone': '+12024196755', 'display_phone': '(202) 419-6755', 'distance': 1888.0645039295632}, {'id': 'SpCeYPhky4gsWa9-IBtw2A', 'alias': 'a-baked-joint-washington-9', 'name': 'A Baked Joint', 'image_url': 'https://s3-media1.fl.yelpcdn.com/bphoto/iTBw1KHg5twjvhNbvYjIDg/o.jpg', 'is_closed': false, 'url': 'https://www.yelp.com/biz/a-baked-joint-washington-9?adjust_creative=rg3aCatDNB7OzJpIIvzl1A&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_search&utm_source=rg3aCatDNB7OzJpIIvzl1A', 'review_count': 1239, 'categories': [{'alias': 'coffee', 'title': 'Coffee & Tea'}, {'alias': 'breakfast_brunch', 'title': 'Breakfast & Brunch'}, {'alias': 'sandwiches', 'title': 'Sandwiches'}], 'rating': 4.5, 'coordinates': {'latitude': 38.9024108024879, 'longitude': -77.0171392790004}, 'transactions': [], 'price': '$', 'location': {'address1': '440 K St NW', 'address2': '', 'address3': '', 'city': 'Washington, DC', 'zip_code': '20001', 'country': 'US', 'state': 'DC', 'display_address': ['440 K St NW', 'Washington, DC 20001']}, 'phone': '+12024086985', 'display_phone': '(202) 408-6985', 'distance': 1529.768632974741}, {'id': '5xuqyS6zIg5uXuK-fya5Kg', 'alias': 'keren-restaurant-washington', 'name': 'Keren Restaurant', 'image_url': 'https://s3-media4.fl.yelpcdn.com/bphoto/aNeonDxPGL11eXUOcB35vw/o.jpg', 'is_closed': false, 'url': 'https://www.yelp.com/biz/keren-restaurant-washington?adjust_creative=rg3aCatDNB7OzJpIIvzl1A&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_search&utm_source=rg3aCatDNB7OzJpIIvzl1A', 'review_count': 889, 'categories': [{'alias': 'ethiopian', 'title': 'Ethiopian'}, {'alias': 'coffee', 'title': 'Coffee & Tea'}], 'rating': 4.5, 'coordinates': {'latitude': 38.916715, 'longitude': -77.041243}, 'transactions': [], 'price': '$$', 'location': {'address1': '1780 Florida Ave NW', 'address2': '', 'address3': '', 'city': 'Washington, DC', 'zip_code': '20009', 'country': 'US', 'state': 'DC', 'display_address': ['1780 Florida Ave NW', 'Washington, DC 20009']}, 'phone': '+12022655764', 'display_phone': '(202) 265-5764', 'distance': 1097.7370143344592}, {'id': 'GlAT_ksg5ftJ87PXwXmoVA', 'alias': 'fogo-de-ch\u00e3o-brazilian-steakhouse-washington-5', 'name': 'Fogo de Ch\u00e3o Brazilian Steakhouse', 'image_url': 'https://s3-media2.fl.yelpcdn.com/bphoto/02rMGZefw00X6R4o18pdQw/o.jpg', 'is_closed': false, 'url': 'https://www.yelp.com/biz/fogo-de-ch%C3%A3o-brazilian-steakhouse-washington-5?adjust_creative=rg3aCatDNB7OzJpIIvzl1A&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_search&utm_source=rg3aCatDNB7OzJpIIvzl1A', 'review_count': 1576, 'categories': [{'alias': 'brazilian', 'title': 'Brazilian'}, {'alias': 'steak', 'title': 'Steakhouses'}], 'rating': 4.0, 'coordinates': {'latitude': 38.8951162456108, 'longitude': -77.0273284769837}, 'transactions': [], 'price': '$$$', 'location': {'address1': '1101 Pennsylvania Ave NW', 'address2': '', 'address3': '', 'city': 'Washington, DC', 'zip_code': '20004', 'country': 'US', 'state': 'DC', 'display_address': ['1101 Pennsylvania Ave NW', 'Washington, DC 20004']}, 'phone': '+12023474668', 'display_phone': '(202) 347-4668', 'distance': 1821.511131655726}, {'id': 'LofSIdn9SeuHs09WU1IdGw', 'alias': 'daikaya-ramen-shop-washington-2', 'name': 'Daikaya Ramen Shop', 'image_url': 'https://s3-media3.fl.yelpcdn.com/bphoto/xn4SJkSDEIQuaUWZ9gCgWw/o.jpg', 'is_closed': false, 'url': 'https://www.yelp.com/biz/daikaya-ramen-shop-washington-2?adjust_creative=rg3aCatDNB7OzJpIIvzl1A&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_search&utm_source=rg3aCatDNB7OzJpIIvzl1A', 'review_count': 2430, 'categories': [{'alias': 'ramen', 'title': 'Ramen'}], 'rating': 4.0, 'coordinates': {'latitude': 38.8986, 'longitude': -77.0195899}, 'transactions': [], 'price': '$$', 'location': {'address1': '705 6th St NW', 'address2': 'Fl 1', 'address3': null, 'city': 'Washington, DC', 'zip_code': '20001', 'country': 'US', 'state': 'DC', 'display_address': ['705 6th St NW', 'Fl 1', 'Washington, DC 20001']}, 'phone': '+12025891600', 'display_phone': '(202) 589-1600', 'distance': 1704.5909854841868}, {'id': 'U0tfep9yNBASTe2zAG6cPw', 'alias': 'filomena-ristorante-washington', 'name': 'Filomena Ristorante', 'image_url': 'https://s3-media2.fl.yelpcdn.com/bphoto/wmLNYaL2CRQ8L9unXuUIgw/o.jpg', 'is_closed': false, 'url': 'https://www.yelp.com/biz/filomena-ristorante-washington?adjust_creative=rg3aCatDNB7OzJpIIvzl1A&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_search&utm_source=rg3aCatDNB7OzJpIIvzl1A', 'review_count': 1987, 'categories': [{'alias': 'italian', 'title': 'Italian'}, {'alias': 'wine_bars', 'title': 'Wine Bars'}], 'rating': 4.0, 'coordinates': {'latitude': 38.90443, 'longitude': -77.062546}, 'transactions': [], 'price': '$$$', 'location': {'address1': '1063 Wisconsin Ave NW', 'address2': '', 'address3': '', 'city': 'Washington, DC', 'zip_code': '20007', 'country': 'US', 'state': 'DC', 'display_address': ['1063 Wisconsin Ave NW', 'Washington, DC 20007']}, 'phone': '+12023388800', 'display_phone': '(202) 338-8800', 'distance': 2864.30920269697}, {'id': 'sxNUpaApjh_54-l9RcJRDA', 'alias': 'farmers-fishers-bakers-washington', 'name': 'Farmers Fishers Bakers', 'image_url': 'https://s3-media2.fl.yelpcdn.com/bphoto/sYT0ZP0ZRzlvpGYY0SV1Xg/o.jpg', 'is_closed': false, 'url': 'https://www.yelp.com/biz/farmers-fishers-bakers-washington?adjust_creative=rg3aCatDNB7OzJpIIvzl1A&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_search&utm_source=rg3aCatDNB7OzJpIIvzl1A', 'review_count': 2790, 'categories': [{'alias': 'newamerican', 'title': 'American (New)'}, {'alias': 'breakfast_brunch', 'title': 'Breakfast & Brunch'}, {'alias': 'vegetarian', 'title': 'Vegetarian'}], 'rating': 4.0, 'coordinates': {'latitude': 38.90169, 'longitude': -77.060072}, 'transactions': [], 'price': '$$', 'location': {'address1': '3000 K St NW', 'address2': '', 'address3': '', 'city': 'Washington, DC', 'zip_code': '20007', 'country': 'US', 'state': 'DC', 'display_address': ['3000 K St NW', 'Washington, DC 20007']}, 'phone': '+12022988783', 'display_phone': '(202) 298-8783', 'distance': 2728.3825720485625}, {'id': '9X0F8gl4mPSSFHkxSB2lXA', 'alias': 'il-canale-washington-2', 'name': 'il Canale', 'image_url': 'https://s3-media1.fl.yelpcdn.com/bphoto/vVRuHU743nlLAM57A3FraQ/o.jpg', 'is_closed': false, 'url': 'https://www.yelp.com/biz/il-canale-washington-2?adjust_creative=rg3aCatDNB7OzJpIIvzl1A&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_search&utm_source=rg3aCatDNB7OzJpIIvzl1A', 'review_count': 1912, 'categories': [{'alias': 'italian', 'title': 'Italian'}, {'alias': 'pizza', 'title': 'Pizza'}], 'rating': 4.0, 'coordinates': {'latitude': 38.904493, 'longitude': -77.0609744}, 'transactions': ['pickup'], 'price': '$$', 'location': {'address1': '1065 31st St NW', 'address2': '', 'address3': '', 'city': 'Washington, DC', 'zip_code': '20007', 'country': 'US', 'state': 'DC', 'display_address': ['1065 31st St NW', 'Washington, DC 20007']}, 'phone': '+12023374444', 'display_phone': '(202) 337-4444', 'distance': 2731.530822512644}, {'id': '_oMDRU4VSgzGKlTJyfmMzQ', 'alias': 'matchbox-chinatown-washington', 'name': 'Matchbox - Chinatown', 'image_url': 'https://s3-media3.fl.yelpcdn.com/bphoto/flBXeTuihI44NrKceUUjRQ/o.jpg', 'is_closed': false, 'url': 'https://www.yelp.com/biz/matchbox-chinatown-washington?adjust_creative=rg3aCatDNB7OzJpIIvzl1A&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_search&utm_source=rg3aCatDNB7OzJpIIvzl1A', 'review_count': 2674, 'categories': [{'alias': 'pizza', 'title': 'Pizza'}, {'alias': 'newamerican', 'title': 'American (New)'}], 'rating': 4.0, 'coordinates': {'latitude': 38.90005, 'longitude': -77.02254}, 'transactions': [], 'price': '$$', 'location': {'address1': '713 H St NW', 'address2': '', 'address3': '', 'city': 'Washington, DC', 'zip_code': '20001', 'country': 'US', 'state': 'DC', 'display_address': ['713 H St NW', 'Washington, DC 20001']}, 'phone': '+12022894441', 'display_phone': '(202) 289-4441', 'distance': 1436.160987585427}, {'id': 'po1he5ZMcOJ1TOqoVQfaBA', 'alias': 'busboys-and-poets-14th-and-v-washington-2', 'name': 'Busboys and Poets - 14th and V', 'image_url': 'https://s3-media2.fl.yelpcdn.com/bphoto/n1Z7RR4MufIhwURZh8_CzQ/o.jpg', 'is_closed': false, 'url': 'https://www.yelp.com/biz/busboys-and-poets-14th-and-v-washington-2?adjust_creative=rg3aCatDNB7OzJpIIvzl1A&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_search&utm_source=rg3aCatDNB7OzJpIIvzl1A', 'review_count': 1911, 'categories': [{'alias': 'breakfast_brunch', 'title': 'Breakfast & Brunch'}, {'alias': 'tradamerican', 'title': 'American (Traditional)'}], 'rating': 4.0, 'coordinates': {'latitude': 38.917924, 'longitude': -77.031616}, 'transactions': [], 'price': '$$', 'location': {'address1': '2021 14th St NW', 'address2': '', 'address3': '', 'city': 'Washington, DC', 'zip_code': '20009', 'country': 'US', 'state': 'DC', 'display_address': ['2021 14th St NW', 'Washington, DC 20009']}, 'phone': '+12023877638', 'display_phone': '(202) 387-7638', 'distance': 742.0924714081549}, {'id': 'HQ404fe3ndkAprE00uxWLA', 'alias': 'toki-underground-washington', 'name': 'Toki Underground', 'image_url': 'https://s3-media3.fl.yelpcdn.com/bphoto/wY4xWF6-khRhnJLQGAwXDg/o.jpg', 'is_closed': false, 'url': 'https://www.yelp.com/biz/toki-underground-washington?adjust_creative=rg3aCatDNB7OzJpIIvzl1A&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_search&utm_source=rg3aCatDNB7OzJpIIvzl1A', 'review_count': 1932, 'categories': [{'alias': 'ramen', 'title': 'Ramen'}], 'rating': 4.0, 'coordinates': {'latitude': 38.9002596732333, 'longitude': -76.9890601862829}, 'transactions': [], 'price': '$$', 'location': {'address1': '1234 H St NE', 'address2': '', 'address3': '', 'city': 'Washington, DC', 'zip_code': '20002', 'country': 'US', 'state': 'DC', 'display_address': ['1234 H St NE', 'Washington, DC 20002']}, 'phone': '+12023883086', 'display_phone': '(202) 388-3086', 'distance': 3801.296443008847}], 'total': 8400, 'region': {'center': {'longitude': -77.0306396484375, 'latitude': 38.911293573163775}}}";
    }

}
