using System;
using System.Collections.Generic;
using System.Linq;

namespace Audune.Utils.Types
{
  /// <summary>
  /// Class that defines extension methods for types.
  /// </summary>
  public static class TypeExtensions
  {
    #region Getting child types
    /// <summary>
    /// Return an enumerable of the child types of a base type.
    /// </summary>
    /// <param name="baseType">The base type to get child types of.</param>
    /// <returns>An <c>IEnumerable</c> of the child types of the specified base type.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="baseType"/> is set to null.</exception>
    public static IEnumerable<Type> GetChildTypes(this Type baseType, HiddenTypes hiddenTypes = HiddenTypes.None)
    {
      if (baseType == null)
        throw new ArgumentNullException(nameof(baseType));

      return AppDomain.CurrentDomain.GetAssemblies()
       .SelectMany(assembly => assembly.GetTypes())
       .Where(type => type.IsSubclassOfGeneric(baseType))
       .Where(type => !hiddenTypes.HasFlag(HiddenTypes.AbstractClasses) || !type.IsAbstract);
    }

    /// <summary>
    /// Return if the specified type is a subclass of a possibly generic base type.
    /// </summary>
    /// <param name="type">The type to check against.</param>
    /// <param name="genericBaseType">The possible generic base type to check.</param>
    /// <returns>Whether the specified type is a subclass of the possibly generic base type.</returns>
    public static bool IsSubclassOfGeneric(this Type type, Type genericBaseType)
    {
      if (type == null)
        throw new ArgumentNullException(nameof(type));
      if (genericBaseType == null)
        return false;

      if (!genericBaseType.IsGenericTypeDefinition)
        return type.IsSubclassOf(genericBaseType);

      while (type != null && type != typeof(object))
      {
        var current = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
        if (current == genericBaseType)
          return true;
        type = type.BaseType;
      }

      return false;
    }
    #endregion
  }
}