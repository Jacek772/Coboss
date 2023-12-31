using Coboss.Core.Entities.Abstracts;

namespace Coboss.Core.Entities
{
    public class GlobalSetting : BaseEntitiy
    {
        public enum GlobalSettingKey
        {
            Empty = 0,
            EmployeeCodeLength = 1
        }

        public enum GlobalSettingValueType
        {
            String = 0,
            Int = 1,
            Double = 2,
            DateTime = 3
        }

        public GlobalSettingKey Key { get; set; } = default!;
        public string Value { get; set; } = default!;
        public GlobalSettingValueType Type { get; set; } = default!;

        public string GetValueString()
        {
            if(Type != GlobalSettingValueType.String)
            {
                throw new Exception($"Invalid method. Type of this GlobalSetting is {Type}.");
            }
            return Value;
        }

        public int GetValueInt()
        {
            if (Type != GlobalSettingValueType.Int)
            {
                throw new Exception($"Invalid method. Type of this GlobalSetting is {Type}.");
            }
            return int.Parse(Value);
        }

        public double GetValueDouble()
        {
            if (Type != GlobalSettingValueType.Int)
            {
                throw new Exception($"Invalid method. Type of this GlobalSetting is {Type}.");
            }
            return double.Parse(Value);
        }

        public DateTime GetValueDateTime()
        {
            if (Type != GlobalSettingValueType.DateTime)
            {
                throw new Exception($"Invalid method. Type of this GlobalSetting is {Type}.");
            }
            return DateTime.Parse(Value);
        }

        public void SetValue<T>(T value)
        {
            if(value is int valInt)
            {
                Value = valInt.ToString();
                Type = GlobalSettingValueType.Int;
            }
            else if(value is double valDouble)
            {
                Value = valDouble.ToString();
                Type = GlobalSettingValueType.Double;
            }
            else if(value is string valString)
            {
                Value = valString;
                Type = GlobalSettingValueType.String;
            }
            else if(value is DateTime valDateTime)
            {
                Value = valDateTime.ToString();
                Type = GlobalSettingValueType.DateTime;
            }
            else
            {
                throw new Exception("Invalid type!");
            }
        }
    }
}