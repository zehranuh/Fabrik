using Fabrik;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fabrik
{
    class JobManager
    {
        private Queue<Job> jobQueue = new Queue<Job>();
        private Machine machine = new Machine();

        public void AddJob(Job job)
        {
            jobQueue.Enqueue(job);
            Console.Write($"Job {job.JobName} hinzugefügt.");
        }

        public void StartJobs()
        {

            if (jobQueue.Count == 0)
            {
                Console.WriteLine("Keine weiteren Jobs in der Warteschlange.");
                return;
            }

            Job job = jobQueue.Dequeue();
            job.Start();
            machine.Start();
            Console.WriteLine($"Job gestartet: Produziere {job.Quantity} {job.Product}.");

            Random random = new Random();
            for (int i = 1; i <= job.Quantity; i++)
            {
                if (random.Next(0, 10) < 1)
                {
                    machine.Fail();
                    break;
                }

                Console.WriteLine($"Produziere {job.Product}... ({i} von {job.Quantity})");
                Thread.Sleep(2000);
            }

            if (!machine.IsRunning())
            {
                machine.Stop();
                Console.WriteLine($"Job abgeschlossen: {job.Quantity} {job.Product} produziert.");
            }
        }

        public void Status()
        {
            machine.Status();
            Console.WriteLine($"Jobs in der Wartenschlange: {jobQueue.Count}");
        }
    }
}
