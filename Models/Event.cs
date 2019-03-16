using System;
using System.Collections.Generic;

namespace DotNetCoreSqlDb.Models
{
    public class Event
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Choice> Choices { get; set; }
    }
}
