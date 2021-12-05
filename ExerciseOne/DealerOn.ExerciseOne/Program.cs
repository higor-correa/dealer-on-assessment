using DealerOn.ExerciseOne.Entities;
using DealerOn.ExerciseOne.Services;

var headingTranslator = new Dictionary<string, CardinalPoints> {
    { "N", CardinalPoints.North },
    { "E", CardinalPoints.East },
    { "S", CardinalPoints.South },
    { "W", CardinalPoints.West }

};

var explorationGridBounds = Console.ReadLine();
var bounds = explorationGridBounds!.Trim().Split(' ');

var coordinator = new ExplorationCoordinator(new ExplorationGrid(int.Parse(bounds[0]), int.Parse(bounds[1])));

var line = Console.ReadLine();
while (!string.IsNullOrWhiteSpace(line))
{
    var roverPos = line.ToUpper().Trim().Split(' ');
    var roverX = int.Parse(roverPos[0]);
    var roverY = int.Parse(roverPos[1]);
    var roverHeading = headingTranslator[roverPos[2]];

    var instructions = Console.ReadLine()!;
    coordinator.AddRover(new Rover(roverX, roverY, roverHeading), instructions);

    line = Console.ReadLine();
}

coordinator.Coordinate();

foreach (var rover in coordinator.Rovers)
{
    Console.WriteLine($"{rover.PosX} {rover.PosY} {rover.Heading.ToString().First()}");
}