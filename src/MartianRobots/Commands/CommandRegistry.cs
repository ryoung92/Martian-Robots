using System.Collections.Generic;

namespace MartianRobots
{
    static class CommandRegistry
    {
        private static readonly Dictionary<char, IRobotCommand> _map = new()
        {
            ['L'] = new LeftCommand(),
            ['R'] = new RightCommand(),
            ['F'] = new ForwardCommand()
            // Add new commands later: e.g. ['B'] = new BackwardCommand()
        };

        public static bool TryGet(char c, out IRobotCommand cmd) => _map.TryGetValue(c, out cmd);
    }
}