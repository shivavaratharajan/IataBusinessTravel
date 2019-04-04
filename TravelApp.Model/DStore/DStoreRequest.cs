using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Model.DStore
{
   public class DStoreRequest
    {
        public string RequestId { get; set; }
        public string CompanyId { get; set; }
        public string ProfileId { get; set; }
        public string Location { get; set; }
    }

    public class DStoreResponse
    {
        public List<Offer> Offers { get; set; }
    }

    public class Offer
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Price { get; set; }
    }
}
