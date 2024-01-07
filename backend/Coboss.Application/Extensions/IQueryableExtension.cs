using Coboss.Core.Entities;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace Coboss.Application.Extensions
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> ApplaySort<T>(this IQueryable<T> values, string orderByQueryString)
        {
            IEnumerable<string> orderParams = orderByQueryString
                .Trim()
                .Split(',')
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrEmpty(x) && !string.IsNullOrWhiteSpace(x));

            if(!orderParams.Any())
            {
                return values;
            }

            PropertyInfo[] propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string orderParam in orderParams)
            {
                string propertyName = orderParam.Split(":").FirstOrDefault()?.ToLower();
                if (string.IsNullOrEmpty(propertyName))
                {
                    continue;
                }

                PropertyInfo propertyInfo = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));
                if (propertyInfo == null)
                {
                    continue;
                }

                string direction = orderParam.Split(":").LastOrDefault();
                string sortingOrder = direction == "desc" ? "descending" : "ascending";

                stringBuilder.Append($"{propertyInfo.Name} {sortingOrder}, ");
            }

            string orderByString = stringBuilder.ToString().TrimEnd(',', ' ');
            if (!string.IsNullOrEmpty(orderByString))
            {
                return values.OrderBy(orderByString);
            }

            return values;
        }
    }
}
