using ConsoleView;
using Domain;

Console.OutputEncoding = System.Text.Encoding.UTF8;

const int height = 20;
const int width = 40;

SetupField();
await Game.StartGame(width, height, 100, (game) =>
{
    var input = Input();

    DrawField(game);

    return new ControllerState(input);
}, CancellationToken.None);


Console.ReadKey();
return;

ControllerKey Input()
{
    if (!Console.KeyAvailable) { return ControllerKey.None; }

    var key = Console.ReadKey(false).Key;

    return key switch 
    { 
        ConsoleKey.UpArrow => ControllerKey.Up,
        ConsoleKey.DownArrow => ControllerKey.Down,
        ConsoleKey.LeftArrow => ControllerKey.Left,
        ConsoleKey.RightArrow => ControllerKey.Right,
        ConsoleKey.Escape => ControllerKey.Stop,
        _ => ControllerKey.None
    };
}

void DrawField(Game game)
{
    for (var i = 1; i < height - 1; i++)
    {
        new string('x', width - 2).Write(1, i, ConsoleColor.Blue, ConsoleColor.Blue);
    }

    foreach (var part in game.Snake.Body)
    {
        var symbol = MapSnakePartToSymbol(part);
        ConsoleDrawer.Write(symbol, part.Position.X, part.Position.Y, ConsoleColor.Yellow, ConsoleColor.Blue);
    }

    var rabbitPosition = game.Rabbit.Position;
    ConsoleDrawer.Write('*', rabbitPosition.X, rabbitPosition.Y, ConsoleColor.Red, ConsoleColor.Blue);

    

    if (game.IsOver)
    {
        $"Game over. Score: {game.Snake.Score}".Write(width + 3, 5, ConsoleColor.Blue, ConsoleColor.Black);
    }
    else if (game.IsWin)
    {
        $"WIN. Score: {game.Snake.Score}".Write(width + 3, 5, ConsoleColor.Blue, ConsoleColor.Black);
    }
    else
    {
        $"Score: {game.Snake.Score}".Write(width + 3, 5, ConsoleColor.Blue, ConsoleColor.Black);
    }
}

char MapSnakePartToSymbol(SnakePart part)
{
    if (part.Type == SnakePartType.Head)
    {
        return part.Direction switch
        {
            Direction.Up => '˄',
            Direction.Down => '˅',
            Direction.Left => '<',
            Direction.Right => '>',
            _ => throw new ArgumentOutOfRangeException(),
        };
    }

    if (part.Type == SnakePartType.Body)
    {
        return part.Direction switch
        {
            _ => 'O'
        };
    }

    if (part.Type == SnakePartType.Tail)
    {
        return part.Direction switch
        {
            _ => 'o'
        };
    }

    return 'X';
}

void SetupField()
{
    Console.Clear();
    Console.CursorVisible = false;
    Console.Title = "Snake Game";

    for (var i = 0; i < height; i++)
    {
        if (i == 0 || i == height - 1)
        {
            $"+{new string('-', width - 2)}+".Write(0, i, ConsoleColor.Green, ConsoleColor.Green);
        }
        else
        {
            $"|{new string(' ', width - 2)}|".Write(0, i, ConsoleColor.Green, ConsoleColor.Green);
        }
    }
}