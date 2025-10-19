using System;
using UnityEngine;

namespace Audune.Utils.Types
{
  /// <summary>
  /// Attribute that defines options for drawing a serializable type.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
  public class SerializableTypeOptionsAttribute : PropertyAttribute
  {
    /// <summary>
    /// The type of which the child types are drawn.
    /// </summary>
    public Type baseType { get; set; }

    /// <summary>
    /// The display string options for the type.
    /// </summary>
    public TypeDisplayOptions displayOptions { get; set; }


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="baseType">The type of which the child types are drawn.</param>
    /// <param name="displayOptions">The display string options for the type.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="baseType"/> is set to null.</exception>
    public SerializableTypeOptionsAttribute(Type baseType, TypeDisplayOptions displayOptions = TypeDisplayOptions.None)
    {
      if (baseType == null) 
        throw new ArgumentNullException(nameof(baseType));

      this.baseType = baseType;
      this.displayOptions = displayOptions;
    }
  }
}