using System;
using System.Collections.Generic;
using System.Linq;


namespace AbstructFabric
{
    interface IDriver
    {
        void Sit();
    }
    
    interface IAnyCar
    {
        bool IsReady();
        void BoardDriver();
        void BoardPassenger(int count);
    }
    
    class Passenger
    {
        public Passenger()
        {
            Console.WriteLine("Passenger sit into the car");
        }
    }
    
    class BusDriver : IDriver
    {
        public void Sit()
        {
            Console.WriteLine("Bus driver sit into the bus");
        }
    }
    
    class TaxiDriver : IDriver
    {
        public void Sit()
        {
            Console.WriteLine("Taxi driver set into the taxi");
        }
    }
    
    class Taxi : IAnyCar
    {
        private TaxiDriver _driver;
        private readonly List<Passenger> _passengers = new List<Passenger>();
        public void BoardDriver()
        {
            _driver = new TaxiDriver();
            _driver.Sit();
        }
        public void BoardPassenger(int count)
        {
            if (_passengers.Count() + count <= 4)
            {
                for (int i = 0; i < count; ++i)
                {
                    _passengers.Add(new Passenger());
                }
            }
            else
            {
                Console.WriteLine("Cannot place {0} passengers", count);
            }
        }
        public bool IsReady()
        {
            if (_driver != null && _passengers.Count <= 4 && _passengers.Any())
            {
                Console.WriteLine("The taxi is ready to route");
                return true;
            }
            else
            {
                Console.WriteLine("This taxi isn't ready to route");
                return false;
            }
        }
    }

    class Bus : IAnyCar
    {
        private BusDriver _driver;
        private readonly List<Passenger> _passengers = new List<Passenger>();
        public void BoardDriver()
        {
            _driver = new BusDriver();
            _driver.Sit();
        }
        public void BoardPassenger(int count)
        {
            if ((_passengers.Count() + count) <= 30)
            {
                for (int i = 0; i < count; ++i)
                {
                    _passengers.Add(new Passenger());
                }
            }
            else
            {
                Console.WriteLine("Cannot place {0} passengers", count);
            }
        }
        public bool IsReady()
        {
            if (_driver != null && _passengers.Count <= 30 && _passengers.Any())
            {
                Console.WriteLine("The bus is ready to route");
                return true;
            }
            else
            {
                Console.WriteLine("This bus isn't ready to route");
                return false;
            }
        }
    }
    
    internal abstract class Program
    {
        public static void Main(string[] args)
        {
            Bus busTest = new Bus();
            Taxi taxiTest = new Taxi();
            IAnyCar tmp = busTest;
            tmp.BoardDriver();
            tmp.BoardPassenger(0);
            tmp.IsReady();
            
            Console.WriteLine();
            
            tmp = taxiTest;
            tmp.BoardDriver();
            tmp.BoardPassenger(4);
            tmp.IsReady();
        }
    }
}