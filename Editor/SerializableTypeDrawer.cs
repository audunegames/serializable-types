using Audune.Utils.UnityEditor.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Audune.Utils.Types.Editor
{
  /// <summary>
  /// Class that defines a property drawer for serializable types.
  /// </summary>
  [CustomPropertyDrawer(typeof(SerializableType))]
  public class SerializableTypeDrawer : PropertyDrawer
  {
    // The options attribute of the property drawer.
    private SerializableTypeOptionsAttribute _attribute;

    // The types of the property drawer.
    private List<Type> _types;

    // Specify if the serialized type has child types.
    private bool _hasChildTypes = false;

    
    /// <summary>
    /// Draw the GUI for a property.
    /// </summary>
    /// <param name="rect">The rect to draw the property in.</param>
    /// <param name="property">The property to draw the GUI for.</param>
    /// <param name="label">The label for the property.</param>
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
      using var scope = new EditorGUI.PropertyScope(rect, label, property);

      _attribute ??= fieldInfo.GetCustomAttribute<SerializableTypeOptionsAttribute>();
      if (_attribute == null)
      {
        EditorGUI.HelpBox(rect, $"{typeof(SerializableType)} must have a {typeof(SerializableTypeOptionsAttribute)} attribute", MessageType.None);
        return;
      }

      if (_types == null)
      {
        _types = TypeExtensions.GetChildTypes(_attribute.baseType).ToList();
        if (_types.Count > 0)
          _hasChildTypes = true;
        else
          _types = new List<Type>() { _attribute.baseType };
      }
      
      var typeName = property.FindPropertyRelative("_typeName");
      if (string.IsNullOrEmpty(typeName.stringValue))
        typeName.stringValue =  _types[0].AssemblyQualifiedName;

      label = EditorGUI.BeginProperty(rect, label, property);

      using (new EditorGUI.DisabledScope(!_hasChildTypes))
      {
        var newType = EditorGUIExtensions.ItemPopup(rect, label, _types, type => type.AssemblyQualifiedName == typeName.stringValue, type => new GUIContent(type.ToDisplayString(_attribute?.displayOptions ?? TypeDisplayOptions.None)));
        typeName.stringValue = newType?.AssemblyQualifiedName;
      }

      EditorGUI.EndProperty();
    }

    /// <summary>
    /// Return the height for a property.
    /// </summary>
    /// <param name="property">The property to return the height for.</param>
    /// <param name="label">The label for the property.</param>
    /// <returns>The height of the specified property.</returns>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
      return EditorGUIUtility.singleLineHeight;
    }
  }
}