using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services
{
    public interface IRideServices
    {
        bool IsValidRideId(int rideId);

        decimal GetMaximumCharge(int carType);

        int GetDistanceBetweenPlaces(string source, string destination, List<string> viaPoints);

        int GetDistanceBetweenPlaces(string source, string destination);

        int GetDurationBetweenPlaces(string source, string destination);

    }
}
