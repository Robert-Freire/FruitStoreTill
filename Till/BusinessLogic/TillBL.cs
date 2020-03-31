using System.Linq;
using Till.Data;
using Till.Model;

namespace Till.BusinessLogic
{

    public class TillBL : ITillBL
    {
        private ITillContext TillContext;

        public Invoice CurrentInvoice { get; private set; }
        public TillBL(ITillContext tillContext)
        {
            this.TillContext = tillContext;
        }

        public void AddInvoice()
        {
            CurrentInvoice = new Invoice() { Id = this.TillContext.Invoices.Count + 1 };
            this.TillContext.Invoices.Add(CurrentInvoice);
        }
        public void AddLine(string fruitName, string quantity)
        {
            if (CurrentInvoice == null)
                AddInvoice();

            int pieces = 0;
            var fruit = TillContext.Fruits.FirstOrDefault(f => f.Name == fruitName);
            if (fruit == null)
                throw new TillBLException($"Fruit not found '{fruitName}'");
            if (!int.TryParse(quantity, out pieces))
                throw new TillBLException($"Invalid quantity '{quantity}'");

            var newLine = new InvoiceItem()
            {
                Id = CurrentInvoice.Items.Count + 1,
                InvoiceId = CurrentInvoice.Id,
                Fruit = fruit,
                Quantity = pieces
            };
            CurrentInvoice.Items.Add(newLine);

        }
    }
}