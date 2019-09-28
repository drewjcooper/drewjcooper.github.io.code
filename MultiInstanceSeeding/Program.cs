using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MultiInstanceSeeding
{
    class Program
    {
        private static ConcurrentDictionary<string, IEnumerable<int>> results =
            new ConcurrentDictionary<string, IEnumerable<int>>();

        private const int threadCount = 3;
        private const int instanceCount = 3;
        private const int sampleCount = 20;
        private const int sampleRange = 100;

        static void Main(string[] args)
        {
            var threads = Enumerable
                .Range(0, threadCount)
                .Select(_ => new Thread(ExerciseMultipleInstances))
                .ToList();
            threads.ForEach(t => t.Start());
            threads.ForEach(t => t.Join());
            
            foreach (var kvp in results.OrderBy(x => x.Value.First()))
            {
                var rndId = kvp.Key;
                var sequence = kvp.Value;

                Console.WriteLine($"{rndId,5}: {String.Join(", ", sequence.Select(x => $"{x,2}"))}");
            }
        }

        static void ExerciseMultipleInstances()
        {
            // Create multiple instance quickly
            var rnd = Enumerable
                .Range(0, instanceCount)
                .Select(_ => new Random())
                .ToArray();

            for (int i = 0; i < instanceCount; i++)
            {
                results[$"{Thread.CurrentThread.ManagedThreadId}.{i}"] = 
                    Enumerable
                        .Range(0, sampleCount)
                        .Select(_ => rnd[i].Next(sampleRange))
                        .ToArray();
            }
        }
    }
}
