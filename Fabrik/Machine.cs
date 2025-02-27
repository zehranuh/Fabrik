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

        public void StartJob(string product, int quantity)
        {
            if (currentState == State.Running)
            {
                Console.WriteLine("Ein Job ist bereits in Arbeit.");
                return;
            }

            currentState = State.Running;
            signalLight.SetState(SignalLight.State.Green);
            Console.WriteLine($"Job gestartet: Produziere {quantity} {product}.");

            for (int i = 1; i <= quantity; i++)
            {
                Console.WriteLine($"Produziere {product}... ({i} von {quantity})");
                Thread.Sleep(2000);
            }

            currentState = State.Ready;
            signalLight.SetState(SignalLight.State.Yellow);
            Console.WriteLine($"Job abgeschlossen: {quantity} {product} produziert.");
            Console.WriteLine("Machine ist Ready.");
        }

        public void Start()
        {
            if (currentState == State.Ready)
            {
                currentState = State.Running;
                signalLight.SetState(SignalLight.State.Green);
                Console.WriteLine("Maschine läuft...");
            }
            else
            {
                Console.WriteLine("Maschine kann nicht gestartet werden.");
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
            else
            {
                Console.WriteLine("Maschine kann nicht gestoppt werden.");
            }
        }

        public void Fail()
        {
            if (currentState == State.Running)
            {
                currentState = State.Error;
                signalLight.SetState(SignalLight.State.Red);
                Console.WriteLine("Fehler! Die Maschine ist im Error-Zustand.");
            }
        }

        public void Reset()
        {
            if (currentState == State.Error)
            {
                currentState = State.Ready;
                signalLight.SetState(SignalLight.State.Yellow);
                Console.WriteLine("Fehler behoben! Die Maschine ist bereit.");
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
    }
}