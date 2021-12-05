namespace DealerOn.ExerciseThree.Entities;

public class Trip
{
    private readonly List<City> _route;

    public Trip(int totalDistance, bool reachedFinalDestination, List<City> route)
    {
        ReachedFinalDestination = reachedFinalDestination;
        TotalDistance = totalDistance;
        _route = route;
    }

    public bool ReachedFinalDestination { get; set; }
    public IReadOnlyList<City> Route => _route.ToList().AsReadOnly();
    public int Stops => _route.Count - 1;
    public int TotalDistance { get; set; }

    public void AddCity(City city)
    {
        _route.Add(city);
    }

    public override string ToString()
    {
        return string.Join("-", _route.Select(x => x.Name));
    }
}
