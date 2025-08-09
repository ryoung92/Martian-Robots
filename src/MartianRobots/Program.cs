// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;

namespace MartianRobots
{


    public static class Program
    {
        public static void Main()
        {
            using var reader = Console.In;
            string? first = ReadNonEmptyLine(reader);
            if (first == null) return;

            var (maxX, maxY) = ParseGrid(first);
            var world = new World(maxX, maxY);

            while (true)
            {
                string? posLine = ReadNonEmptyLine(reader);
                if (posLine == null) break;

                var (x, y, o) = ParsePosition(posLine);

                string? instrLine = ReadNonEmptyLine(reader);
                if (instrLine == null) break;

                var robot = new Robot(x, y, o);

                foreach (char c in instrLine.Trim())
                {
                    if (!CommandRegistry.TryGet(c, out var cmd))
                        continue; // Unknown commands are ignored (future-proofing)

                    cmd.Execute(robot, world);

                    if (robot.IsLost) break; // Stop processing this robot’s instructions
                }

                Console.WriteLine(robot);
            }
        }

        static string? ReadNonEmptyLine(TextReader reader)
        {
            while (true)
            {
                var line = reader.ReadLine();
                if (line == null) return null;
                line = line.Trim();
                if (line.Length > 0) return line;
            }
        }

        static (int x, int y) ParseGrid(string line)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2) throw new InvalidDataException("Invalid grid line.");
            return (int.Parse(parts[0]), int.Parse(parts[1]));
        }

        static (int x, int y, Orientation o) ParsePosition(string line)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3) throw new InvalidDataException("Invalid position line.");
            var o = parts[2] switch
            {
                "N" => Orientation.N,
                "E" => Orientation.E,
                "S" => Orientation.S,
                "W" => Orientation.W,
                _ => throw new InvalidDataException("Invalid orientation.")
            };
            return (int.Parse(parts[0]), int.Parse(parts[1]), o);
        }
    }
}