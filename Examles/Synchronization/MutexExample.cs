using System;
using System.Threading;
using Infrastructure;
using Infrastructure.Extensions;
using Infrastructure.Logging;

namespace Examples.Synchronization
{
    /**
     * A Mutex is like a C# lock, but it can work across multiple processes.
     * In other words, Mutex can be computer-wide as well as application-wide.
     *
     * A Mutex is a synchronization primitive that can also be used for interprocess synchronization.
     * When two or more threads need to access a shared resource at the same time,
     * the system needs a synchronization mechanism to ensure that only one thread at a time uses the resource.
     *
     * Mutex is a synchronization primitive that grants exclusive access to the shared resource to only one thread.
     * If a thread acquires a Mutex, the second thread that wants to acquire that Mutex
     * is suspended until the first thread releases the Mutex.
     */
    [Run]
    public class MutexExample : BaseSynchronousExample
    {
        private static readonly Mutex Mutex = new Mutex();

        public MutexExample(ILogger logger)
        {
            Logger = logger;
        }

        public override void Run()
        {
            for (var i = 0; i < 5; i++)
            {
                ThreadPool.QueueUserWorkItem((obj) => UseResource());
            }
        }

        private void UseResource()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            var threadColor = threadId.GetColor(); // extension method

            Logger.Write($"Thread {threadId} starting method", threadColor);

            Mutex.WaitOne();

            Logger.Write($"Thread {threadId} entered synchronized context", threadColor);

            Thread.Sleep(TimeSpan.FromSeconds(1));

            Logger.Write($"Thread {threadId} exiting synchronized context", threadColor);

            Mutex.ReleaseMutex();

        }
    }
}