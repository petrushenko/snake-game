namespace Domain;

public record struct Position
{
    private static readonly Random Random = new();

    public int X { get; private set; } 

    public int Y { get; private set; }

    private Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Position Create(int x, int y) => new(x, y);

    public static Position CopyShifted(Position original, Direction shiftDirection) 
        => original.Shift(shiftDirection);

    public Position Shift(Direction direction)
    {
        var xShift = 0;
        var yShift = 0;
        switch (direction)
        {
            case Direction.Left:
                xShift = -1;
                break;
            case Direction.Right:
                xShift = 1;
                break;
            case Direction.Up:
                yShift = -1;
                break;
            case Direction.Down:
                yShift = 1;
                break;
        }

        X += xShift;
        Y += yShift;

        return this;
    }

    public Position SetFrom(Position position)
    {
        X = position.X;
        Y = position.Y;

        return this;
    }

    public static Position CreateRandom(int xLimit, int yLimit)
    {
        var x = Random.Next(1, xLimit);
        var y = Random.Next(1, yLimit);

        return Create(x, y);
    }
}

