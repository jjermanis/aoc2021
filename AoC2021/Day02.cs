using System;
using System.Collections.Generic;

namespace AoC2021
{
    public class Day02 : DayBase, IDay
    {
        private readonly IEnumerable<string[]> _commands;

        public Day02(string filename)
            => _commands = TextFileTokens(filename, ' ');

        public Day02() : this("Day02.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"Part1: {ProductSimpleCommands()}");
            Console.WriteLine($"Part2: {ProductCommandsWithAim()}");
        }

        public int ProductSimpleCommands()
        {
            var position = 0;
            var depth = 0;

            foreach (var command in _commands)
            {
                (var action, var value) = ParseCommand(command);

                switch (action)
                {
                    case "forward":
                        position += value;
                        break;

                    case "up":
                        depth -= value;
                        break;

                    case "down":
                        depth += value;
                        break;

                    default:
                        throw new Exception($"Unexpected action: {action}");
                }
            }
            return position * depth;
        }

        public int ProductCommandsWithAim()
        {
            var position = 0;
            var aim = 0;
            var depth = 0;

            foreach (var command in _commands)
            {
                (var action, var value) = ParseCommand(command);

                switch (action)
                {
                    case "forward":
                        position += value;
                        depth += (aim * value);
                        break;

                    case "up":
                        aim -= value;
                        break;

                    case "down":
                        aim += value;
                        break;

                    default:
                        throw new Exception($"Unexpected action: {action}");
                }
            }
            return position * depth;
        }

        private (string action, int value) ParseCommand(string[] command)
            => (command[0], int.Parse(command[1]));
    }
}