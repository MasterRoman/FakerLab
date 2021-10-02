using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldCreators.PrimitiveTypesCreators
{
    public interface IPrimitiveTypeCreator : IType
    {
        object create();
    }

}
