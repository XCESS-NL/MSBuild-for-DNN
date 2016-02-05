using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dnn.MsBuild.Tasks.Extensions
{
    internal static class TypeExtensionMethods
    {
        public static bool HasAttribute<TAttribute>(this Type source)
            where TAttribute : Attribute
        {
            return source.GetCustomAttribute<TAttribute>() != null;
        }

        public static TAttribute GetCustomAttribute<TAttribute>(this IEnumerable<Type> source)
            where TAttribute : Attribute
        {
            var type = source.FirstOrDefault(arg => arg.HasAttribute<TAttribute>());
            return type?.GetCustomAttribute<TAttribute>();
        }
    }
}
