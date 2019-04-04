using System;
using System.Collections.Generic;
using System.Text;

namespace Provider.Groupon.Schema
{
    
    public class Division
    {
        public string id { get; set; }
        public string name { get; set; }
        public string timezone { get; set; }
        public int timezoneOffsetInSeconds { get; set; }
        public string timezoneIdentifier { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Tag
    {
        public string name { get; set; }
        public string uuid { get; set; }
    }

    public class DisplayOption
    {
        public string name { get; set; }
        public bool enabled { get; set; }
        public string value { get; set; }
    }

    public class TextAd
    {
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string headline { get; set; }
    }

    public class Merchant
    {
        public string id { get; set; }
        public string uuid { get; set; }
        public string name { get; set; }
        public string websiteUrl { get; set; }
        public object facebookUrl { get; set; }
        public object twitterUrl { get; set; }
        public object ratings { get; set; }
    }

    public class CustomField
    {
        public string label { get; set; }
        public string type { get; set; }
        public bool required { get; set; }
    }

    public class Detail
    {
        public string description { get; set; }
    }

    public class Discount
    {
        public int amount { get; set; }
        public string currencyCode { get; set; }
        public string formattedAmount { get; set; }
        public int currencyExponent { get; set; }
    }

    public class Price
    {
        public int amount { get; set; }
        public string currencyCode { get; set; }
        public string formattedAmount { get; set; }
        public int currencyExponent { get; set; }
    }

    public class TakeoutDelivery
    {
        public bool takeout { get; set; }
        public bool delivery { get; set; }
    }

    public class SpecificAttributes
    {
        public TakeoutDelivery takeoutDelivery { get; set; }
    }

    public class Value
    {
        public int amount { get; set; }
        public string currencyCode { get; set; }
        public string formattedAmount { get; set; }
        public int currencyExponent { get; set; }
    }

    public class InventoryService
    {
        public string id { get; set; }
    }

    public class Option
    {
        public int id { get; set; }
        public string uuid { get; set; }
        public bool? bookable { get; set; }
        public string buyUrl { get; set; }
        public CustomField customField { get; set; }
        public List<Detail> details { get; set; }
        public Discount discount { get; set; }
        public int discountPercent { get; set; }
        public DateTime endAt { get; set; }
        public DateTime? expiresAt { get; set; }
        public int? expiresInDays { get; set; }
        public string externalUrl { get; set; }
        public bool isLimitedQuantity { get; set; }
        public bool isSoldOut { get; set; }
        public int maximumPurchaseQuantity { get; set; }
        public int minimumPurchaseQuantity { get; set; }
        public Price price { get; set; }
        public List<object> traits { get; set; }
        public List<object> redemptionLocations { get; set; }
        public int soldQuantity { get; set; }
        public string soldQuantityMessage { get; set; }
        public SpecificAttributes specificAttributes { get; set; }
        public string status { get; set; }
        public string title { get; set; }
        public Value value { get; set; }
        public InventoryService inventoryService { get; set; }
        public List<object> customFields { get; set; }
    }

    public class Deal
    {
        public string id { get; set; }
        public string uuid { get; set; }
        public string dealUrl { get; set; }
        public string title { get; set; }
        public string announcementTitle { get; set; }
        public string shortAnnouncementTitle { get; set; }
        public string finePrint { get; set; }
        public string highlightsHtml { get; set; }
        public string pitchHtml { get; set; }
        public int soldQuantity { get; set; }
        public object limitedQuantityRemaining { get; set; }
        public int tippingPoint { get; set; }
        public string soldQuantityMessage { get; set; }
        public string placeholderUrl { get; set; }
        public string grid4ImageUrl { get; set; }
        public string grid6ImageUrl { get; set; }
        public string largeImageUrl { get; set; }
        public string mediumImageUrl { get; set; }
        public string smallImageUrl { get; set; }
        public string sidebarImageUrl { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public string accessType { get; set; }
        public DateTime endAt { get; set; }
        public DateTime startAt { get; set; }
        public DateTime tippedAt { get; set; }
        public List<object> areas { get; set; }
        public List<object> channels { get; set; }
        public List<object> dealTypes { get; set; }
        public Division division { get; set; }
        public object grouponRating { get; set; }
        public bool allowedInCart { get; set; }
        public bool isInviteOnly { get; set; }
        public bool shippingAddressRequired { get; set; }
        public bool isOptionListComplete { get; set; }
        public bool isMerchandisingDeal { get; set; }
        public bool isNowDeal { get; set; }
        public bool isSoldOut { get; set; }
        public bool isTipped { get; set; }
        public bool isTravelBookableDeal { get; set; }
        public bool isGliveInventoryDeal { get; set; }
        public bool isAutoRefundEnabled { get; set; }
        public bool isGiftable { get; set; }
        public string placementPriority { get; set; }
        public string redemptionLocation { get; set; }
        public string locationNote { get; set; }
        public string vip { get; set; }
        public object fulfillmentMethod { get; set; }
        public List<Tag> tags { get; set; }
        public List<DisplayOption> displayOptions { get; set; }
        public TextAd textAd { get; set; }
        public Merchant merchant { get; set; }
        public List<Option> options { get; set; }
        public object says { get; set; }
        public object salesforceLink { get; set; }
    }

    public class Pagination
    {
        public int offset { get; set; }
        public int count { get; set; }
    }

    public class RelevanceService
    {
        public string persistentRanking { get; set; }
        public string context { get; set; }
        public string treatment { get; set; }
    }

    public class GrouponDeals
    {
        public List<Deal> deals { get; set; }
        public List<object> facets { get; set; }
        public Pagination pagination { get; set; }
        public RelevanceService relevanceService { get; set; }
        public List<object> dealSubsetAttributes { get; set; }
    }
}
