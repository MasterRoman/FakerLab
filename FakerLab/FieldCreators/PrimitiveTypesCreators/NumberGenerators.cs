using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldCreators.PrimitiveTypesCreators
{
    class ByteCreator : IPrimitiveTypeCreator
    {
        public Type curType { get; }

        public ByteCreator()
        {
            this.curType = typeof(byte);
        }

        public object create()
        {
            var number = new Random().Next();
            return (byte)number;
        }

    }

    class SbyteCreator : IPrimitiveTypeCreator
    {
        public Type curType { get; }

        public SbyteCreator()
        {
            this.curType = typeof(sbyte);
        }

        public object create()
        {
            var number = new Random().Next();
            return (sbyte)number;
        }

    }

    class DecimalCreator : IPrimitiveTypeCreator
    {
        public Type curType { get; }

        public DecimalCreator()
        {
            this.curType = typeof(decimal);
        }

        public object create()
        {
            var number = new Random().NextDouble();
            return (decimal)number;
        }

    }

    class DoubleCreator : IPrimitiveTypeCreator
    {
        public Type curType { get; }

        public DoubleCreator()
        {
            this.curType = typeof(double);
        }

        public object create()
        {
            var number = new Random().NextDouble();
            return (double)number;
        }

    }

    class FloatCreator : IPrimitiveTypeCreator
    {
        public Type curType { get; }

        public FloatCreator()
        {
            this.curType = typeof(float);
        }

        public object create()
        {
            var number = new Random().NextDouble();
            return (float)number;
        }

    }

    class IntCreator : IPrimitiveTypeCreator
    {
        public Type curType { get; }

        public IntCreator()
        {
            this.curType = typeof(int);
        }

        public object create()
        {
            var number = new Random().Next();
            return (int)number;
        }

    }

    class UIntCreator : IPrimitiveTypeCreator
    {
        public Type curType { get; }

        public UIntCreator()
        {
            this.curType = typeof(uint);
        }

        public object create()
        {
            var number = new Random().Next();
            return (uint)number;
        }

    }

    class LongCreator : IPrimitiveTypeCreator
    {
        public Type curType { get; }

        public LongCreator()
        {
            this.curType = typeof(long);
        }

        public object create()
        {
            var number = new Random().Next();
            return (long)number;
        }

    }

    class ULongCreator : IPrimitiveTypeCreator
    {
        public Type curType { get; }

        public ULongCreator()
        {
            this.curType = typeof(ulong);
        }

        public object create()
        {
            var number = new Random().Next();
            return (ulong)number;
        }

    }


    class ShortCreator : IPrimitiveTypeCreator
    {
        public Type curType { get; }

        public ShortCreator()
        {
            this.curType = typeof(short);
        }

        public object create()
        {
            var number = new Random().Next();
            return (short)number;
        }

    }

    class UShortCreator : IPrimitiveTypeCreator
    {
        public Type curType { get; }

        public UShortCreator()
        {
            this.curType = typeof(ushort);
        }

        public object create()
        {
            var number = new Random().Next();
            return (ushort)number;
        }

    }
}
