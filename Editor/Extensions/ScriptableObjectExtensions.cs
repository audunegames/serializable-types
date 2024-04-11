using System;
using UnityEngine;

namespace Audune.Utils.Types.Editor
{
  // Class that defines extension methods for types in the editor
  public static class ScriptableObjectExtensions
  {
    #region Creating scriptable objects with fancy names based on theyr type
    // Create a scriptable object with its name set to the nice type name
    public static ScriptableObject CreateInstance(Type type)
    {
      var obj = ScriptableObject.CreateInstance(type);
      obj.name = type.ToDisplayString(TypeDisplayOptions.DontShowNamespace);
      return obj;
    }

    // Create a scriptable object with its name set to the nice type name
    public static TObject CreateInstance<TObject>() where TObject : ScriptableObject
    {
      return CreateInstance(typeof(TObject)) as TObject;
    }
    #endregion
  }
}