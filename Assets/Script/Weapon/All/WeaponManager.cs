using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>
/// Showing an array with Enum as keys in the property inspector. (Supported children)
/// </summary>
public class EnumIndexAttribute : PropertyAttribute
{
    private string[] _names;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumType"></param>
    public EnumIndexAttribute(Type enumType) => _names = Enum.GetNames(enumType);

#if UNITY_EDITOR
    /// <summary>
    /// Show inspector
    /// </summary>
    [CustomPropertyDrawer(typeof(EnumIndexAttribute))]
    private class Drawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            var names = ((EnumIndexAttribute)attribute)._names;
            // propertyPath returns something like hogehoge.Array.data[0]
            // so get the index from there.
            var index = int.Parse(property.propertyPath.Split('[', ']').Where(c => !string.IsNullOrEmpty(c)).Last());
            if (index < names.Length) label.text = names[index];
            EditorGUI.PropertyField(rect, property, label, includeChildren: true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, includeChildren: true);
        }
    }
#endif
}

public class WeaponManager : MonoBehaviour
{
    /// <summary>
    /// 武器ID列挙体
    /// </summary>
    public enum WeaponID
    {
        None,
        Kogetu,
        Gun,
        Asteroid,
        Shield
    }

    // EnumIndex属性で表示したいEnumを設定することでInspector上に反映されます
    [SerializeField, EnumIndex(typeof(WeaponID))]
    private Damage[] damage;

    [System.Serializable]
    /// <summary>
    /// ダメージクラス
    /// </summary>
    public class Damage
    {
        [SerializeField]
        private int damage_num;
    }
}
