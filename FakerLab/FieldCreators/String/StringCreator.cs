using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldCreators.PrimitiveTypesCreators;

namespace FieldCreators.String
{
    class StringCreator : IPrimitiveTypeCreator
    {
        public Type curType { get; }

        public StringCreator()
        {
            this.curType = typeof(string);
        }

        public object create()
        {
            byte[] bytesArray = new byte[128];
            new Random().NextBytes(bytesArray);
           
            return (string)bytesArray.ToString();
        }

    }
}
