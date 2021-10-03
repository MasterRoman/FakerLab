using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using FieldCreators.PrimitiveTypesCreators;


namespace FakerLib
{
    public class FakerConfig : IFakerConfig
    {
        public Dictionary<PropertyInfo, IPrimitiveTypeCreator> creators;

        public void add<TClass, TProperty, TPrimitive>(Expression<Func<TClass, TProperty>> expression)
           where TClass : class
           where TPrimitive : IPrimitiveTypeCreator
        {
            Expression expressionBody = expression.Body;
            IPrimitiveTypeCreator creator = (IPrimitiveTypeCreator)Activator.CreateInstance(typeof(TPrimitive));
            if (!creator.curType.Equals(typeof(TProperty)))
            {
                throw new ArgumentException("Types of creators aren't match");
            }
            creators.Add((PropertyInfo)((MemberExpression)expressionBody).Member, creator);
        }

        public FakerConfig()
        {
            creators = new Dictionary<PropertyInfo, IPrimitiveTypeCreator>();
        }
    }
}
