using Fabrik;
using System;


namespace Fabrik
{
    class Program
    {
        public static void Main(string[] args)
        {
            Machine machine = new Machine();

            while (true)
            {
                Console.WriteLine("Befehle: start, stop, fail, reset, exit");
                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "start":
                        Console.WriteLine("Wähle einen Job: 1=Auto bauen, 2=Kabel erstellen, 3=Metallstück herstellen");
                        int jobType = int.Parse(Console.ReadLine());
                        string product = jobType switch
                        {
                            1 => "Auto",
                            2 => "Kabel",
                            3 => "Metallstück",
                            _ => throw new ArgumentException("Ungültiger Job-Typ")
                        };
                        Console.WriteLine("Stückzahl:");
                        int quantity = int.Parse(Console.ReadLine());
                        machine.StartJob(product, quantity);
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
            }
        }
    }
}