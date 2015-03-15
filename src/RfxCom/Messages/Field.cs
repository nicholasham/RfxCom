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

        public bool IsEnabled(byte @byte)
        {
            return (@byte & Value) != 0;
        }

        public override string ToString()
        {
            return string.Format("Id:'{0}', Description: '{1}' ", Value, Description);
        }
    }

    public abstract class Field<T> :Field where T : Field
    {
        public static bool TryParse(byte value, out T field) 
        {
            var fields = List().Cast<Field<T>>();

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

        public static IEnumerable<T> List()
        {
            var fields = (from field in typeof(T).GetRuntimeFields()
                where field.FieldType == typeof(T)
                select field.GetValue(null)).Cast<T>();

            return fields;
        }

        public static IEnumerable<T> ListEnabled(byte value)
        {
            return List().Where(x=> x.IsEnabled(value));
        }

      
    }
}