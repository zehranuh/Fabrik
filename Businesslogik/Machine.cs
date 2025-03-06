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
                Console.WriteLine("Geben Sie '9944' ein, um den Fehler zu beheben:");
                string input = Console.ReadLine();
                if (input == "9944")
                {
                    currentState = State.Running;
                    Console.WriteLine("Fehler behoben! Die Maschine ist bereit, starte noch mal deinen Job.");
                }
                else
                {
                    Console.WriteLine("Falscher Code. Fehler nicht behoben.");
                }
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

        public SignalLight.State GetSignalLightState()
        {
            return signalLight.GetState();
        }

        public void Status()
        {
            string status = $"Maschinenstatus: {currentState}, Signalleuchte: {signalLight.GetState()}";
            Console.WriteLine(status);
        }
    }
}