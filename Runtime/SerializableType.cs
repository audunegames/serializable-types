using System;
using System.Collections.Generic;
using UnityEngine;

namespace Audune.Utils.Types
{
  /// <summary>
  /// Class that defines a type that can be serialized using the Unity serializer.
  /// </summary>
  [Serializable]
  public sealed class SerializableType : IEquatable<SerializableType>
  {
    // The fully qualified name of the type for serialization.
    [SerializeField]
    private string _typeName;


    /// <summary>
    /// Return the referenced type of the serialized type
    /// </summary>
    public Type type => Type.GetType(_typeName);


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="type">The referenced type of the serialized type.</param>
    public SerializableType(Type type)
    {
      _typeName = type.AssemblyQualifiedName;
    }


    #region Equatable implementation
    /// <summary>
    /// Return if the serializable type equals another object.
    /// </summary>
    /// <param name="obj">The other object to check equality against.</param>
    /// <returns>True if the serializable type equals another object.</returns>
    public override bool Equals(object obj)
    {
      return Equals(obj as SerializableType);
    }

    /// <summary>
    /// Return if the serializable type equals another serializable type.
    /// </summary>
    /// <param name="other">The other serialized tpye to check equality against.</param>
    /// <returns>True if the serializable type equals another serializable type.</returns>
    public bool Equals(SerializableType other)
    {
      return other is not null && _typeName == other._typeName;
    }

    /// <summary>
    /// Return the hash code of the serializable type.
    /// </summary>
    /// <returns>The hash code of the serialized type.</returns>
    public override int GetHashCode()
    {
      return HashCode.Combine(_typeName);
    }


    /// <summary>
    /// Return if the serializable type equals another serializable type.
    /// </summary>
    /// <param name="left">The left-hand side of the operator.</param>
    /// <param name="right">The right-hand side of the operator.</param>
    /// <returns>True if the operands are equal to each other.</returns>
    public static bool operator ==(SerializableType left, SerializableType right)
    {
      return EqualityComparer<SerializableType>.Default.Equals(left, right);
    }

    /// <summary>
    /// Return if the serializable type does not equal another serializable type.
    /// </summary>
    /// <param name="left">The left-hand side of the operator.</param>
    /// <param name="right">The right-hand side of the operator.</param>
    /// <returns>True if the operands are not equal to each other.</returns>
    public static bool operator !=(SerializableType left, SerializableType right)
    {
      return !(left == right);
    }
    #endregion

    #region Implicit operators
    /// <summary>
    /// Implicitly convert a type to a serializable type.
    /// </summary>
    /// <param name="type">The type to convert to a serializable type.</param>
    public static implicit operator SerializableType(Type type)
    {
      return new SerializableType(type);
    }

    /// <summary>
    /// Implicitly convert a serializable type to a type.
    /// </summary>
    /// <param name="reference">The serializable type to convert to a type.</param>
    public static implicit operator Type(SerializableType reference)
    {
      return reference.type;
    }
    #endregion
  }
}