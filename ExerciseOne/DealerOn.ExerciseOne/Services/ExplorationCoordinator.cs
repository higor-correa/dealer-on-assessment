using DealerOn.ExerciseOne.Entities;

namespace DealerOn.ExerciseOne.Services;

public class ExplorationCoordinator
{
    private readonly IDictionary<char, Instruction> _instructionsTranslation = new Dictionary<char, Instruction>
    {
        { 'M', Instruction.Move },
        { 'L', Instruction.Left },
        { 'R', Instruction.Right },
    };

    private readonly IList<Rover> _rovers;
    private readonly IDictionary<Rover, string> _roverInstructions;
    private readonly ExplorationGrid _explorationGrid;

    public IReadOnlyList<Rover> Rovers => _rovers.ToList().AsReadOnly();

    public ExplorationCoordinator(ExplorationGrid explorationGrid)
    {
        _rovers = new List<Rover>();
        _roverInstructions = new Dictionary<Rover, string>();
        _explorationGrid = explorationGrid;
    }

    public void AddRover(Rover rover, string instructions)
    {
        if (_rovers.All(x => x.PosX != rover.PosX && x.PosY != rover.PosY))
        {
            _rovers.Add(rover);

            var validInstructions = instructions.ToUpper().Where(x => _instructionsTranslation.ContainsKey(x));
            _roverInstructions.Add(rover, string.Join(string.Empty, validInstructions));
        }
    }

    public void Coordinate()
    {
        foreach (var rover in _rovers)
        {
            foreach (var instruction in _roverInstructions[rover])
            {
                var roverInstruction = _instructionsTranslation[instruction];
                if (CanExecuteInstruction(rover, roverInstruction))
                {
                    rover.ReceiveInstruction(roverInstruction);
                }
            }
        }
    }

    private bool CanExecuteInstruction(Rover rover, Instruction roverInstruction)
    {
        if (roverInstruction == Instruction.Move)
        {
            var (PosX, PosY) = rover.PredictMove();
            var roverCrashes = _rovers.Except(Enumerable.Repeat(rover, 1))
                                      .Any(x => x.PosY == PosY && x.PosX == PosX);

            var exceedExplorationGrid = PosX > _explorationGrid.MaxX || PosY > _explorationGrid.MaxY || PosX < 0 || PosY < 0;

            return !roverCrashes && !exceedExplorationGrid;
        }

        return true;
    }
}
