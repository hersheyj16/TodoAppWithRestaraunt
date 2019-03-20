using System;
namespace DotNetCoreSqlDb.Models
{
    public class Restaurant
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Phone { get; set; }
        public string Url { get; set; }
        public float Rating { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
    }
}
