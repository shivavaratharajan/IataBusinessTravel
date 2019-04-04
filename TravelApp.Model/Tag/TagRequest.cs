using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Model.Tag
{
    public class TagRequest
    {
        public string RequestId { get; set; }
        public string CompanyId { get; set; }
        public string ProfileId { get; set; }
        public string Location { get; set; }
    }

    public class TagResponse
    {
        public List<TagOffer> Offers { get; set; }
    }

    public class TagOffer
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Price { get; set; }
    }
}
