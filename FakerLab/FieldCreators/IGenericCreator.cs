using System;
using System.Collections.Generic;

using FieldCreators;

namespace FieldCreators { 
    public interface IGenericCreator : IType
    {
        object create(Type type);
    }
}
