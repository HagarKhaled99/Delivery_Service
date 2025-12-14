using System;

namespace DeliveryServiceAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1) Create Objects
            DeliveryTruck truck1 = new DeliveryTruck("Standard Truck", 200);
            RefrigeratedTruck truck2 = new RefrigeratedTruck("Cold Truck", 150);
            LuxuryCourierVan truck3 = new LuxuryCourierVan("Luxury Van", 100, premiumInterior: true);

            Console.WriteLine("Engine Test (Polymorphism)");
            truck1.StartEngine();
            truck2.StartEngine();
            truck3.StartEngine();

            Console.WriteLine();
            Console.WriteLine("Safety Test (Validation)");
            truck1.LoadCargo(9999);
            truck1.UnloadCargo(9999);

            Console.WriteLine();
            Console.WriteLine("Extra Feature Test (Luxury Van) ");
            truck3.ActivateClimateControl();

            Console.WriteLine();
            Console.WriteLine(" Fleet Test (Interface Array)");
            IDeliverable[] myFleet = { truck1, truck2, truck3 };

            foreach (IDeliverable vehicle in myFleet)
            {
                vehicle.LoadCargo(50);
                Console.WriteLine($"RequiresSpecialDocking? {vehicle.RequiresSpecialDocking}");
            }

            Console.WriteLine("Done");
        }
    }
}
