using System.Collections.Generic;
using Till.Model;

namespace Till.Data
{
    public class TillContext : ITillContext
    {
        public ICollection<Fruit> Fruits { get; set; }
        public ICollection<Invoice> Invoices { get; set; }

        public TillContext()
        {
            Fruits = new List<Fruit>();
            Invoices = new List<Invoice>();
        }
    }
}