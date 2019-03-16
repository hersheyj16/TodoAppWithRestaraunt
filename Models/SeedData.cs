using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Device.Location;
using Yelp.Api;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreSqlDb.Models
{
    public class SeedData
    {
        public static async void InitializeAsync(IServiceProvider serviceProvider)
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

                    var client = new Yelp.Api.Client(YELP_API);
                    List<Restaurant> results = new List<Restaurant>();
                    await GetResultsFromYelpAsync(client, results);

                    context.Restaurants.AddRange(results);
                    context.SaveChanges();
                }
            }
        }

        //GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
        public static async Task GetResultsFromYelpAsync(Client client, List<Restaurant> data)
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

                Restaurant r = new Restaurant();
                r.Name = Name;
                r.ImageUrl = ImageUrl;
                r.Phone = Phone;
                r.Url = Url;
                r.Rating = Rating;
                r.Latitude = Latitude;
                r.Longitude = Longitude;

                data.Add(r);
            }
        }
    }
}
