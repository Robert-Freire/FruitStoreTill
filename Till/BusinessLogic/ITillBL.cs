using Till.Model;

namespace Till.BusinessLogic
{
    public interface ITillBL
    {
        Invoice CurrentInvoice { get; }
        void AddInvoice();
        void AddLine(string fruitName, string quantity);
    }
}