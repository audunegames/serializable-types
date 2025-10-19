using System;

namespace Audune.Utils.Types
{
  /// <summary>
  /// Enum that defines options for hiding types.
  /// </summary>
  [Flags]
  public enum HiddenTypes
  {
    /// <summary>
    /// No hiding of types.
    /// </summary>
    None = 0,

    /// <summary>
    /// Hide abstract classs.
    /// </summary>
    AbstractClasses = 1 << 0,
  }
}
