using System;
using System.Collections.Generic;

namespace MartianRobots
{
    internal sealed class World
    {
        public int MaxX { get; }
        public int MaxY { get; }

        // Scent is tied to (x, y, orientation) attempts off the grid.
        private readonly HashSet<(int x, int y, Orientation o)> _scents = new();

        public World(int maxX, int maxY)
        {
            if (maxX < 0 || maxY < 0) throw new ArgumentOutOfRangeException(nameof(maxX));
            MaxX = maxX;
            MaxY = maxY;
        }

        public bool IsInside(int x, int y) => x >= 0 && x <= MaxX && y >= 0 && y <= MaxY;

        public bool HasScent(int x, int y, Orientation o) => _scents.Contains((x, y, o));

        public void LeaveScent(int x, int y, Orientation o) => _scents.Add((x, y, o));
    }
}