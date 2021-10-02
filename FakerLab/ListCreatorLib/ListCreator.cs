using System;
using System.Collections;
using System.Collections.Generic;

using FieldCreators.PrimitiveTypesCreators;
using FieldCreators;

namespace ListCreatorLib
{
    class ListCreator : IGenericCreator
    {

        public Type curType { get; }
        private Dictionary<Type, IPrimitiveTypeCreator> primitiveTypeCreator;

        public ListCreator(Dictionary<Type, IPrimitiveTypeCreator> primitiveTypeCreator)
        {
            this.curType = typeof(List<>);
            this.primitiveTypeCreator = primitiveTypeCreator;
        }

        public object create(Type type)
        {
            IList result = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
            var length = new Random().Next(1, 10);

            if (primitiveTypeCreator.TryGetValue(type, out IPrimitiveTypeCreator creator))
            {
                for (int i = 0; i < length; i++)
                {
                    result.Add(creator.create());
                }
            }
            else
            {
                var defaultValue = "DEFAULT";
                for (int i = 0; i < length; i++)
                {
                    result.Add(defaultValue);
                }
            }
            return result;
        }
    }
}
