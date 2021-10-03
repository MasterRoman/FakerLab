using System;
using System.Collections.Generic;
using System.Linq;

using FieldCreators.PrimitiveTypesCreators;

namespace FakerLib
{
    public class DefaultStringCreator : IPrimitiveTypeCreator
    {
        public Type curType { get; }

        public DefaultStringCreator()
        {
            this.curType = typeof(string);
        }

        public object create()
        {
            return "default";
        }

    }
}

