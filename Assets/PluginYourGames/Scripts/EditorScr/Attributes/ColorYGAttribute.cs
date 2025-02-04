using UnityEngine;

namespace YG
{
    public class ColorYGAttribute : PropertyAttribute
    {
        public ColorYGAttribute(float r, float g, float b)
        {
            color = new Color(r, g, b);
        }

        public ColorYGAttribute(float r, float g, float b, float a)
        {
            color = new Color(r, g, b, a);
        }

        public ColorYGAttribute()
        {
            color = new Color(1.3f, 1.3f, 1.0f);
        }

        public Color color { get; private set; }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ColorYGAttribute))]
    public class ColorYGAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ColorYGAttribute colorAttribute = (ColorYGAttribute)attribute;
            Color previousColor = GUI.color;
            GUI.color = colorAttribute.color;

            if (property.propertyType == SerializedPropertyType.Generic)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }

            GUI.color = previousColor;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }
#endif
}