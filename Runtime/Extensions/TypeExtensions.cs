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
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<Type> GetChildTypes(this Type baseType, HiddenTypes hiddenTypes = HiddenTypes.None)
    {
      if (baseType == null)
        throw new ArgumentNullException(nameof(baseType));

      return AppDomain.CurrentDomain.GetAssemblies()
       .SelectMany(assembly => assembly.GetTypes())
       .Where(type => type.IsSubclassOf(baseType))
       .Where(type => !hiddenTypes.HasFlag(HiddenTypes.AbstractClasses) || !type.IsAbstract);
    }
    #endregion
  }
}