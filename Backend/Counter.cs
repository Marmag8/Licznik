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

        public Counter(int count, String name)
        {
            this.count = count;
            this.name = name;
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
