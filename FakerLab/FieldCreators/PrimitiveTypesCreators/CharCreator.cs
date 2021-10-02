using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldCreators.PrimitiveTypesCreators
{
    class CharCreator : IPrimitiveTypeCreator
    {
        public Type curType { get; }

        public CharCreator()
        {
            this.curType = typeof(char);
        }

        public object create()
        {
            var number = new Random().Next();
            return (char)number;
        }

    }
}
