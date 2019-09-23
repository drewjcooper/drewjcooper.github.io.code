using System;
using System.Threading;

namespace SystemRandomIsNotThreadSafe
{
    class SharedOrExclusiveLock
    {
        private readonly ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

        public IDisposable GetSampleLock()
        {
            rwLock.EnterReadLock();
            return new UnlockAction(() => rwLock.ExitReadLock());
        }

        public IDisposable GetExclusiveLock()
        {
            rwLock.EnterWriteLock();
            return new UnlockAction(() => rwLock.ExitWriteLock());
        }

        private class UnlockAction : IDisposable
        {
            private readonly Action unlockAction;

            public UnlockAction(Action unlockAction)
            {
                this.unlockAction = unlockAction;
            }

            public void Dispose()
            {
                unlockAction.Invoke();
            }
        }
    }
}
