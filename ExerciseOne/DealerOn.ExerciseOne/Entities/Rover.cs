namespace DealerOn.ExerciseOne.Entities;

public class Rover
{
    private readonly Dictionary<CardinalPoints, CardinalPoints> RotationLeftHeadings = new Dictionary<CardinalPoints, CardinalPoints>
    {
        { CardinalPoints.North, CardinalPoints.West },
        { CardinalPoints.West, CardinalPoints.South },
        { CardinalPoints.South, CardinalPoints.East },
        { CardinalPoints.East, CardinalPoints.North }
    };

    private readonly Dictionary<CardinalPoints, CardinalPoints> RotationRightHeadings = new Dictionary<CardinalPoints, CardinalPoints>
    {
        { CardinalPoints.North, CardinalPoints.East },
        { CardinalPoints.East, CardinalPoints.South },
        { CardinalPoints.South, CardinalPoints.West },
        { CardinalPoints.West, CardinalPoints.North }
    };

    public Rover(int posX, int posY, CardinalPoints heading)
    {
        PosX = posX;
        PosY = posY;
        Heading = heading;
    }

    public int PosX { get; private set; }
    public int PosY { get; private set; }
    public CardinalPoints Heading { get; private set; }

    public void ReceiveInstruction(Instruction instruction)
    {
        if (instruction == Instruction.Move)
        {
            Move();
        }
        else
        {
            Rotate(instruction);
        }
    }

    private void Rotate(Instruction instruction)
    {
        var rotationHeading = instruction == Instruction.Left ? RotationLeftHeadings : RotationRightHeadings;
        Heading = rotationHeading[Heading];
    }

    private void Move()
    {
        switch (Heading)
        {
            case CardinalPoints.North:
                PosY++;
                break;
            case CardinalPoints.East:
                PosX++;
                break;
            case CardinalPoints.South:
                PosY--;
                break;
            case CardinalPoints.West:
                PosX--;
                break;
        }
    }

    public (int PosX, int PosY) PredictMove()
    {
        return Heading switch
        {
            CardinalPoints.North => (PosX, PosY + 1),
            CardinalPoints.East => (PosX + 1, PosY),
            CardinalPoints.South => (PosX, PosY - 1),
            CardinalPoints.West => (PosX - 1, PosY),
            _ => (PosX, PosY),
        };
    }
}
