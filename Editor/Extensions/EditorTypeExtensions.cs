using System;
using UnityEditor;
using UnityEngine;
using System.Text;
using System.Reflection;
using Audune.Utils.UnityEditor.Editor;

namespace Audune.Utils.Types.Editor
{
  // Class that defines extension methods for types in the editor
  public static class EditorTypeExtensions
  {
    #region Getting the display name of types
    // Return a display string for a type
    public static string ToDisplayString(this Type baseType, TypeDisplayOptions displayOptions = TypeDisplayOptions.None)
    {
      if (baseType == null)
        throw new ArgumentNullException(nameof(baseType));

      var displayNameAttribute = baseType.GetCustomAttribute<TypeDisplayNameAttribute>();
      if (!displayOptions.HasFlag(TypeDisplayOptions.IgnoreDisplayNames) && displayNameAttribute != null)
      {
        return displayNameAttribute.name;
      }
      else if (!displayOptions.HasFlag(TypeDisplayOptions.DontUseNicifiedNames))
      {
        var builder = new StringBuilder();
        builder.Append(ObjectNames.NicifyVariableName(baseType.Name));
        if (!displayOptions.HasFlag(TypeDisplayOptions.DontShowNamespace))
          builder.Append($" ({baseType.Namespace})");
        return builder.ToString();
      }
      else
      {
        if (!displayOptions.HasFlag(TypeDisplayOptions.DontShowNamespace))
          return baseType.FullName;
        else
          return baseType.Name;
      }
      
    }
    #endregion

    #region Creating generic menus for child types
    // Return a generic menu of child types of the specified base type
    public static GenericMenu CreateGenericMenuForChildTypes(this Type baseType, TypeDisplayOptions displayOptions, Type selectedType, Action<Type> onClicked)
    {
      if (baseType == null)
        throw new ArgumentNullException(nameof(baseType));

      var menu = new GenericMenu();
      foreach (var type in TypeExtensions.GetChildTypes(baseType))
        menu.AddItem(new GUIContent(ToDisplayString(type, displayOptions)), type == selectedType, () => onClicked(type));
      return menu;
    }

    // Return a generic menu of child types based on the specified attribute
    public static GenericMenu CreateGenericMenuForChildTypes(this SerializableTypeOptionsAttribute attribute, Type selectedType, Action<Type> onClicked)
    {
      if (attribute == null)
        throw new ArgumentNullException(nameof(attribute));

      var menu = new GenericMenu();
      foreach (var type in TypeExtensions.GetChildTypes(attribute.baseType))
        menu.AddItem(new GUIContent(ToDisplayString(type, attribute.displayOptions)), type == selectedType, () => onClicked(type));
      return menu;
    }
    #endregion

    #region Creating reorderable dropdown lists for types
    // Create a builder for the child types of the specified object type 
    public static ReorderableDropdownListBuilder<Type> CreateForObjectTypes<TType>(TypeDisplayOptions typeDisplayOptions, Func<TType, object> itemSelector = null)
    {
      itemSelector ??= obj => obj;

      return new ReorderableDropdownListBuilder<Type>()
        .SetDropdownCreator((list, buttonRect, addCallback) => typeof(TType).CreateGenericMenuForChildTypes(typeDisplayOptions, null, addCallback).DropDown(buttonRect))
        .SetDropdownAddCallback((element, index, type) => element.boxedValue = itemSelector((TType)Activator.CreateInstance(type)));
    }

    // Create a builder for the child types of the specified scriptable object type 
    public static ReorderableDropdownListBuilder<Type> CreateForScriptableObjectTypes<TType>(TypeDisplayOptions typeDisplayOptions, Func<TType, object> itemSelector = null) where TType : ScriptableObject
    {
      itemSelector ??= obj => obj;

      return new ReorderableDropdownListBuilder<Type>()
        .SetDropdownCreator(((list, buttonRect, addCallback) => CreateGenericMenuForChildTypes(typeof(TType), typeDisplayOptions, null, addCallback).DropDown(buttonRect)))
        .SetDropdownAddCallback((element, index, type) => element.boxedValue = itemSelector((TType)ScriptableObjectExtensions.CreateInstance(type)));
    }
    #endregion
  }
}