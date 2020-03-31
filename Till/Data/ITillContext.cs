using System.Collections.Generic;
using Till.Model;

namespace Till.Data
{
    public interface ITillContext
    {
        ICollection<Fruit> Fruits { get; set; }
        ICollection<Invoice> Invoices { get; set; }
    }
}