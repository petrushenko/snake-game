namespace Domain;

public sealed class Rabbit
{
    public Position Position { get; }

    private Rabbit(Position position) 
    {
        Position = position;
    }

    public static Rabbit CreateAt(Position position) => new(position);
}
