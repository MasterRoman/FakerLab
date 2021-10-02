using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FieldCreators.PrimitiveTypesCreators;
using FieldCreators.String;
using FieldCreators.Date;

namespace FieldCreators
{
    public static class PrimitiveTypesCreator
    {
        public static Dictionary<Type, IPrimitiveTypeCreator> getPrimitiveTypes()
        {
            var dict = new Dictionary<Type, IPrimitiveTypeCreator>();
        
            addToDict(dict,new BoolCreator());
            addToDict(dict, new ByteCreator());
            addToDict(dict, new SbyteCreator());
         
            addToDict(dict, new DecimalCreator());
            addToDict(dict, new DoubleCreator());
            addToDict(dict, new FloatCreator());
            addToDict(dict, new IntCreator());
            addToDict(dict, new UIntCreator());

            addToDict(dict, new LongCreator());
            addToDict(dict, new ULongCreator());

            addToDict(dict, new ShortCreator());
            addToDict(dict, new UShortCreator());

            addToDict(dict, new DateTimeCreator());

            addToDict(dict, new StringCreator());

            return dict;
        }

        private static void addToDict(Dictionary<Type, IPrimitiveTypeCreator> dict,IPrimitiveTypeCreator creator)
        {
            dict.Add(creator.curType, creator);
        }
    }

 
}
