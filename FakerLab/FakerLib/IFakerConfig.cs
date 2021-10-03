using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FieldCreators.PrimitiveTypesCreators;

namespace FakerLib
{
    public interface IFakerConfig
    {
        void add<TClass,TProperty, TPrimitive>(Expression<Func<TClass, TProperty>> expression)
            where TClass : class
            where TPrimitive : IPrimitiveTypeCreator;
    }
}
