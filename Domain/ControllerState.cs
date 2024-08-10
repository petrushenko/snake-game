namespace Domain;

public enum ControllerKey
{
    None,
    Up,
    Down,
    Left,
    Right,
    Stop
}

public record struct ControllerState(ControllerKey PressedKey);