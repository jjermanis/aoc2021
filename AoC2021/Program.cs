using System;

namespace AoC2021
{
    class Program
    {
        static void Main()
        {
            int start = Environment.TickCount;

            new Day01().Do();

            Console.WriteLine($"Time: {Environment.TickCount - start} ms");
        }
    }
}
