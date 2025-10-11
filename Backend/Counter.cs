using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licznik.Backend
{
    internal class Counter
    {
        private int _count;
        public int count 
        { 
            get { return _count; } 
            set { _count = value; }
        }

        private String _name;
        public String name 
        { 
            get { return _name; } 
            set { _name = value; }
        }

        private int _initialCount;
        public int initialCount
        {
            get { return _initialCount; }
            set { _initialCount = value; }
        }

        public Counter(int count, String name)
        {
            this.count = count;
            this.name = name;
            this.initialCount = count;
        }

        public Counter(int count, String name, int initialCount)
        {
            this.count = count;
            this.name = name;
            this.initialCount = initialCount;
        }

        public void Increment()
        {
            count++;
        }

        public void Decrement()
        {
            count--;
        }
    }
}
