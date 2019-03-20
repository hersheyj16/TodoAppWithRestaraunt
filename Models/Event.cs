using System;
using System.Collections.Generic;

namespace DotNetCoreSqlDb.Models
{
    public class Event
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public virtual List<Choice> Choices { get; set; }

    }
}
