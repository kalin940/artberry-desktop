using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtberryApp.Models
{
    public class SubscriptionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SessionsNumber { get; set; }
        public double Price { get; set; }
        public int MonthsDuration { get; set; }
        public string Description { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTime Timestamp { get; set; }
        public string ETag { get; set; }
    }
}
