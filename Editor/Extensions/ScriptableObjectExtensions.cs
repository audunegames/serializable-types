using System;
using UnityEngine;

namespace Audune.Utils.Types.Editor
{
  /// <summary>
  /// Class that defines extension methods for scriptable objects in the editor.
  /// </summary>
  public static class ScriptableObjectExtensions
  {
    #region Creating scriptable objects with fancy names based on theyr type
    /// <summary>
    /// Create a scriptable object with its name set to the display string of the type.
    /// </summary>
    /// <param name="type">The type for the isntance of the scriptable object.</param>
    /// <returns>A <see cref="ScriptableObject"/> instance of the specified type with its name set to the display string of the type.</returns>
    public static ScriptableObject CreateInstance(Type type)
    {
      var obj = ScriptableObject.CreateInstance(type);
      obj.name = type.ToDisplayString(TypeDisplayOptions.DontShowNamespace);
      return obj;
    }

    /// <summary>
    /// Create a scriptable object with its name set to the display string of the type.
    /// </summary>
    /// <typeparam name="TObject">The type for the isntance of the scriptable object.</typeparam>
    /// <returns>A <see cref="ScriptableObject"/> instance of the specified type with its name set to the display string of the type.</returns>
    public static TObject CreateInstance<TObject>() where TObject : ScriptableObject
    {
      return CreateInstance(typeof(TObject)) as TObject;
    }
    #endregion
  }
}