namespace Till.Model
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public Fruit Fruit { get; set; }
        public int Quantity { get; set; }
        public decimal InvoiceRowAmount { get => Quantity * Fruit.Price; }
    }
}