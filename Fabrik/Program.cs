using Fabrik;
using System;


namespace Fabrik
{
    class Program
    {
        public static void Main(string[] args)
        {
            Machine machine = new Machine();
            int tickCounter = 0;

            while (true)
            {
                if (tickCounter % 3 == 0)
                {
                    machine.CheckRunning();
                }

                Console.WriteLine("Befehle: start, stop, fail, reset, exit");
                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "start":
                        machine.Start();
                        break;
                    case "stop":
                        machine.Stop();
                        break;
                    case "fail":
                        machine.Fail();
                        break;
                    case "reset":
                        machine.Reset();
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Ungültiger Befehl");
                        break;
                }
                Console.WriteLine($"Aktuelle Signalleuchte: {machine.GetSignalLightState()}");
                tickCounter++;
            }
        }
    }
}