namespace Domain;

public sealed class Snake
{
    private readonly LinkedList<SnakePart> _body = [];

    public IEnumerable<SnakePart> Body => _body;
    
    public SnakePart Head => _body.First();

    public int Score => _body.Count - 3;

    public bool IsDead 
    { 
        get
        {
            var headPosition = Head.Position;

            return _body.Where(x => x.Type != SnakePartType.Head).Any(x => x.Position == headPosition);
        } 
    }

    private Snake(Position startPosition, Direction direction)
    {
        var head = SnakePart.CreateHead(startPosition, direction);
        _body.AddFirst(head);

        var bodyPosition = Position.CopyShifted(head.Position, DirectionHelper.GetOppositeDirection(direction));
        var body = SnakePart.CreateBody(bodyPosition, head.Direction);
        _body.AddLast(body);

        var tailPosition = Position.CopyShifted(body.Position, DirectionHelper.GetOppositeDirection(direction));
        var tail = SnakePart.CreateTail(tailPosition, body.Direction);
        _body.AddLast(tail);
    }

    public static Snake Create(Position position, Direction direction) => new(position, direction);

    public int IncrementBody()
    {
        var headNode = _body.First;
        var head = headNode!.Value;

        var body = SnakePart.CreateBody(head.Position, head.Direction);
        _body.AddAfter(headNode, body);

        head.Shift(head.Direction);

        return _body.Count;
    }

    public void Move(Direction direction)
    {
        var headNode = _body.First;
        var head = headNode!.Value;
        if (direction == DirectionHelper.GetOppositeDirection(head.Direction))
        {
            direction = head.Direction;
        }
        
        var lastDirection = head.Direction;
        var lastPosition = head.Position;
        head.Move(direction);

        foreach (var node in _body) 
        {
            if (node.Type == SnakePartType.Head)
                continue;

            var currentDirection = node.Direction;
            var currentPosition = node.Position;

            node.MoveTo(lastPosition, lastDirection);

            lastDirection = currentDirection;
            lastPosition = currentPosition;
        }
    }

    public void Move()
    {
        Move(Head.Direction);
    }

    public void MoveTo(Position position)
    {
        Head.MoveTo(position);
    }

    public bool IsOverlaps(Position position)
    {
        return _body.Any(x => x.Position == position);
    }
}