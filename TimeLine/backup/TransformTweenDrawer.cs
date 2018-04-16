using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(TransformTweenBehaviour))]
public class TransformTweenDrawer : PropertyDrawer {

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 3 * (EditorGUIUtility.singleLineHeight);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty tweenPositionProp = property.FindPropertyRelative("tweenPosition");
        SerializedProperty tweenRotationProp = property.FindPropertyRelative("tweenRotation");

        Rect singleFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(singleFieldRect, tweenPositionProp);
        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, tweenRotationProp);
    }
}
