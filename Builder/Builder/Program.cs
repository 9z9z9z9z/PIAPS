using System;
using System.Collections.Generic;

namespace Builder
{
    // Interfaces
    interface IDriver
    {
        void Sit();
    }
    class Car
    {
        public bool isReady;
        public BusDriver _driver;
        public List<Passenger> _passengers;
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
        public Passenger(string kategory)
        {
            _kategory = kategory;
        }
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
    
    class Bus : Car
    {
    }
    class Taxi : Car
    {
        public bool _childChair;
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
            if (_taxi._passengers.Count + count <= 4)
            {
                for (int i = 0; i < count; ++i)
                {
                    Console.WriteLine("Input category: o-old (not 0) or c-child");
                    string input = Console.ReadLine();
                    _taxi._passengers.Add(new Passenger(input));
                    if (input == "c")
                    {
                        _taxi._childChair = true;
                    }
                }
            }
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
            if (_bus._passengers.Count + count <= 30)
            {
                for (int i = 0; i < count; ++i)
                {
                    Console.WriteLine("Input category: o-old (not 0) or c-child or b- beneficiary");
                    string input = Console.ReadLine();
                    _bus._passengers.Add(new Passenger(input));
                }
            }
        }
    }
    
    
    class Station
    {
        private IBuilder aBuilder;
        private Car choosedCar;
        
        public Station(IBuilder example)
        {
            aBuilder = example;
        }
        public bool CheckCar()
        {
            return choosedCar.isReady;
        }
        public void CallCar()
        {
            aBuilder.InitProduct();
            
        } 
        public void SetDriver()
        {
            aBuilder.SetDriver();
        }
        public void SetPassengers(int count)
        {
            aBuilder.SetPassengers(count);
        }
    }
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            Station director;
            int solution = 0;
            while (solution != 4)
            {
                Console.WriteLine("\t\t===== Menu =====" +
                                  "1) Call a bus " +
                                  "2) Call a taxi" +
                                  "3) Check car" +
                                  "4) Exit program");
                switch (solution)
                {
                    case 1:
                        director = new Station(new BusBuilder());
                        break;
                    case 2:
                        director = new Station(new TaxiBuilder());
                        break;
                    case 3:
                        director.CheckCar();
                        break;
                }
            }
                        
        }
    }
}