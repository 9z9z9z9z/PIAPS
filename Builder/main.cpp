#include <iostream>
#include <string>
#include <vector>


class Driver {
    protected: std::string kategory;
};

class TaxiDriver : public Driver {
public:
    TaxiDriver() {
        this->kategory = "Taxi driver";
        std::cout << "Drivers sit in the taxi" << std::endl;
    }
};
class BusDriver : public Driver {
public:
    BusDriver() {
        this->kategory = "Bus driver";
        std::cout << "Driver sit in the bus" << std::endl;
    }
};

// Product
class Vehicle {
public:
    std::string type;
    std::vector<std::string> passengers;
    Driver driver;
    int capacity;
};

class Taxi : public Vehicle {
public:
    bool isChair = false;
    Taxi() {
        this->type = "Taxi";
        this->capacity = 4;
    }
};
class Bus : public Vehicle {
public:
    Bus() {
        this->type = "Bus";
        this->capacity = 30;
    }
};
// Builder interface
class VehicleBuilder {
public:
    virtual void initVechicle() = 0;
    virtual void setPassengers() = 0;
    virtual void setDriver() = 0;
    virtual void showInfo() = 0;
    virtual Vehicle getResult() = 0;
};


// Concrete Builder for Bus
class BusBuilder : public VehicleBuilder {
private:
    Bus bus;
public:
    void initVechicle() override {
        bus = Bus();
        std::cout << "Bus initialized" << std::endl;
    }
    void setDriver() override {
        bus.driver = BusDriver();
    }
    void setPassengers() override {
        std::cout << "How much people?" << std::endl;
        int count;
        std::cin >> count;
        while (count > 0 && bus.passengers.size() < bus.capacity) {
            std::cout << "child, adult or beneficiary?" << std::endl;
            std::string tmp;
            std::cin >> tmp;
            bus.passengers.push_back(tmp);
            --count;
        }
    }
    void showInfo() override {
        std::cout << R"(==========================)" << std::endl;
        std::cout << "Vehicle type: " << bus.type << std::endl;
        std::cout << "Passengers: " << bus.passengers.size() << std::endl;
        std::cout << "Capacity: " << bus.capacity << std::endl;
        if (!bus.passengers.empty() && bus.passengers.size() <= bus.capacity && &bus.driver != nullptr) {
            std::cout << "Bus is ready for drive" << std::endl;
        } else {
            std::cout << "Bus isn\'t ready for drive" << std::endl;
        }
        std::cout << R"(==========================)" << std::endl;
    }
    Vehicle getResult() override {
        return bus;
    }
};
// Concrete Builder for Taxi
class TaxiBuilder : public VehicleBuilder {
private:
    Taxi taxi;
public:
    void initVechicle() override {
        taxi = Taxi();
        std::cout << "Taxi initialized" << std::endl;
    }
    void setDriver() override {
        taxi.driver = TaxiDriver();
    }
    void setPassengers() override {
        std::cout << "How much people?" << std::endl;
        int count;
        std::cin >> count;
        while (count > 0 && taxi.passengers.size() < taxi.capacity) {
            std::cout << "child or adult?" << std::endl;
            std::string tmp;
            std::cin >> tmp;
            if (tmp == "child") {
                taxi.isChair = true;
            }
            taxi.passengers.push_back(tmp);
            --count;
        }
    }
    void showInfo() override {
        std::cout << R"(==========================)" <<std::endl;
        std::cout << "Vehicle type: " << taxi.type << std::endl;
        std::cout << "Passengers: " << taxi.passengers.size() << std::endl;
        std::cout << "Capacity: " << taxi.capacity << std::endl;
        if (taxi.isChair) {
            std::cout << "Child chair was installed" << std::endl;
        } else {
            std::cout << "Child chair is not necessary" << std::endl;
        }
        if (!taxi.passengers.empty() && taxi.passengers.size() <= taxi.capacity && &taxi.driver != nullptr) {
            std::cout << "Taxi is ready for drive" << std::endl;
        } else {
            std::cout << "Taxi isn\'t ready for drive" << std::endl;
        }
        std::cout << R"(==========================)" << std::endl;
    }
    Vehicle getResult() override {
        return taxi;
    }
};

// Director
class Dispatcher {
private:
    VehicleBuilder* builder;
public:
    void setBuilder(VehicleBuilder* newBuilder) {
        builder = newBuilder;
    }
    Vehicle buildVehicle() {
        builder->initVechicle();
        builder->setDriver();
        builder->setPassengers();
        builder->showInfo();
        return builder->getResult();
    }
};

int main() {
    Dispatcher dispatcher{};
    BusBuilder busBuilder;
    TaxiBuilder taxiBuilder;

    dispatcher.setBuilder(&busBuilder);
    Vehicle bus = dispatcher.buildVehicle();

    dispatcher.setBuilder(&taxiBuilder);
    Vehicle taxi = dispatcher.buildVehicle();

    return 0;
}
