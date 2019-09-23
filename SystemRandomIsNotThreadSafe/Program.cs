using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SystemRandomIsNotThreadSafe
{
    class Program
    {
        private static MonitoredRandom rnd;

        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            rnd = new MonitoredRandom(cts);

            var threads = new List<Thread> { new Thread(HammerRandom), new Thread(HammerRandom) };
            threads.ForEach(t => t.Start(cts.Token));

            cts.Token.WaitHandle.WaitOne();

            threads.ForEach(t => t.Join());

            Console.WriteLine(String.Join(Environment.NewLine, rnd.InternalStates));
        }

        private static void HammerRandom(object cancellationToken)
        {
            var token = (CancellationToken)cancellationToken;

            while (!token.IsCancellationRequested)
            {
                rnd.Next();
            }
        }
    }
}
