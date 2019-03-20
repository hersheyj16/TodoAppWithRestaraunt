using System;
namespace DotNetCoreSqlDb.Models
{
    public class EventChoice
    {
        public Choice choice { get; set; }
        public Restaurant restaurant { get; set; }

        public EventChoice(Choice choice, Restaurant restaurant)
        {
            this.choice = choice;
            this.restaurant = restaurant;
        }
    }
}
