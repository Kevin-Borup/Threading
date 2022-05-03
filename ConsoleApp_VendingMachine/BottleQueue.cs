using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_VendingMachine
{
    // Inspired by Stackoverflow post
    // https://stackoverflow.com/a/1305
    internal class BottleQueue<T> : Queue<T>
    {
        public object Lock;
        public int MaxLength { get; private set; }
        public bool Full { get; private set; }
        public bool Empty { get; private set; }
        public BottleQueue(int maxLength) : base(maxLength)
        {
            MaxLength = maxLength;
            Lock = new object();
            Empty = true;
            Full = false;
        }

        public new void Enqueue(T item)
        {
            while (Count >= MaxLength)
            {
                Dequeue();
            }
            base.Enqueue(item);
            CountCheck();
        }

        public new dynamic Dequeue()
        {
            var item = base.Dequeue();
            CountCheck();
            return item;
        }

        private void CountCheck()
        {
            if (Count == MaxLength)
            {
                Full = true;
                Empty = false;
            }
            else if (Count < 1)
            {
                Empty = true;
                Full = false;
            }
            else
            {
                Empty = false;
                Full = false;
            }
        }
    }
}
