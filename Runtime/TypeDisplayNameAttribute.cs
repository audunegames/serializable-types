using System;

namespace Audune.Utils.Types
{
  /// <summary>
  /// Attribute that defines a display name for the type of a class.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public class TypeDisplayNameAttribute : Attribute
  {
    /// <summary>
    /// The name to display for the serialized type.
    /// </summary>
    public string name { get; set; }


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name">The name to display for the serialized type.</param>
    public TypeDisplayNameAttribute(string name)
    {
      this.name = name;
    }
  }
}