using System;

namespace DeliveryServiceAssignment
{
    // Rulebook Interface
    public interface IDeliverable
    {
        bool RequiresSpecialDocking { get; }
        void LoadCargo(int weight);
        void UnloadCargo(int weight);
    }

    // Base Vehicle using Encapsulation and Polymorphism
    public class DeliveryTruck : IDeliverable
    {
        private string _truckName;
        private int _currentLoadWeight;
        private int _maxLoadCapacity;

        // Constructor
        public DeliveryTruck(string name, int capacity)
        {
            _truckName = name;
            _maxLoadCapacity = capacity;
            _currentLoadWeight = 0;
        }

        // Read-only truck name
        public string TruckName
        {
            get { return _truckName; }
        }

        // Encapsulated load with validation
        public int CurrentLoadWeight
        {
            get { return _currentLoadWeight; }
            private set
            {
                if (value < 0)
                {
                    _currentLoadWeight = 0;
                    Console.WriteLine($"[Warning] {TruckName}: Load cannot be negative. Setting load to 0.");
                }
                else if (value > _maxLoadCapacity)
                {
                    _currentLoadWeight = _maxLoadCapacity;
                    Console.WriteLine($"[Warning] {TruckName}: Load exceeded capacity ({_maxLoadCapacity}). Setting load to max capacity.");
                }
                else
                {
                    _currentLoadWeight = value;
                }
            }
        }

        // Polymorphism (can be overridden)
        public virtual void StartEngine()
        {
            Console.WriteLine($"{TruckName}: Engine started.");
        }

        // Interface Property (can be overridden)
        public virtual bool RequiresSpecialDocking
        {
            get { return false; } // standard trucks
        }

        // Interface Methods
        public void LoadCargo(int weight)
        {
            if (weight <= 0)
            {
                Console.WriteLine($"[Warning] {TruckName}: Load weight must be positive.");
                return;
            }

            // Use the property so validation happens
            CurrentLoadWeight = CurrentLoadWeight + weight;
            Console.WriteLine($"{TruckName}: Loaded {weight} kg. Current load = {CurrentLoadWeight} kg.");
        }

        public void UnloadCargo(int weight)
        {
            if (weight <= 0)
            {
                Console.WriteLine($"[Warning] {TruckName}: Unload weight must be positive.");
                return;
            }

            // Use the property so validation happens
            CurrentLoadWeight = CurrentLoadWeight - weight;
            Console.WriteLine($"{TruckName}: Unloaded {weight} kg. Current load = {CurrentLoadWeight} kg.");
        }
    }

    // Refrigerated Truck (Inheritance)
    public class RefrigeratedTruck : DeliveryTruck
    {
        public RefrigeratedTruck(string name, int capacity) : base(name, capacity) { }

        public override void StartEngine()
        {
            Console.WriteLine($"{TruckName}: Engine started. Cooling system is now ON.");
        }
    }

    // Luxury Courier Van (Inheritance + extra feature + overrides)
    public class LuxuryCourierVan : DeliveryTruck
    {
        private bool _hasPremiumInterior;

        public LuxuryCourierVan(string name, int capacity, bool premiumInterior)
            : base(name, capacity)
        {
            _hasPremiumInterior = premiumInterior;
        }

        public void ActivateClimateControl()
        {
            Console.WriteLine(_hasPremiumInterior ? "Climate control on." : "Standard AC on.");
        }

        public override void StartEngine()
        {
            Console.WriteLine($"{TruckName}: Engine started smoothly and quietly.");
        }

        public override bool RequiresSpecialDocking
        {
            get { return true; } // luxury vans need safe parking
        }
    }
}
