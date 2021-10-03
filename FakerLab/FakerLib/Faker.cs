using System;
using System.Collections.Generic;
using System.Reflection;

using FieldCreators.PrimitiveTypesCreators;
using FieldCreators;

using System.IO;

namespace FakerLib
{
    public class Faker : IFaker
    {

        private Dictionary<Type, IPrimitiveTypeCreator> primitiveTypeCreator;
        private Dictionary<Type, IGenericCreator> genericTypeCreator;
        private Dictionary<PropertyInfo, IPrimitiveTypeCreator> customPrimitiveTypeCreator = new Dictionary<PropertyInfo, IPrimitiveTypeCreator>();

        private HashSet<Type> createdTypesInClass;

        public Faker()
        {
            this.primitiveTypeCreator = PrimitiveTypesCreator.getPrimitiveTypes();
            this.createdTypesInClass = new HashSet<Type>();
            this.genericTypeCreator = new Dictionary<Type, IGenericCreator>();
            var assemblies = new List<Assembly>();
            var path = "D:\\SPPLABS\\FakerLab\\FakerLab\\ListCreatorLib\\bin\\Debug";

            try
            {
                foreach (string file in Directory.GetFiles(path, "*.dll"))
                {
                    try
                    {
                        assemblies.Add(Assembly.LoadFile(file));
                    }
                    catch (BadImageFormatException)
                    { }
                    catch (FileLoadException)
                    { }
                }
            }
            catch (DirectoryNotFoundException)
            { }

            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    foreach (Type typeInterface in type.GetInterfaces())
                    {
                        if (typeInterface.Equals(typeof(IGenericCreator)))
                        {
                            var creator = (IGenericCreator)Activator.CreateInstance(type, this.primitiveTypeCreator);
                            genericTypeCreator.Add(creator.curType, creator);
                        }
                        else if (typeInterface.Equals(typeof(IPrimitiveTypeCreator)))
                        {
                            var creator = (IPrimitiveTypeCreator)Activator.CreateInstance(type);
                            primitiveTypeCreator.Add(creator.curType, creator);
                        }
                    }
                }
            }
        }


        public Faker(FakerConfig config) : this()
        {
            this.customPrimitiveTypeCreator = config.creators;
        }

        public T create<T>()
        {
            return (T)createObject(typeof(T));
        }

        private object createObject(Type type)
        {
            object createdObject = null;


            if (primitiveTypeCreator.TryGetValue(type, out IPrimitiveTypeCreator creator))
            {
                createdObject = creator.create();
            }
            else if (type.IsValueType)
            {
                createdObject = Activator.CreateInstance(type);
            }
            else if (type.IsGenericType && genericTypeCreator.TryGetValue(type.GetGenericTypeDefinition(), out IGenericCreator genCreator))
            {
                createdObject = genCreator.create(type.GenericTypeArguments[0]); //type of object in collection
            }
            else if (type.IsClass && !type.IsArray && !type.IsPointer && !type.IsAbstract && !type.IsGenericType)
            {
                if (!createdTypesInClass.Contains(type))
                {
                    createdObject = createClass(type);
                }
                else
                {
                    createdObject = null;
                }
            }

            return createdObject;
        }


        private object createClass(Type type)
        {
            object createdClass = null;

            int largestConstructor = 0;
            ConstructorInfo constructor = null;
            var constructorsOfClass = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (ConstructorInfo curConstructor in constructorsOfClass)
            {
                var curCount = curConstructor.GetParameters().Length;
                if (curCount > largestConstructor)
                {
                    largestConstructor = curCount;
                    constructor = curConstructor;
                }
            }

            createdTypesInClass.Add(type);


            if (constructor != null)
            {
                createdClass = createFromConstructor(constructor, type);
            }

            createdClass = createFromProperties(type, createdClass);

            createdTypesInClass.Remove(type);

            return createdClass;
        }


        private object createFromProperties(Type type, object createdObject)
        {
            object created = null;
            if (createdObject == null)
            {
                created = Activator.CreateInstance(type);
            }
            else
            {
                created = createdObject;
            }

            foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic))
            {
                if (fieldInfo.GetValue(created) == null)
                {
                    object value = null;
                    if (!createByCustomCreator(fieldInfo, out value))
                    {
                        value = createObject(fieldInfo.FieldType);
                    }
                    fieldInfo.SetValue(created, value);
                }

            }

            foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic))
            {
                if (propertyInfo.CanWrite)
                {
                    if (propertyInfo.GetValue(created) == null)
                    {
                        object value = null;
                        if (!createByCustomCreator(propertyInfo, out value))
                        {
                            value = createObject(propertyInfo.PropertyType);
                        }
                        propertyInfo.SetValue(created, value);
                    }
                }
            }

            return created;
        }

        private object createFromConstructor(ConstructorInfo constructor, Type type)
        {
            var parametersValues = new List<object>();

            foreach (ParameterInfo parameterInfo in constructor.GetParameters())
            {
               object value = null;
               if (!createByCustomCreator(parameterInfo,type,out value)) {
                    value = createObject(parameterInfo.ParameterType);
                }
                parametersValues.Add(value);
            }

            try
            {
                return constructor.Invoke(parametersValues.ToArray());
            }
            catch (TargetInvocationException)
            {
                return null;
            }
        }


        private bool createByCustomCreator(ParameterInfo parameterInfo, Type type,out object created)
        {
            foreach (KeyValuePair<PropertyInfo, IPrimitiveTypeCreator> keyValue in customPrimitiveTypeCreator)
            {
                if (keyValue.Key.Name == parameterInfo.Name && keyValue.Value.curType.Equals(parameterInfo.ParameterType)  && keyValue.Key.ReflectedType.Equals(type))
                {
                    created = keyValue.Value.create();
                    return true;
                }
            }
            created = null;
            return false;
        }

        private bool createByCustomCreator(PropertyInfo propertyInfo, out object created)
        {
            if (customPrimitiveTypeCreator.TryGetValue(propertyInfo, out IPrimitiveTypeCreator creator))
            {
                created = creator.create();
                return true;
            }
            else
            {
                created = null;
                return false;
            }
        }

        private bool createByCustomCreator(FieldInfo fieldInfo, out object created)
        {
            foreach (KeyValuePair<PropertyInfo, IPrimitiveTypeCreator> keyValue in customPrimitiveTypeCreator)
            {
                if (keyValue.Key.Name == fieldInfo.Name  && keyValue.Value.curType.Equals(fieldInfo.FieldType) && keyValue.Key.ReflectedType.Equals(fieldInfo.ReflectedType))
                {
                    created = keyValue.Value.create();
                    return true;
                }
            }
            created = null;
            return false;
        }

    }
}
