using System;
namespace DotNetCoreSqlDb.Models
{
    public class Restaurant
    {
        public int ID { get; set; }
        public string ImageUrl { get; set; }
        public string Phone { get; set; }
        public string Url { get; set; }
        public float Rating { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
        public string Address { get; set; }
    }
}
