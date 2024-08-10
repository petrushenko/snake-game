namespace Domain;

public sealed class Game
{
    private readonly int _width;
    private readonly int _height;

    public Snake Snake { get; private set; }
    public Rabbit Rabbit { get; private set; }

    public bool IsOver { get; private set; }

    public bool IsWin { get; private set; }

    private Game(int width, int height, Snake snake, Rabbit rabbit)
    {
        _width = width;
        _height = height;
        Snake = snake;
        Rabbit = rabbit;
    }

    public static async Task StartGame(int width, int height, int speed, Func<Game, ControllerState> onKeyPressed, CancellationToken cancellationToken)
    {
        var startX = width / 2;
        var startY = height / 2;

        var snakePosition = Position.Create(startX, startY);
        var snake = Snake.Create(snakePosition);
        var rabbitPosition = Position.CreateRandom(width - 1, height - 1);
        var rabbit = Rabbit.CreateAt(rabbitPosition);

        while (snake.Overlaps(rabbit.Position))
        {
            rabbit = Rabbit.CreateAt(Position.CreateRandom(width - 1, height - 1));
        }

        var game = new Game(width, height, snake, rabbit);
        var endLoop = false;

        while (true)
        {
            var controllerState = onKeyPressed(game);

            if (endLoop || controllerState.PressedKey == ControllerKey.Stop)
            {
                break;
            }

            if (snake.IsDead)
            {
                game.IsOver = true;
                endLoop = true;
            }

            if (snake.Score == width * height - 3)
            {
                game.IsWin = true;
                endLoop = true;
            }

            if (controllerState.PressedKey != ControllerKey.None)
            {
                var direction = DirectionHelper.MapFromControllerKey(controllerState.PressedKey);
                snake.Move(direction!.Value);
            }
            else
            {
                snake.Move();
            }

            if (snake.Overlaps(game.Rabbit.Position))
            {
                while (snake.Overlaps(game.Rabbit.Position))
                {
                    game.Rabbit = Rabbit.CreateAt(Position.CreateRandom(width - 1, height - 1));
                }

                snake.IncBody();
            }

            var currentPosition = snake.Head.Position;

            if (snake.Head.Position.X == 0)
            {
                snake.MoveTo(Position.Create(width-2, currentPosition.Y));
            }
            else if (snake.Head.Position.X == width - 1)
            {
                snake.MoveTo(Position.Create(1, currentPosition.Y));
            } 
            else if (snake.Head.Position.Y == 0)
            {
                snake.MoveTo(Position.Create(currentPosition.X, height - 2));
            } 
            else if (snake.Head.Position.Y == height - 1)
            {
                snake.MoveTo(Position.Create(currentPosition.X, 1));
            }

            await Task.Delay(TimeSpan.FromMilliseconds(speed), cancellationToken);
        }
    }
}