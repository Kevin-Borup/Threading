
namespace ConsoleApp_BaggageHandling
{
    public class Program
    {
        public static bool applicationExecuting = true;
        public static void Main(string[] args)
        {
            FrontDesk desk1 = new FrontDesk(1, 100);
            FrontDesk desk2 = new FrontDesk(2, 100);
            FrontDesk desk3 = new FrontDesk(3, 100);

            BaggageLine<Baggage> sortingInventory = new BaggageLine<Baggage>(400);

            Terminal terminal1 = new Terminal(1, 45);
            Terminal terminal2 = new Terminal(2, 45);
            Terminal terminal3 = new Terminal(3, 45);

            BaggageSystem baggageSystem = new BaggageSystem(desk1, desk2, desk3);
            SortingSystem sortingSystem = new SortingSystem(desk1, desk2, desk3, sortingInventory, terminal1, terminal2, terminal3);
            PlaneSystem planeSystem = new PlaneSystem(terminal1, terminal2, terminal3);

            baggageSystem.SystemStarter();

            while (applicationExecuting)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    applicationExecuting = false;
                    baggageSystem.SystemStopper();
                }
            }
        }
    }

}