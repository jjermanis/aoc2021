using System;

namespace AoC2021
{
    internal class Program
    {
        private static void Main()
        {
            int start = Environment.TickCount;

            new Day15().Do();

            Console.WriteLine($"Time: {Environment.TickCount - start} ms");
        }
    }
}