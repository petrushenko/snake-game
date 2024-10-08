﻿namespace Domain;

public static class DirectionHelper
{
    private static Random _random = new();
    
    public static Direction GetOppositeDirection(Direction direction) => direction switch
    {
        Direction.Left => Direction.Right,
        Direction.Right => Direction.Left,
        Direction.Up => Direction.Down,
        Direction.Down => Direction.Up,
        _ => direction
    };

    public static Direction? MapFromControllerKey(ControllerKey key) => key switch
    {
        ControllerKey.Up => Direction.Up,
        ControllerKey.Down => Direction.Down,
        ControllerKey.Left => Direction.Left,
        ControllerKey.Right => Direction.Right,
        _ => null
    };

    public static Direction RandomDirection() => (Direction)_random.Next(3);
}

