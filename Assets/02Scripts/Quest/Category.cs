using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;

/// <summary>
/// This class represents the type of actions that fulfill the clear conditions of a quest during its progression.
/// </summary>
[CreateAssetMenu(menuName = "Category", fileName = "Category_")]
public class Category : ScriptableObject, IEquatable<Category>
{
    [SerializeField]
    private string id;
    [SerializeField]
    private string displayName;

    public string ID => id;
    public string DisplayName => displayName;

    #region Operator
    // you can (category.ID == "kill")
    // you can (category == "kill")
    public bool Equals(Category other)
    {
        if (other == null) return false;
        if (ReferenceEquals(other, this)) return true;
        if (GetType() != other.GetType()) return false;

        return id == other.ID;
    }

    public override int GetHashCode() => (ID, DisplayName).GetHashCode();

    public override bool Equals(object other) => Equals(other as Category);

    public static bool operator ==(Category lhs, string rhs)
    {
        if (lhs is null) return ReferenceEquals(rhs, null);
        return lhs.ID == rhs || lhs.DisplayName == rhs;
    }

    public static bool operator !=(Category lhs, string rhs) => !(lhs == rhs);
    #endregion
}
