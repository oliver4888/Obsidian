using Obsidian.Chat;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Obsidian.Util
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class VariableAttribute : Attribute
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

    public class Variable
    {
        public Variable(object info, VariableAttribute attribute)
        {
            this.Info = info ?? throw new ArgumentNullException(nameof(info));
            this.Attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
        }

        public object Info { get; }

        public VariableAttribute Attribute { get; }

        public VariableType Type
        {
            get
            {
                VariableType variableType = Attribute.Type;

                if (variableType == VariableType.Auto)
                {
                    Type type = GetValueType();

                    if (type == typeof(bool)) return VariableType.Boolean;
                    else if (type == typeof(byte)) return VariableType.UnsignedByte;
                    else if (type == typeof(byte[])) return VariableType.ByteArray;
                    else if (type == typeof(ChatMessage)) return VariableType.Chat;
                    else if (type == typeof(double)) return VariableType.Double;
                    else if (type == typeof(float)) return VariableType.Float;
                    else if (type == typeof(Guid)) return VariableType.UUID;
                    else if (type == typeof(int)) return VariableType.VarInt;
                    else if (type == typeof(long)) return VariableType.VarLong;
                    else if (type == typeof(Position)) return VariableType.Position;
                    else if (type == typeof(sbyte)) return VariableType.Byte;
                    else if (type == typeof(short)) return VariableType.Short;
                    else if (type == typeof(string)) return VariableType.String;
                    else if (type == typeof(Transform)) return VariableType.Transform;
                    else if (type == typeof(ushort)) return VariableType.UnsignedShort;
                    else if (type.IsEnum) return VariableType.VarInt;
                    else Console.WriteLine($"Failed to get type: {type.Name}");
                }

                return variableType;
            }
        }

        public Type GetValueType()
        {
            if (Info is PropertyInfo propertyInfo)
            {
                return propertyInfo.PropertyType;
            }
            else if (Info is FieldInfo fieldInfo)
            {
                return fieldInfo.FieldType;
            }

            Debugger.Break(); //this isn't supposed to be hit.
            throw new NotImplementedException();
        }

        public object GetValue(object obj)
        {
            if (Info is PropertyInfo propertyInfo)
            {
                return propertyInfo.GetValue(obj);
            }
            else if (Info is FieldInfo fieldInfo)
            {
                return fieldInfo.GetValue(obj);
            }

            Debugger.Break(); //this isn't supposed to be hit.
            throw new NotImplementedException();
        }

        public void SetValue(object obj, object value)
        {
            if (Info is PropertyInfo propertyInfo)
            {
                propertyInfo.SetValue(obj, value);
                return;
            }
            else if (Info is FieldInfo fieldInfo)
            {
                fieldInfo.SetValue(obj, value);
                return;
            }

            Debugger.Break(); //this isn't supposed to be hit.
            throw new NotImplementedException();
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