using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldCreators.PrimitiveTypesCreators
{
    class BoolCreator : IPrimitiveTypeCreator
    {
        public Type curCype { get;}

        public BoolCreator()
        {
            this.curCype = typeof(bool);
        }

        public object create()
        {
            var number = new Random().Next();
            return number % 2 == 0;
        }

    }
}
