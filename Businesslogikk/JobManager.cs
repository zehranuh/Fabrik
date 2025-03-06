using Fabrik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fabrik
{
    public class JobManager
    {
        private Queue<Job> jobQueue = new Queue<Job>();
        private Machine machine = new Machine();
        private Job currentJob;
        private int currentStep;

        public event Action<string> JobStatusChanged;
        public event Action<string> JobCompleted;
        public event Action<string> MachineFailed;

        public void AddJob(Job job)
        {
            jobQueue.Enqueue(job);
            JobStatusChanged?.Invoke($"Job {job.JobName} hinzugefügt.");
        }

        public async void StartJobs()
        {
            if (jobQueue.Count == 0)
            {
                JobStatusChanged?.Invoke("Keine weiteren Jobs in der Wartenschlange.");
                return;
            }

            currentJob = jobQueue.Dequeue();
            currentJob.Start();
            machine.Start();
            JobStatusChanged?.Invoke($"Job gestartet: Produziere {currentJob.Quantity} {currentJob.Product}.");

            Random random = new Random();
            bool jobCompleted = true;
            for (currentStep = 1; currentStep <= currentJob.Quantity; currentStep++)
            {
                if (random.Next(0, 10) < 1)
                {
                    machine.Fail();
                    MachineFailed?.Invoke("Fehler! Die Maschine ist im Error-Zustand. Geben Sie '9944' ein, um den Fehler zu beheben.");
                    jobCompleted = false;
                    break;
                }
                JobStatusChanged?.Invoke($"Produziere {currentJob.Product}... ({currentStep} von {currentJob.Quantity})");
                await Task.Delay(2000);
            }

            if (jobCompleted)
            {
                machine.Stop();
                JobCompleted?.Invoke($"Job abgeschlossen: {currentJob.Quantity} {currentJob.Product} produziert.");
            }
        }

        public void ContinueJob()
        {
            if (currentJob == null || machine.IsNotRunning())
            {
                return;
            }
            Random random = new Random();
            bool jobCompleted = true;
            for (; currentStep <= currentJob.Quantity; currentStep++)
            {
                if (random.Next(0,10) < 1)
                {
                    machine.Fail();
                    MachineFailed?.Invoke("Fehler! Die Maschine ist im Error-Zustand. Geben Sie '9944' ein, um den Fehler zu beheben.");
                    jobCompleted = false;
                    break;
                }

                JobStatusChanged?.Invoke($"Produziere {currentJob.Product}... ({currentStep} von {currentJob.Quantity})");
                Task.Delay(2000).Wait(); 
            }

            if (jobCompleted)
            {
                machine.Stop();
                JobCompleted?.Invoke($"Job abgeschlossen: {currentJob.Quantity} {currentJob.Product} produziert.");
            }
        }

        public void Status()
        {
            JobStatusChanged?.Invoke(machine.Status());
            JobStatusChanged?.Invoke($"Jobs in der Wartenschlange: {jobQueue.Count}");
        }
    }
}
