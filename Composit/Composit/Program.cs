using System;
using System.Collections.Generic;

// Примитивный объект - Пассажир
class Passenger
{
    public string Name { get; set; }
    public double BaggageWeight { get; set; }
}

// Примитивный объект - Пилот
class Pilot
{
    public string Name { get; set; }
}

// Примитивный объект - Стюардесса
class FlightAttendant
{
    public string Name { get; set; }
}

// Композит - класс для хранения пассажиров
class PassengerGroup
{
    private List<object> passengers = new List<object>();

    public void Add(object passenger)
    {
        passengers.Add(passenger);
    }

    public void Remove(object passenger)
    {
        passengers.Remove(passenger);
    }

    public double GetTotalBaggageWeight()
    {
        double totalWeight = 0;
        foreach (var passenger in passengers)
        {
            if (passenger is Passenger)
            {
                totalWeight += ((Passenger)passenger).BaggageWeight;
            }
        }
        return totalWeight;
    }

    public object GetPassenger(int index)
    {
        return passengers[index];
    }
    public int GetPassengerCount()
    {
        return passengers.Count;
    }
}

// Клиентский код
class Program
{
    static void Main(string[] args)
    {
        // Создаем пассажиров, пилотов и стюардесс
        Passenger passenger1 = new Passenger { Name = "John", BaggageWeight = 25 };
        Passenger passenger2 = new Passenger { Name = "Alice", BaggageWeight = 30 };
        Pilot pilot1 = new Pilot { Name = "Captain Smith" };
        FlightAttendant fa1 = new FlightAttendant { Name = "Emma" };
        // Создаем группы пассажиров для каждого класса
        PassengerGroup firstClassPassengers = new PassengerGroup();
        PassengerGroup businessClassPassengers = new PassengerGroup();
        PassengerGroup economyClassPassengers = new PassengerGroup();
        // Добавляем пассажиров в соответствующие группы
        firstClassPassengers.Add(passenger1);
        firstClassPassengers.Add(passenger2);
        businessClassPassengers.Add(new Passenger { Name = "Bob", BaggageWeight = 15 });
        economyClassPassengers.Add(new Passenger { Name = "Charlie", BaggageWeight = 140 });
        // Создаем самолет
        Aircraft aircraft = new Aircraft();
        // Добавляем пилотов и стюардесс
        aircraft.AddCrewMember(pilot1);
        aircraft.AddCrewMember(fa1);
        // Добавляем группы пассажиров
        aircraft.AddPassengerGroup(firstClassPassengers);
        aircraft.AddPassengerGroup(businessClassPassengers);
        aircraft.AddPassengerGroup(economyClassPassengers);
        // Выводим информацию о загрузке самолета
        aircraft.PrintLoadStatus();
    }
}

// Композит - Самолет
class Aircraft
{
    private List<object> crewMembers = new List<object>();
    private List<PassengerGroup> passengerGroups = new List<PassengerGroup>();
    private const double maxBaggageWeight = 100; // Максимальная допустимая загрузка багажом

    public void AddCrewMember(object crewMember)
    {
        crewMembers.Add(crewMember);
    }

    public void RemoveCrewMember(object crewMember)
    {
        crewMembers.Remove(crewMember);
    }

    public void AddPassengerGroup(PassengerGroup group)
    {
        passengerGroups.Add(group);
    }

    public void RemovePassengerGroup(PassengerGroup group)
    {
        passengerGroups.Remove(group);
    }

    public void PrintLoadStatus()
    {
        Console.WriteLine("Load Status:");
        Console.WriteLine("Crew Members:");
        foreach (var crewMember in crewMembers)
        {
            Console.WriteLine("- " + crewMember.GetType().Name);
        }
        Console.WriteLine("Passenger Groups:");
        foreach (var group in passengerGroups)
        {
            Console.WriteLine("- " + group.GetPassengerCount() + " passengers");
            Console.WriteLine("  Total baggage weight: " + group.GetTotalBaggageWeight() + " kg");
            if (group.GetTotalBaggageWeight() > maxBaggageWeight &&
                group == passengerGroups[2]) // Проверяем только эконом-класс
            {
                Console.WriteLine("  Baggage overweight, removing excess baggage...");
                double excessWeight = group.GetTotalBaggageWeight() - maxBaggageWeight;
                for (int i = 0; i < group.GetPassengerCount(); i++)
                {
                    var passenger = group.GetPassenger(i);
                    if (passenger is Passenger && ((Passenger)passenger).BaggageWeight > 0)
                    {
                        double baggageWeight = ((Passenger)passenger).BaggageWeight;
                        if (baggageWeight > excessWeight)
                        {
                            ((Passenger)passenger).BaggageWeight -= excessWeight;
                            excessWeight = 0;
                        }
                        else
                        {
                            excessWeight -= baggageWeight;
                            ((Passenger)passenger).BaggageWeight = 0;
                        }
                    }
                    
                    if (excessWeight == 0)
                        break;
                }

                Console.WriteLine("  Excess baggage removed successfully.");
                Console.WriteLine("  Total baggage weight after removal: " + group.GetTotalBaggageWeight() + " kg");
            }
        }
    }
}
