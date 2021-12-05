using DealerOn.ExerciseThree.Entities;

namespace DealerOn.ExerciseThree.Services;

public class RouteService
{
    private readonly IDictionary<char, City> citiesTranslator;

    public RouteService()
    {
        citiesTranslator = new Dictionary<char, City>();
    }

    public void SeedDistances(string allRoutesDistances)
    {
        var routes = allRoutesDistances.ToUpper()
                                       .Split(",")
                                       .Select(x => x.Trim());

        foreach (var route in routes)
        {
            if (!citiesTranslator.ContainsKey(route[0]))
            {
                citiesTranslator.Add(route[0], new City(route[0].ToString()));
            }

            if (!citiesTranslator.ContainsKey(route[1]))
            {
                citiesTranslator.Add(route[1], new City(route[1].ToString()));
            }

            var cityOrigin = citiesTranslator[route[0]];
            var cityDestination = citiesTranslator[route[1]];

            var routeDistance = int.Parse(route[2].ToString());

            cityOrigin.AddDestination(cityDestination, routeDistance);
        }
    }

    public int GetAmountOfPossibleRoutes(char originCity, char destinationCity, int maxDistance)
    {
        var origin = citiesTranslator[originCity];
        var finalDestination = citiesTranslator[destinationCity];

        var trips = origin.Reach(finalDestination, 0, int.MaxValue, new Trip(0, false, new List<City>()));
        trips.RemoveAll(x => !x.ReachedFinalDestination || x.TotalDistance >= maxDistance);

        return trips.Count;
    }

    public int? GetShortestDistance(char originCity, char destinationCity)
    {
        var origin = citiesTranslator[originCity];
        var finalDestination = citiesTranslator[destinationCity];

        var trips = origin.Reach(finalDestination, 0, int.MaxValue, new Trip(0, false, new List<City>()));
        trips.RemoveAll(x => !x.ReachedFinalDestination);
        var shortestTrip = trips.OrderBy(x => x.TotalDistance).First();

        return shortestTrip.TotalDistance;
    }

    public int GetNumberOfRoutesWithExactStops(char originCity, char destinationCity, int stops)
    {
        var origin = citiesTranslator[originCity];
        var finalDestination = citiesTranslator[destinationCity];

        var trips = origin.Reach(finalDestination, 0, int.MaxValue, new Trip(0, false, new List<City>()));
        trips.RemoveAll(x => !x.ReachedFinalDestination || x.Stops != stops);

        return trips.Count;
    }

    public int GetNumberOfRoutes(char originCity, char destinationCity, int maxStops)
    {

        var origin = citiesTranslator[originCity];
        var finalDestination = citiesTranslator[destinationCity];

        var trips = origin.Reach(finalDestination, 0, maxStops, new Trip(0, false, new List<City>()));
        trips.RemoveAll(x => !x.ReachedFinalDestination || x.Stops > maxStops);

        return trips.Count;
    }

    public int? GetRouteDistance(string route)
    {
        var cities = route.ToUpper().Split("-").Select(x => x.First()).ToArray();
        var totalDistance = 0;

        for (var i = 0; i < cities.Length - 1; i++)
        {
            if (!citiesTranslator.ContainsKey(cities[i]) || !citiesTranslator.ContainsKey(cities[i + 1]))
            {
                return null;
            }

            var origin = citiesTranslator[cities[i]];
            var destination = citiesTranslator[cities[i + 1]];

            var distance = origin.DistanceTo(destination);
            if (!distance.HasValue)
            {
                return null;
            }

            totalDistance += distance.Value;
        }

        return totalDistance;
    }


}
