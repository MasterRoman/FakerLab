using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerTests.Mocks
{
    class ClassWithConstructor
    {
        private int a;
        public DateTime b;
        public C c;

        private ClassWithConstructor(int a,DateTime b)
        {
            this.a = a;
            this.b = b;

        }
    }
}
