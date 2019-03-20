using System;
namespace DotNetCoreSqlDb.Models
{
    public class Choice
    {
        public int ID { get; set; }
        public string Suggester { get; set; }
        public int RestaurantID { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public int EventID { get; set; }
        public virtual Event Event { get; set; }
    }
}
