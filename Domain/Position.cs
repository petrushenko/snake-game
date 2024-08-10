namespace Domain;

public record struct Position
{
    private static Random _random = new();

    public int X { get; private set; } 

    public int Y { get; private set; }

    private Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Position Create(int x, int y) => new(x, y);

    public static Position CopyShifted(Position original, int xAxis = 0, int yAxis = 0) 
        => new(original.X + xAxis, original.Y + yAxis);

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
        var x = _random.Next(1, xLimit);
        var y = _random.Next(1, yLimit);

        return Create(x, y);
    }
}

