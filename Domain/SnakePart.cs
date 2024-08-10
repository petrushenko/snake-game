namespace Domain;
public enum SnakePartType
{
    Body = 0,
    Head = 1,
    Tail = 2
}

public sealed record SnakePart
{
    public Position Position { get; private set; }

    public Direction Direction { get; private set; }

    public SnakePartType Type { get; private set; }

    private SnakePart(Position position, Direction direction, SnakePartType type)
    {
        Position = position;
        Direction = direction;
        Type = type;
    }

    private static SnakePart Create(Position position, Direction direction, SnakePartType type)
    {
        return new SnakePart(position, direction, type);
    }

    public static SnakePart CreateHead(Position position, Direction direction)
    {
        return new SnakePart(position, direction, SnakePartType.Head);
    }

    public static SnakePart CreateBody(Position position, Direction direction)
    {
        return new SnakePart(position, direction, SnakePartType.Body);
    }

    public static SnakePart CreateTail(Position position, Direction direction)
    {
        return new SnakePart(position, direction, SnakePartType.Tail);
    }

    public void Move(Direction direction)
    {
        Direction = direction;
        Position = Position.Shift(Direction);
    }

    public void MoveTo(Position position, Direction direction)
    {
        Direction = direction;
        Position = Position.SetFrom(position);
    }

    public void MoveTo(Position position)
    {
        Position = Position.SetFrom(position);
    }

    public void Shift(Direction direction)
    {
        Position = Position.Shift(Direction);
    }
}