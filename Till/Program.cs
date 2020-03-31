using Till.APIInterface;
using Till.BusinessLogic;
using Till.Data;

namespace Till
{
    class Program
    {
        static void Main(string[] args)
        {
            var tillContext = new TillContext();
            var tillBL = new TillBL(tillContext);

            var consoleInterface = new ConsoleInterface(tillContext, tillBL);

            consoleInterface.Run();
        }
    }
}
