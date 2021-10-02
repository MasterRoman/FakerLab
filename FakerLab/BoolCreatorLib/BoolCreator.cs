using System;
using System.Collections.Generic;

using FieldCreators.PrimitiveTypesCreators;

namespace FieldCreators.PrimitiveTypesCreators
{
    class BoolCreator : IPrimitiveTypeCreator
    {
        public Type curType { get;}

        public BoolCreator()
        {
            this.curType = typeof(bool);
        }

        public object create()
        {
            var number = new Random().Next();
            return number % 2 == 0;
        }

    }
}
