using System;
using System.Collections.Generic;
using System.Reflection;


namespace FakerLib
{
    class Faker : IFaker
    {
        public T create<T>()
        {
            return (T)createObject(typeof(T));
        }

        private object createObject(Type type)
        {
            object createdObject = null;

            if (type.IsValueType)
            {

            }

            return createdObject;
        }
    }
}
