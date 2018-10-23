using System;
using System.Linq;
using System.Reflection;

namespace Prosper.Library
{
    public static class TypeHelper
    {
        public static Type[] GetAssignableTypes(Type type)
        {
            var toSkipAssemblies = new[] { "mscorlib", "netstandard", "System.", "Microsoft.", "testhost", "Anonymously Hosted DynamicMethods Assembly", "Newtonsoft." };

            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !toSkipAssemblies.Any(s => a.FullName.StartsWith(s)))
                .ToArray();

            var types = assemblies.SelectMany(GetTypes).Where(t => !t.IsAbstract).ToArray();

            var assignables = type.IsGenericType || type.IsGenericTypeDefinition
                ? types.Where(t => IsAssignableToGenericType(t, type))
                : types.Where(t => type.IsAssignableFrom(t));
            return assignables.ToArray();
        }

        public static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            Type baseType = givenType.BaseType;
            if (baseType == null)
                return false;

            return IsAssignableToGenericType(baseType, genericType);
        }

        private static Type[] GetTypes(Assembly assembly)
        {
            try
            {
                return assembly?.GetTypes() ?? new Type[0];
            }
            catch
            {
                return new Type[0];
            }
        }
    }
}
