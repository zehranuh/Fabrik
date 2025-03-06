using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Fabrik
{
    public class Machine
    {
        private enum State { Ready, Running, Error }
        private State currentState = State.Ready;
        private SignalLight signalLight = new SignalLight();

        public event Action<string> MachineFailed;

        public void Start()
        {
            if (currentState == State.Ready)
            {
                currentState = State.Running;
                signalLight.SetState(SignalLight.State.Green);
                Console.WriteLine("Maschine läuft...");
            }
        }

        public void Stop()
        {
            if (currentState == State.Running || currentState == State.Ready)
            {
                currentState = State.Ready;
                signalLight.SetState(SignalLight.State.Yellow);
                Console.WriteLine("Maschine gestoppt.");
            }
        }

        public void Fail()
        {
            if (currentState == State.Running)
            {
                currentState = State.Error;
                signalLight.SetState(SignalLight.State.Red);
                Console.WriteLine("Fehler! Die Maschine ist im Error-Zustand.");
                MachineFailed?.Invoke("Fehler! Die Maschine ist im Error-Zustand. Geben Sie '9944' ein, um den Fehler zu beheben.");
            }
        }

        public bool FixError(string code)
        {
            if (currentState == State.Error && code == "9944")
            {
                currentState = State.Running;
                signalLight.SetState(SignalLight.State.Green);
                Console.WriteLine("Fehler behoben! Die Maschine ist bereit.");
                return true;
            }
            else
            {
                Console.WriteLine("Falscher Code. Fehler nicht behoben.");
                return false;
            }
        }

        public void CheckRunning()
        {
            if (currentState == State.Running)
            {
                Console.WriteLine("Maschine arbeitet...");
            }
        }

        public bool IsRunning()
        {
            return currentState == State.Running;
        }

        public bool IsNotRunning()
        {
            return currentState == State.Ready;
        }

        public SignalLight.State GetSignalLightState()
        {
            return signalLight.GetState();
        }

        public string Status()
        {
            return $"{currentState}";
        }
    }
}