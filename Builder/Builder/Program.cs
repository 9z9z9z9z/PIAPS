using System;

namespace Builder
{
    // Interfaces
    interface IDriver
    {
        void Sit();
    }
    interface ICar
    {
    }
    interface IBuilder
    {
        void InitProduct();
        void SetDriver();
        void SetPassengers(int count);

    }
    // Classes
    class Passenger
    {
        string _kategory;
    }

    class BusDriver : IDriver
    {
        public void Sit()
        {
            Console.WriteLine("Driver sit in the bus");
        }
    }

    class TaxiDriver : IDriver
    {
        public void Sit()
        {
            Console.WriteLine("Driver sit in the taxi");
        }
    }
    
    class Bus : ICar
    {
        public BusDriver _driver;
    }
    class Taxi : ICar
    {
        public TaxiDriver _driver;
    }
    

    class TaxiBuilder : IBuilder
    {
        protected Taxi _taxi;

        public void InitProduct()
        {
            _taxi = new Taxi();
        }
        public void SetDriver()
        {
            _taxi._driver = new TaxiDriver();
        }

        public void SetPassengers(int count)
        {
            if 
        }
        
    }
    class BusBuilder : IBuilder
    {
        protected Bus _bus;
        public void InitProduct()
        {
            _bus = new Bus();
        }
        public void SetDriver()
        {
            _bus._driver = new BusDriver();
        }

        public void SetPassengers(int count)
        {
            
        }
    }
    
    
    class Station
    {
        private IBuilder aBuilder;

        public Station(IBuilder example)
        {
            aBuilder = example;
        }
    }
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            Taxi taxiTest = new Taxi();
            Bus busTest = new Bus();
            Station director = new Station(new TaxiBuilder());
        }
    }
}