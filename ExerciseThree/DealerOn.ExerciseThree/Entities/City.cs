using System.Collections.ObjectModel;

namespace DealerOn.ExerciseThree.Entities;

public class City
{
    private readonly IDictionary<City, int> _roads;

    public City(string name)
    {
        Name = name;
        _roads = new Dictionary<City, int>();
    }

    public string Name { get; set; }

    public IReadOnlyDictionary<City, int> Roads => new ReadOnlyDictionary<City, int>(_roads);

    public void AddDestination(City cityDestination, int routeDistance)
    {
        _roads.Add(cityDestination, routeDistance);
    }

    public int? DistanceTo(City destination)
    {
        return _roads.ContainsKey(destination) ? _roads[destination] : null;
    }

    public List<Trip> Reach(City finalDestination, int stopCount, int maxStops, Trip currentTrip)
    {
        return Reach(finalDestination, stopCount, maxStops, currentTrip, true);
    }

    private List<Trip> Reach(City finalDestination, int stopCount, int maxStops, Trip currentTrip, bool firstInteraction)
    {
        var trips = new List<Trip>
        {
            currentTrip
        };

        currentTrip.AddCity(this);
        currentTrip.ReachedFinalDestination = !firstInteraction && this == finalDestination;

        foreach (var road in _roads)
        {
            if (currentTrip.Route.Count(x => x == road.Key) < 5)
            {
                trips.AddRange(road.Key.Reach(finalDestination, stopCount + 1, maxStops, new Trip(currentTrip.TotalDistance + road.Value, false, currentTrip.Route.ToList()), false));
            }
        }
        return trips;
    }
    public override string ToString()
    {
        return Name;
    }
}
