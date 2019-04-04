using System;
using System.Collections.Generic;
using System.Text;

namespace Provider.SkyScanner.Schema
{
    public class SubmittedQuery
    {
        public string market { get; set; }
        public string currency { get; set; }
        public string locale { get; set; }
        public string pickup_place { get; set; }
        public string dropoff_place { get; set; }
        public string pickup_date_time { get; set; }
        public string drop_off_date_time { get; set; }
        public string driver_age { get; set; }
    }

    public class PickUp
    {
        public double distance_to_search_location_in_km { get; set; }
        public List<double> geo_info { get; set; }
        public string address { get; set; }
    }

    public class Location
    {
        public PickUp pick_up { get; set; }
    }

    public class Fuel
    {
        public bool fair { get; set; }
        public string policy { get; set; }
        public string type { get; set; }
    }

    public class Insurance
    {
        public bool? theft_protection { get; set; }
        public bool? free_collision_waiver { get; set; }
        public bool? third_party_cover { get; set; }
    }

    public class IncludedMileage
    {
        public bool unlimited { get; set; }
        public int? included { get; set; }
        public string unit { get; set; }
    }

    public class Paid
    {
        public string currency_id { get; set; }
        public double price { get; set; }
    }

    public class AdditionalDrivers
    {
        public Paid paid { get; set; }
    }

    public class ValueAdd
    {
        public Fuel fuel { get; set; }
        public Insurance insurance { get; set; }
        public IncludedMileage included_mileage { get; set; }
        public bool? free_cancellation { get; set; }
        public bool? free_breakdown_assistance { get; set; }
        public List<string> free_equipments { get; set; }
        public AdditionalDrivers additional_drivers { get; set; }
    }

    public class Car
    {
        public string sipp { get; set; }
        public int image_id { get; set; }
        public double price_all_days { get; set; }
        public int seats { get; set; }
        public int doors { get; set; }
        public int bags { get; set; }
        public bool manual { get; set; }
        public bool air_conditioning { get; set; }
        public bool mandatory_chauffeur { get; set; }
        public string website_id { get; set; }
        public string vehicle { get; set; }
        public string deeplink_url { get; set; }
        public int car_class_id { get; set; }
        public Location location { get; set; }
        public ValueAdd value_add { get; set; }
    }

    public class Website
    {
        public string id { get; set; }
        public string name { get; set; }
        public string image_url { get; set; }
        public bool in_progress { get; set; }
        public bool optimised_for_mobile { get; set; }
    }

    public class Image
    {
        public int id { get; set; }
        public string url { get; set; }
        public string thumbnail { get; set; }
    }

    public class CarClass
    {
        public int id { get; set; }
        public int sort_order { get; set; }
        public string name { get; set; }
    }

    public class DebugItem
    {
        public string type { get; set; }
        public string title { get; set; }
        public string content { get; set; }
    }

    public class CarCollection
    {
        public SubmittedQuery submitted_query { get; set; }
        public List<Car> cars { get; set; }
        public List<Website> websites { get; set; }
        public List<Image> images { get; set; }
        public List<CarClass> car_classes { get; set; }
        public List<DebugItem> debug_items { get; set; }
    }
}
