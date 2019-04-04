using System;
using System.Collections.Generic;
using System.Text;

namespace Provider.SkyScanner.Schema
{
  
    public class Partner
    {
        public bool is_dbook { get; set; }
        public string logo { get; set; }
        public string website_id { get; set; }
        public string partner_type { get; set; }
        public string name { get; set; }
        public bool is_official { get; set; }
    }

    public class MapBoundary
    {
        public double s_w_lng { get; set; }
        public double n_e_lng { get; set; }
        public double n_e_lat { get; set; }
        public double s_w_lat { get; set; }
    }

    public class OfficialCenter
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class Centroid
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class Entity
    {
        public string entity_type { get; set; }
        public string entity_id { get; set; }
        public string name { get; set; }
        public OfficialCenter official_center { get; set; }
        public Centroid centroid { get; set; }
    }

    public class Offer
    {
        public string partner_id { get; set; }
        public object closed_user_groups { get; set; }
        public string meal_plan { get; set; }
        public double price { get; set; }
        public List<string> room_type { get; set; }
        public string cancellation { get; set; }
        public string deeplink { get; set; }
        public object strike_through { get; set; }
        public object available { get; set; }
        public double price_gbp { get; set; }
        public bool is_official { get; set; }
        public object cancellation_text { get; set; }
        public object dbook_link { get; set; }
    }

    public class Rating
    {
        public string desc { get; set; }
        public double value { get; set; }
    }

    public class Hotel
    {
        public string hotel_id { get; set; }
        public string chain { get; set; }
        public string stars { get; set; }
        public List<object> group_name { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public double distance { get; set; }
        public string checkout_time { get; set; }
        public List<Offer> offers { get; set; }
        public double reviews_count { get; set; }
        public List<string> amenities { get; set; }
        public List<Image> images { get; set; }
        public string chain_id { get; set; }
        public Rating rating { get; set; }
        public List<object> group_id { get; set; }
        public string property_type { get; set; }
        public string name { get; set; }
        public List<double> coordinates { get; set; }
        public string checkin_time { get; set; }
    }


    public class Chain
    {
        public object max_price { get; set; }
        public string id { get; set; }
        public double? min_price { get; set; }
        public int count { get; set; }
    }

    public class Amenity
    {
        public object max_price { get; set; }
        public string id { get; set; }
        public double? min_price { get; set; }
        public int count { get; set; }
    }

    public class MealPlan
    {
        public object max_price { get; set; }
        public string id { get; set; }
        public double min_price { get; set; }
        public int count { get; set; }
    }

    public class Cancellation
    {
        public object max_price { get; set; }
        public string id { get; set; }
        public double min_price { get; set; }
        public int count { get; set; }
    }

    public class PropertyType
    {
        public object max_price { get; set; }
        public string id { get; set; }
        public double? min_price { get; set; }
        public int count { get; set; }
    }

    public class PriceBucket
    {
        public double? max_price { get; set; }
        public string id { get; set; }
        public double? min_price { get; set; }
        public int count { get; set; }
    }

    public class Star
    {
        public object max_price { get; set; }
        public string id { get; set; }
        public double min_price { get; set; }
        public int count { get; set; }
    }

    public class City
    {
        public object max_price { get; set; }
        public string id { get; set; }
        public double min_price { get; set; }
        public int count { get; set; }
    }

    public class District
    {
        public object max_price { get; set; }
        public string id { get; set; }
        public double? min_price { get; set; }
        public int count { get; set; }
    }

    public class Filters
    {
        public List<Chain> chain { get; set; }
        public List<Amenity> amenities { get; set; }
        public List<MealPlan> meal_plan { get; set; }
        public List<Cancellation> cancellation { get; set; }
        public List<PropertyType> property_type { get; set; }
        public List<PriceBucket> price_buckets { get; set; }
        public List<Star> stars { get; set; }
        public List<City> city { get; set; }
        public List<District> district { get; set; }
    }

    public class Results
    {
        public List<Partner> partners { get; set; }
        public MapBoundary map_boundary { get; set; }
        public List<object> price_includes { get; set; }
        public Entity entity { get; set; }
        public object hotel_pivot { get; set; }
        public List<Hotel> hotels { get; set; }
        public List<Location> location { get; set; }
        public Filters filters { get; set; }
    }

    public class Meta
    {
        public double total { get; set; }
        public int completion_percentage { get; set; }
        public string status { get; set; }
        public int total_available { get; set; }
        public int hotels_filtered { get; set; }
        public int offers { get; set; }
    }

    public class HotelCollection
    {
        public string correlation_id { get; set; }
        public Results results { get; set; }
        public Meta meta { get; set; }
        public int count { get; set; }
    }
}
