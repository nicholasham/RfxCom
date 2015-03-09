using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RfxCom.Messages
{
    public abstract class Field
    {
        protected Field(byte value, string description)
        {
            Description = description;
            Value = value;
        }

        public string Description { get; private set; }
        public byte Value { get; private set; }

        public static implicit operator byte(Field field)
        {
            return field.Value;
        }

        public override string ToString()
        {
            return string.Format("Id:'{0}', Description: '{1}' ", Value, Description);
        }
    }

    public abstract class Field<T> :Field where T : class
    {
        public static bool TryParse(byte value, out T field) 
        {
            var fields = GetAll().Cast<Field<T>>();

            field = fields.FirstOrDefault(x => x.Value == value) as T;

            return field != null;

        }

        public static T Parse(byte value, T defaultValue)
        {
            T result;

            if (!TryParse(value, out result))
            {
                result = defaultValue;
            }

            return result;
        }

        protected Field(byte value, string description) : base(value, description)
        {
        }

        public static IEnumerable<T> GetAll()
        {
            var fields = (from property in typeof(T).GetRuntimeFields()
                where property.FieldType == typeof(T)
                select property.GetValue(null)).Cast<T>();

            return fields;
        }
    }
}