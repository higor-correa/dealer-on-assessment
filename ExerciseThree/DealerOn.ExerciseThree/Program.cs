using DealerOn.ExerciseThree.Services;

var routeService = new RouteService();

var line = Console.ReadLine();
if (!string.IsNullOrWhiteSpace(line))
{
    routeService.SeedDistances(line);
}

Console.WriteLine($"Output #1: {WriteValues(routeService.GetRouteDistance("A-B-C"))}");
Console.WriteLine($"Output #2: {WriteValues(routeService.GetRouteDistance("A-D"))}");
Console.WriteLine($"Output #3: {WriteValues(routeService.GetRouteDistance("A-D-C"))}");
Console.WriteLine($"Output #4: {WriteValues(routeService.GetRouteDistance("A-E-B-C-D"))}");
Console.WriteLine($"Output #5: {WriteValues(routeService.GetRouteDistance("A-E-D"))}");
Console.WriteLine($"Output #6: {WriteValues(routeService.GetNumberOfRoutes('C', 'C', 3))}");
Console.WriteLine($"Output #7: {WriteValues(routeService.GetNumberOfRoutesWithExactStops('A', 'C', 4))}");
Console.WriteLine($"Output #8: {WriteValues(routeService.GetShortestDistance('A', 'C'))}");
Console.WriteLine($"Output #9: {WriteValues(routeService.GetShortestDistance('B', 'B'))}");
Console.WriteLine($"Output #10: {WriteValues(routeService.GetAmountOfPossibleRoutes('C', 'C', 30))}");

static string WriteValues(int? value)
{
    return value.HasValue ? value.Value.ToString() : "NO SUCH ROUTE";
}