using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Device.Location;
using Yelp.Api;

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
            while (iterator.MoveNext()) {
                var business = iterator.Current;
                Console.WriteLine(business.ImageUrl);
                Console.WriteLine(business.Name);
                Console.WriteLine(business.Phone);
                Console.WriteLine(business.Url);
                Console.WriteLine(business.Rating);
                Console.WriteLine(business.Coordinates.Latitude);
                Console.WriteLine(business.Coordinates.Longitude);
                Console.WriteLine(business.Location.DisplayAddress);
                Console.WriteLine("======(\"======\")(\"======\")(\"======\")(\"======\")");

            }
        }


    }
}
