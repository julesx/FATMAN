using System.Collections.Generic;

namespace FATMAN.Json_Structures
{
    public class ThumbImage
    {
        public int fileSize { get; set; }
        public string fileType { get; set; }
        public string binId { get; set; }
        public string fileName { get; set; }
        public string url { get; set; }
    }

    public class Date
    {
        public string startDateTime { get; set; }
        public string endDateTime { get; set; }
    }

    public class Coords
    {
        public string lng { get; set; }
        public string lat { get; set; }
    }

    public class Location
    {
        public string id { get; set; }
        public Coords coords { get; set; }
        public bool geoCoded { get; set; }
        public string address { get; set; }
        public string transitInfo { get; set; }
        public string locationName { get; set; }
        public string type { get; set; }
    }

    public class Features
    {
    }

    public class Conv
    {
        public string DominoImage { get; set; }
    }

    public class Image
    {
        public int fileSize { get; set; }
        public string fileType { get; set; }
        public string binId { get; set; }
        public string fileName { get; set; }
        public string altText { get; set; }
        public string url { get; set; }
    }

    public class Cost
    {
    }

    public class Category
    {
        public string name { get; set; }
    }

    public class NewsletterCategory
    {
        public string value { get; set; }
    }

    public class Admin
    {
        public string reviewerComments { get; set; }
        public string updateTimestamp { get; set; }
        public bool includeInNewsletter { get; set; }
        public List<NewsletterCategory> newsletterCategory { get; set; }
        public string featuredEvent { get; set; }
        public List<object> newsletterSubcategory { get; set; }
        public string approvedTimestamp { get; set; }
        public string updatedBy { get; set; }
    }

    public class CalEvent
    {
        public string orgType { get; set; }
        public string startDate { get; set; }
        public ThumbImage thumbImage { get; set; }
        public string startDateTime { get; set; }
        public string orgAddress { get; set; }
        public string terms { get; set; }
        public string endDate { get; set; }
        public string frequency { get; set; }
        public string partnerType { get; set; }
        public string eventWebsite { get; set; }
        public string orgPhone { get; set; }
        public string eventEmail { get; set; }
        public List<Date> dates { get; set; }
        public List<Location> locations { get; set; }
        public string description { get; set; }
        public string orgEmail { get; set; }
        public string otherCostInfo { get; set; }
        public Features features { get; set; }
        public string categoryString { get; set; }
        public Conv conv { get; set; }
        public string themeString { get; set; }
        public string expectedPeak { get; set; }
        public string accessibility { get; set; }
        public string partnerName { get; set; }
        public List<object> theme { get; set; }
        public Image image { get; set; }
        public string reservationsRequired { get; set; }
        public string recId { get; set; }
        public string orgName { get; set; }
        public Cost cost { get; set; }
        public string endDateTime { get; set; }
        public List<Category> category { get; set; }
        public string contactName { get; set; }
        public Admin admin { get; set; }
        public string freeEvent { get; set; }
        public string eventName { get; set; }
        public string alcoholServed { get; set; }
    }

    public class RootObject
    {
        public CalEvent calEvent { get; set; }
    }
}
