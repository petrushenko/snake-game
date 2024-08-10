namespace ConsoleView;

internal static class ConsoleDrawer
{
    public static void Write(this string text, int x, int y, ConsoleColor foreground, ConsoleColor background)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = foreground;
        Console.BackgroundColor = background;
        Console.Write(text);
        Console.ResetColor();
    }

    public static void Write(char pixel, int x, int y, ConsoleColor foreground, ConsoleColor background)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = foreground;
        Console.BackgroundColor = background;
        Console.Write(pixel);
        Console.ResetColor();
    }
}

