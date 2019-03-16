using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Device.Location;
using Yelp.Api;
using System.Text;

namespace DotNetCoreSqlDb.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MyDatabaseContext(
            serviceProvider.GetRequiredService<DbContextOptions<MyDatabaseContext>>()))
            {

                if (context.Restaurants.Any())
                {
                    return;
                }
                else
                {
                    var YELP_API = Environment.GetEnvironmentVariable("YELP_API");
                    Console.WriteLine("JENNYLIA YELP API DEBUG");
                    Console.WriteLine(YELP_API);


                    //GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();

                    var client = new Yelp.Api.Client(YELP_API);
                    GetResultsFromYelpAsync(client, context);

                    //context.Restaurants.Add(new Restaurant
                    //{
                    //    Name = Name,
                    //    ImageUrl = ImageUrl,
                    //    Phone = Phone,
                    //    Url = Url,
                    //    Rating = Rating,
                    //    Latitude = Latitude,
                    //    Longitude = Longitude,
                    //    Address = address
                    //});
                    context.Restaurants.AddRange(new Restaurant
                    {
                        Name = "a",
                        ImageUrl = "b",
                        Phone = "c",
                        Url = "Url",
                        Rating = 1,
                        Latitude = 0.1,
                        Longitude = 0.2,
                        Address = "Url"
                    });
                    context.SaveChanges();
                }

            }
        }
        /*
         *     
         * public int ID { get; set; }
        public string ImageUrl { get; set; }
        public string Phone { get; set; }
        public string Url { get; set; }
        public float Rating { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
        public string Address { get; set; }       
         */

        private static async void GetResultsFromYelpAsync(Client client, MyDatabaseContext context)
        {
            var results = await client.SearchBusinessesAllAsync("food", 47.606209, -122.332069);

            var iterator = results.Businesses.GetEnumerator();
            while (iterator.MoveNext())
            {
                var business = iterator.Current;

                Console.WriteLine("adding a biz");
                var Name = business.Name;
                var ImageUrl = business.ImageUrl;
                var Phone = business.Phone;
                var Url = business.Url;
                var Rating = business.Rating;
                var Latitude = business.Coordinates.Latitude;
                var Longitude = business.Coordinates.Latitude;

                Console.WriteLine(Name);

                string[] disAddr = business.Location.DisplayAddress;
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < disAddr.Length; i++)
                {
                    sb.Append(disAddr[0]);
                }

                var address = sb.ToString();
                Console.WriteLine(address);
                Console.WriteLine("======(\"======\")(\"======\")(\"======\")(\"======\")");


            }
        }


    }
}
