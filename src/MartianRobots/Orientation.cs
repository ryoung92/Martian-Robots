using System;

namespace MartianRobots
{
    enum Orientation { N, E, S, W }

    internal static class OrientationExtensions
    {
        public static Orientation TurnLeft(this Orientation o) => o switch
        {
            Orientation.N => Orientation.W,
            Orientation.W => Orientation.S,
            Orientation.S => Orientation.E,
            Orientation.E => Orientation.N,
            _ => o
        };

        public static Orientation TurnRight(this Orientation o) => o switch
        {
            Orientation.N => Orientation.E,
            Orientation.E => Orientation.S,
            Orientation.S => Orientation.W,
            Orientation.W => Orientation.N,
            _ => o
        };

        public static (int dx, int dy) ForwardDelta(this Orientation o) => o switch
        {
            Orientation.N => (0, 1),
            Orientation.E => (1, 0),
            Orientation.S => (0, -1),
            Orientation.W => (-1, 0),
            _ => (0, 0)
        };
    }
}