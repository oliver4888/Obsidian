using System;

namespace Obsidian.Util
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class VariableAttribute : Attribute
    {
        public int Order { get; }

        public VariableType Type { get; }

        public int Size { get; }

        public VariableAttribute(int order, VariableType type = VariableType.Auto, int size = 0)
        {
            this.Order = order;
            this.Type = type;
            this.Size = size;
        }
    }

    public enum VariableType
    {
        /// <summary>
        /// Automatically figures out what data type to use for this variable.
        /// </summary>
        Auto,

        Int,

        Long,

        VarInt,

        VarLong,

        UnsignedByte,

        Byte,

        Short,

        UnsignedShort,

        String,

        Array,

        ByteArray,

        LongArray,

        List,

        Position,

        Boolean,

        Float,

        Double,

        Transform,

        UUID,

        Chat,

        Tranform
    }
}