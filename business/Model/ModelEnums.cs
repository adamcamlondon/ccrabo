using System;

namespace CongestionCharge.Business.Model
{
    [Flags]
    public enum Day
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 4,
        Thursday = 8,
        Friday = 16,
        Saturday = 32,
        Sunday = 64,
        Weekday = Monday | Tuesday | Wednesday | Thursday | Friday,
        Weekend = Saturday | Sunday
    }

    [Flags]
    public enum VehicleType
    {
        Car = 1,
        Van = 2,
        Truck = 4,
        Motorcycle = 8,
        FullRateVehicle = Car | Van | Truck,
        DicountRateVehicle = Motorcycle
    }
}
