using System;

namespace Audune.Utils.Types
{
  /// <summary>
  /// Enum that defines options for displaying the name of a type.
  /// </summary>
  [Flags]
  public enum TypeDisplayOptions
  {
    /// <summary>
    /// Default type diaplay options.
    /// </summary>
    None = 0,

    /// <summary>
    /// Don't use <see cref="ObjectNames.NicifyVariableName"/> when displaying type names.
    /// </summary>    
    DontUseNicifiedNames = 1 << 0,

    /// <summary>
    /// Don't show the namespace of a type.
    /// </summary>
    DontShowNamespace = 1 << 1,

    /// <summary>
    /// Ignore display names specified with the <see cref="TypeDisplayNameAttribute"/> attribute.
    /// </summary>
    IgnoreDisplayNames = 1 << 2,
  }
}
