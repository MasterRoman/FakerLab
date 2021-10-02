using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerTests.Mocks
{
    class A
    {
        public int integer;
        public B bClass;
    }

    class B
    {
        public char character;
        public A aClass;
        public C cClass;
    }

    class C
    {
        public List<uint> list;
    }
}
