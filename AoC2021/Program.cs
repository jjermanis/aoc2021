﻿using System;

namespace AoC2021
{
    internal class Program
    {
        private static void Main()
        {
            int start = Environment.TickCount;

            new Day22().Do();

            Console.WriteLine($"Time: {Environment.TickCount - start} ms");
        }
    }
}