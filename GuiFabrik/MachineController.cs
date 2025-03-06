using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using Fabrik;

namespace GuiFabrik
{
    public class MachineController
    {
        private Machine _machine;
        private JobManager _jobManager;

        public event Action<string> JobStatusChanged;
        public event Action<string> JobCompleted;
        public event Action<string> MachineFailed;

        public MachineController(Machine machine, JobManager jobManager)
        {
            _machine = machine;
            _jobManager = jobManager;
            _jobManager.JobStatusChanged += (status) => JobStatusChanged?.Invoke(status);
            _jobManager.JobCompleted += (status) => JobCompleted?.Invoke(status);
            _jobManager.MachineFailed += (status) => MachineFailed?.Invoke(status);
            _machine.MachineFailed += (status) => MachineFailed?.Invoke(status);
        }

        public void StartMachine()
        {
            _machine.Start();
        }

        public void StopMachine()
        {
            _machine.Stop();
        }

        public void FailMachine()
        {
            _machine.Fail();
        }

        public void FixMachineError(string code)
        {
            if (_machine.FixError(code))
            {
                _jobManager.ContinueJob();
                JobStatusChanged?.Invoke("Fehler behoben! Starte noch mal deinen Job.");
            }
        }

        public string GetMachineStatus()
        {
            return _machine.Status();
        }

        public bool IsMachineRunning()
        {
            return _machine.IsRunning();
        }

        public bool IsMachineNotRunning()
        {
            return _machine.IsNotRunning();
        }

        public SignalLight.State GetSignalLightState()
        {
            return _machine.GetSignalLightState();
        }

        public void AddJob(Job job)
        {
            _jobManager.AddJob(job);
        }

        public void StartJobs()
        {
            _jobManager.StartJobs();
        }

        public void GetJobStatus()
        {
            _jobManager.Status();
        }
    }
}