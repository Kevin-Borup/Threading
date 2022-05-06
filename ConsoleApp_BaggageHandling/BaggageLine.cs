using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_BaggageHandling
{
    internal class BaggageLine<T> : Queue<T>
    {
        private int maxLength;
        public bool Empty { get; private set; }
        public bool Full { get; private set; }

        public BaggageLine(int length) : base(length)
        {
            maxLength = length;
        }

        public new void Enqueue(T item)
        {
            if (Count >= maxLength)
            {
                return;
            }
            else
            {
                base.Enqueue(item);
                CountCheck();
            }
        }

        public new dynamic Dequeue()
        {
            var item = base.Dequeue();
            CountCheck();
            return item;
        }

        private void CountCheck()
        {
            if (Count >= maxLength)
            {
                Full = true;
                Empty = false;
            }
            else if (Count <= 0)
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
