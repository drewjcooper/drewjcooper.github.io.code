using System;
using System.Collections.Generic;
using System.Threading;

namespace SystemRandomIsNotThreadSafe
{
    class MonitoredRandom
    {
        private readonly Random rnd = new Random();
        private int sampleCount = 0;
        private readonly SharedOrExclusiveLock rndLock = new SharedOrExclusiveLock();
        private const int monitorInterval = 10;
        private int previousOffset;
        private readonly List<InternalState> internalStates = new List<InternalState>();

        CancellationTokenSource cts;

        public MonitoredRandom(CancellationTokenSource cts)
        {
            this.cts = cts;
        }

        public IEnumerable<InternalState> InternalStates => internalStates;

        public int Next()
        {
            var value = GetNext();

            var count = Interlocked.Increment(ref sampleCount);

            if (count % monitorInterval == 1) { CheckInternalState(); }
            return value;
        }

        private int GetNext()
        {
            using (rndLock.GetSampleLock())
            {
                return rnd.Next();
            }
        }

        private void CheckInternalState()
        {
            InternalState state = GetInternalState();

            var offset = state.IndexOffset;

            if (offset != previousOffset)
            {
                internalStates.Add(state);
                previousOffset = offset;
            }

            if (offset == 0) { cts.Cancel(); }
        }

        private InternalState GetInternalState()
        {
            using (rndLock.GetExclusiveLock())
            {
                return InternalState.Get(rnd, ref sampleCount);
            }
        }
    }
}
