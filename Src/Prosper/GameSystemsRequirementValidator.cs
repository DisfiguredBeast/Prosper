using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Prosper.Core;

namespace Prosper
{
    #region Custom Exceptions

    [Serializable]
    public class GameSystemRequirementTypeException : Exception
    {
        private static string F = $"Property \"{{0}}.{{1}}\" has \"{nameof(InjectAttribute)}\" but property type is not of type \"{nameof(ComponentList<IGameComponent>)}\".";
        internal GameSystemRequirementTypeException(PropertyInfo info) : base(F.FormatWith(info.DeclaringType.FullName, info.Name)) { }
    }

    [Serializable]
    public class GameSystemRequirementCtorException : Exception
    {
        private static string F = $"Property \"{{0}}.{{1}}\" has \"{nameof(InjectAttribute)}\" but type does not have a parameterless constructor.";
        internal GameSystemRequirementCtorException(PropertyInfo info) : base(F.FormatWith(info.PropertyType.FullName)) { }
    }

    [Serializable]
    public class GameSystemRequirementSetException : Exception
    {
        private static string F = "Property {0}.{1} must have a setter.";
        internal GameSystemRequirementSetException(PropertyInfo info) : base(F.FormatWith(info.DeclaringType.FullName, info.Name)) { }
    }

    #endregion

    public static class GameSystemsRequirementValidator
    {
        public static void Run()
        {
            Logger.Log(SeverityLevel.Info, "Running Systems Requirement Validation");

            var assembliesToIgnore = new [] { "mscorlib", "System", "OpenTK" };
            var assemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                .Where(a => !string.IsNullOrWhiteSpace(a?.FullName) && assembliesToIgnore.All(i => !a.FullName.StartsWith(i)));

            foreach (var name in assemblyNames)
            {
                Logger.Log(SeverityLevel.Info, $"{nameof(GameSystemsRequirementValidator)} - Validating assembly {name}.");

                var assembly = LoadAssembly(name);
                var types = LoadTypes(assembly);
                var systems = types.Where(t => t != typeof(IGameSystem) && typeof(IGameSystem).IsAssignableFrom(t));
                systems.Do(ValidateSystem);
            }

            Logger.Log(SeverityLevel.Info, "Systems Requirement Validation Successful");
        }

        private static void ValidateSystem(Type type)
        {
            Logger.Log(SeverityLevel.Info, $"Validating Type \"{type.FullName}\".");

            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;
            var properties = type.GetProperties(flags);
            properties.Do(ValidateProperty);
        }

        private static void ValidateProperty(PropertyInfo property)
        {
            Logger.Log(SeverityLevel.Info, $"Validating Property \"{property.Name}\" with type \"{property.PropertyType.Name}\".");

            var reqAttr = property.GetCustomAttribute<InjectAttribute>();
            if (reqAttr == null)
                return;

            if (!property.PropertyType.IsGenericType || property.PropertyType.GetGenericTypeDefinition() != typeof(ComponentList<>))
                throw new GameSystemRequirementTypeException(property);

            if (property.PropertyType.GetConstructor(Type.EmptyTypes) == null)
                throw new GameSystemRequirementCtorException(property);

            if (property.GetSetMethod(true) == null)
                throw new GameSystemRequirementSetException(property);
        }

        private static Assembly LoadAssembly(AssemblyName name)
        {
            return Get(() => Assembly.Load(name), $"Could not load assembly \"{name.FullName}\".");
        }

        private static Type[] LoadTypes(Assembly assembly)
        {
            return Get(() => assembly.GetTypes(), $"Could not load types from assembly \"{assembly.FullName}\".");
        }

        private static T Get<T>(Func<T> func, string msg)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                Logger.Log(SeverityLevel.Fatal, msg, ex);
                throw;
            }
        }
    }
}
