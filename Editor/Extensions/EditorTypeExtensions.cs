using Audune.Utils.UnityEditor.Editor;
using System;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Audune.Utils.Types.Editor
{
  /// <summary>
  /// Class that defines extension methods for types in the editor.
  /// </summary>
  public static class EditorTypeExtensions
  {
    #region Getting the display name of types
    /// <summary>
    /// Return a display string for a type.
    /// </summary>
    /// <param name="baseType">The type to return the display string for.</param>
    /// <param name="displayOptions">The options for getting the display string of a type.</param>
    /// <returns>The display string for the specified type.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="baseType"/> is set to null.</exception>
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
        if (!displayOptions.HasFlag(TypeDisplayOptions.DontShowNamespace) && !string.IsNullOrEmpty(baseType.Namespace))
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
    /// <summary>
    /// Return a generic menu of child types of the specified base type.
    /// </summary>
    /// <param name="baseType">The type to return the generic menu for.</param>
    /// <param name="displayOptions">The options for returning the display string.</param>
    /// <param name="selectedType">The child type that is currently selected.</param>
    /// <param name="onClicked">A callback that is invoked when an item in the generic menu is clicked.</param>
    /// <returns>A <see cref="UnityEditor.GenericMenu"/> containing child types for the specified type.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="baseType"/> is set to null.</exception>
    public static GenericMenu CreateGenericMenuForChildTypes(this Type baseType, TypeDisplayOptions displayOptions, Type selectedType, Action<Type> onClicked)
    {
      if (baseType == null)
        throw new ArgumentNullException(nameof(baseType));

      var menu = new GenericMenu();
      foreach (var type in TypeExtensions.GetChildTypes(baseType))
        menu.AddItem(new GUIContent(ToDisplayString(type, displayOptions)), type == selectedType, () => onClicked(type));
      return menu;
    }

    /// <summary>
    /// Return a generic menu of child types based on the specified attribute.
    /// </summary>
    /// <param name="attribute">The attribute to return the generic menu for.</param>
    /// <param name="selectedType">The child type that is currently selected.</param>
    /// <param name="onClicked">A callback that is invoked when an item in the generic menu is clicked.</param>
    /// <returns>A <see cref="UnityEditor.GenericMenu"/> containing child types for the specified sttribute.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="attribute"/> is set to null.</exception>
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
    /// <summary>
    /// Create a builder for the child types of the specified object type.
    /// </summary>
    /// <typeparam name="TType">The type to get the child types for.</typeparam>
    /// <param name="typeDisplayOptions">The options for getting the display string of a type.</param>
    /// <param name="itemSelector">A function that returns the item to add to the <see cref="UnityEditorInternal.ReorderableList"/> for the specified type argument.</param>
    /// <returns>A <see cref="ReorderableDropdownListBuilder{TItem}"/> containing child types for the specified type</returns>
    public static ReorderableDropdownListBuilder<Type> CreateForObjectTypes<TType>(TypeDisplayOptions typeDisplayOptions, Func<TType, object> itemSelector = null)
    {
      itemSelector ??= obj => obj;

      return new ReorderableDropdownListBuilder<Type>()
        .SetDropdownCreator((list, buttonRect, addCallback) => typeof(TType).CreateGenericMenuForChildTypes(typeDisplayOptions, null, addCallback).DropDown(buttonRect))
        .SetDropdownAddCallback((element, index, type) => element.boxedValue = itemSelector((TType)Activator.CreateInstance(type)));
    }

    /// <summary>
    /// Create a builder for the child types of the specified scriptable object type.
    /// </summary>
    /// <typeparam name="TType">The type to get the child types for.</typeparam>
    /// <param name="typeDisplayOptions">The options for getting the display string of a type.</param>
    /// <param name="itemSelector">A function that returns the item to add to the <see cref="UnityEditorInternal.ReorderableList"/> for the specified type argument.</param>
    /// <returns>A <see cref="ReorderableDropdownListBuilder{TItem}"/> containing child types for the specified type</returns>
    public static ReorderableDropdownListBuilder<Type> CreateForScriptableObjectTypes<TType>(TypeDisplayOptions typeDisplayOptions, Func<TType, object> itemSelector = null) where TType : ScriptableObject
    {
      itemSelector ??= obj => obj;

      return new ReorderableDropdownListBuilder<Type>()
        .SetDropdownCreator((list, buttonRect, addCallback) => CreateGenericMenuForChildTypes(typeof(TType), typeDisplayOptions, null, addCallback).DropDown(buttonRect))
        .SetDropdownAddCallback((element, index, type) => element.boxedValue = itemSelector((TType)ScriptableObjectExtensions.CreateInstance(type)));
    }
    #endregion
  }
}