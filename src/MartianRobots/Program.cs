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
            string? firstLine = ReadNonEmptyLine(reader);
            if (firstLine is null) return;

            RunProgram(reader, firstLine);
        }

        private static void RunProgram(TextReader reader, string first)
        {
            //set up world
            var (maxX, maxY) = ParseGrid(first);
            var world = new World(maxX, maxY);

            while (true)
            {   
                //check line from txt file
                string? positionLine = ReadNonEmptyLine(reader);
                if (positionLine == null) break;

                //parse the valid line from txt file
                var (x, y, o) = ParsePosition(positionLine);

                string? instructionLine = ReadNonEmptyLine(reader);
                if (instructionLine == null) break;

                var robot = new Robot(x, y, o);

                //execute instructions from line
                ExecuteInstructions(instructionLine, robot, world);

                Console.WriteLine(robot);
            }

            static void ExecuteInstructions(string instructions, Robot robot, World world)
            {
                foreach (char c in instructions.Trim())
                {
                    if (!CommandRegistry.TryGet(c, out var command))
                        continue; // Ignore unknown commands

                    command.Execute(robot, world);

                    if (robot.IsLost)
                        break; // Stop if robot is lost
                }
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