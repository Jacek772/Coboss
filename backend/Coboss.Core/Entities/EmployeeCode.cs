using Coboss.Core.Entities.Abstracts;

namespace Coboss.Core.Entities
{
    public class EmployeeCode : BaseEntitiy
    {
        public int CodeNumber { get; set; } = default!;
        public int CodeLength { get; set; } = default!;
        public string Code => GetCode();

        private string GetCode()
        {
            string code = CodeNumber.ToString();
            int complementLength = CodeLength - code.Length;
            for(int i = 0; i < complementLength; i++)
            {
                code = $"0{code}";
            }
            return code;
        }
    }
}
