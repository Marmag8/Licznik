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

        private int _r;
        public int r
        {
            get { return _r; }
            set { _r = value; }
        }

        private int _g;
        public int g
        {
            get { return _g; }
            set { _g = value; }
        }

        private int _b;
        public int b
        {
            get { return _b; }
            set { _b = value; }
        }

        public Counter(int count, String name, int r, int g, int b)
        {
            this.count = count;
            this.name = name;
            this.initialCount = count;
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public Counter(int count, String name, int initialCount, int r, int g, int b)
        {
            this.count = count;
            this.name = name;
            this.initialCount = initialCount;
            this.r = r;
            this.g = g;
            this.b = b;
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
