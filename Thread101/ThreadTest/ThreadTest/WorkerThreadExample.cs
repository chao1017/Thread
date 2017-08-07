using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{
    public class Worker
    {
        // Volatile is used as hint to the compiler that this data
        // member will be accessed by multiple threads.
        private volatile bool _shouldStop = false;

        // This method will be called when the thread is started.
        public void DoWork()
        {
            while (!_shouldStop)
            {
                Console.WriteLine("worker thread: working...");
            }

            Console.WriteLine("worker thread: terminating gracefully.");   
        }

        public void RequestStop()
        {
            _shouldStop = true;
        }
    }

    public class WorkerThreadExample
    {
        static void Main(string[] args)
        {
            // Create the thread object. This does not start the thread.
            Worker workerObj = new Worker();

            Thread workerThread = new Thread(workerObj.DoWork);

            // Start the worker thread.
            workerThread.Start();
            Console.WriteLine("main thread: Starting worker thread...");

            // Loop until worker thread activates.
            while (!workerThread.IsAlive) ;

            // Put the main thread to sleep for 1 millisecond to
            // allow the worker thread to do some work:
            Thread.Sleep(1);

            // Request that the worker thread stop itself:
            workerObj.RequestStop();

            // Use the Join method to block the current thread 
            // until the object's thread terminates.
            workerThread.Join();
            Console.WriteLine("main thread: Worker thread has terminated.");
        }
    }
}
