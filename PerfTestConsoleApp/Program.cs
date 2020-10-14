namespace TestConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text.Json;

    using Newtonsoft.Json;

    using JsonSerializer = System.Text.Json.JsonSerializer;

    /// <summary>
    /// The program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The numbers.
        /// </summary>
        private static IEnumerable<int> testNumbers;

        /// <summary>
        /// The main.
        /// </summary>
        private static void Main()
        {
            testNumbers = Enumerable.Range(0, 10000);
            var iterationCount = 1000;
            var timeCollection = new List<long>();

            for (var i = 0; i <= iterationCount; i++)
            {
                /* var t = RunTest(
                    numbers =>
                        {
                            // Avg 18 ms., 14 MB Mem
                            // Console.WriteLine(JsonSerializer.Serialize(numbers));

                            // Avg 18 ms., 23 MB Mem
                            Console.WriteLine(JsonConvert.SerializeObject(numbers));
                        },
                    "Json Serialize"); */

                /* Time wise both Json libs performs similarly,
                             but "System.Text.Json" takes lot less memory, than "Newtonsoft.Json" */

                // Avg 20 ms, 19 MB Mem
                var t = RunTest(
                    numbers =>
                        {
                            Console.WriteLine(string.Join(", ", numbers));
                        },
                "String Join");

                timeCollection.Add(t);
            }

            var totalTime = timeCollection.Sum();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Total Time: {totalTime} ms for {iterationCount} iteration count.");
            Console.WriteLine($"Average: {totalTime / iterationCount} ms\n\n");
            Console.ResetColor();

            Console.WriteLine("Press any key to exit.");
        }

        /// <summary>
        /// The run test.
        /// </summary>
        /// <param name="test">
        /// The test.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        private static long RunTest(Action<IEnumerable<int>> test, string name)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            test(testNumbers);

            stopwatch.Stop();

            var elapsed = stopwatch.ElapsedMilliseconds;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n{name}:\t{elapsed,4} ms\n");
            Console.ResetColor();

            return elapsed;
        }
    }
}