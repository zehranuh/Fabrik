using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fabrik
{
    class Job
    {
        public enum State {  Pending, InWork, Done }
        public string JobName { get; }
        public string Product { get; }
        public int Quantity { get; }
        private int producedQuantity;
        public  State CurrentState { get; private set; }

        public Job(string jobName, string product, int quantity)
        {
            JobName = jobName;
            Product = product;
            Quantity = quantity;
            producedQuantity = 0;
            CurrentState = State.Pending;
        }

        public void Start()
        {
            if (CurrentState == State.Pending)
            {
                CurrentState = State.InWork;
                Console.WriteLine($"Jog {JobName} gestartet: Produziere {Quantity} {Product}(s).");
            }
        }

        public void Produce()
        {
            if (CurrentState == State.InWork)
            {
                producedQuantity++;
                Console.WriteLine($"Produziere {Product}... ({producedQuantity}/{Quantity})");
                if (producedQuantity == Quantity)
                {
                    CurrentState = State.Done;
                    Console.WriteLine($"Job {JobName} abgeschlossen: {Quantity} {Product}(s) produziert.");
                }
            }
        }

        public void Pending()
        {
            if (JobName != null)
            {
                CurrentState = State.Pending;
            }
        }
    }
}
