using System.Collections.Generic;
using System.Linq;

namespace Till.Model
{
    public class Invoice
    {
        public int Id { get; set; }
        public ICollection<InvoiceItem> Items { get; set; }

        public decimal Total { get => Items.Sum(i => i.InvoiceRowAmount); }
        public Invoice()
        {
            Items = new List<InvoiceItem>();
        }
    }
}