namespace Domain;

public sealed class Game
{
    public Snake Snake { get; private set; }
    public Rabbit Rabbit { get; private set; }

    public bool IsOver { get; private set; }

    public bool IsWin { get; private set; }

    private Game(Snake snake, Rabbit rabbit)
    {
        Snake = snake;
        Rabbit = rabbit;
    }

    public static async Task StartGame(int width, int height, int speed, Func<Game, ControllerState> onGameCycleUpdate, CancellationToken cancellationToken)
    {
        var snakePosition = Position.CreateRandom(width - 1, height - 1);
        var snakeDirection = DirectionHelper.RandomDirection();
        var snake = Snake.Create(snakePosition, snakeDirection);
        
        var rabbitPosition = Position.CreateRandom(width - 1, height - 1);
        var rabbit = Rabbit.CreateAt(rabbitPosition);

        while (snake.IsOverlaps(rabbit.Position))
        {
            rabbit = Rabbit.CreateAt(Position.CreateRandom(width - 1, height - 1));
        }

        var game = new Game(snake, rabbit);
        var endLoop = false;

        while (true)
        {
            var controllerState = onGameCycleUpdate(game);

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

            if (snake.IsOverlaps(game.Rabbit.Position))
            {
                while (snake.IsOverlaps(game.Rabbit.Position))
                {
                    game.Rabbit = Rabbit.CreateAt(Position.CreateRandom(width - 1, height - 1));
                }

                snake.IncrementBody();
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