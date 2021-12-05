namespace DealerOn.ExerciseOne.Entities;

public class ExplorationGrid
{
    public ExplorationGrid(int maxX, int maxY)
    {
        MaxX = maxX;
        MaxY = maxY;
    }

    public int MaxY { get; private set; }
    public int MaxX { get; private set; }
}
