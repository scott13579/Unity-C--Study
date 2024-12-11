using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LayoutComp))]
public class LayoutCompEditor : Editor
{
    private SerializedProperty data1Property;
    private SerializedProperty data2Property;
    private SerializedProperty data3Property;
    private SerializedProperty data4Property;
    private SerializedProperty data5Property;
    private SerializedProperty data6Property;
    private SerializedProperty data7Property;
    private SerializedProperty data8Property;
    private SerializedProperty data9Property;
    
    private bool foldState = false;
    
    private void OnEnable()
    {
        data1Property = serializedObject.FindProperty("data1");
        data2Property = serializedObject.FindProperty("data2");
        data3Property = serializedObject.FindProperty("data3");
        data4Property = serializedObject.FindProperty("data4");
        data5Property = serializedObject.FindProperty("data5");
        data6Property = serializedObject.FindProperty("data6");
        data7Property = serializedObject.FindProperty("data7");
        data8Property = serializedObject.FindProperty("data8");
        data9Property = serializedObject.FindProperty("data9");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        foldState = EditorGUILayout.Foldout(foldState, "Layout");

        if (foldState)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.LabelField("Life");
         
            EditorGUI.indentLevel++;
            data1Property.stringValue = EditorGUILayout.TextField("data1Property", data1Property.stringValue);
            data2Property.stringValue = EditorGUILayout.TextField("Data 2", data2Property.stringValue);
            EditorGUILayout.PropertyField(data3Property);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(data4Property);
            EditorGUI.indentLevel--;
            EditorGUILayout.PropertyField(data5Property);
            EditorGUILayout.PropertyField(data6Property);
            EditorGUILayout.PropertyField(data7Property);
            EditorGUILayout.PropertyField(data8Property);
            EditorGUILayout.PropertyField(data9Property);
            
            EditorGUI.indentLevel--;
            
            EditorGUI.indentLevel--;

            if (GUILayout.Button("Check Scope"))
            {
                
            }
        }

        serializedObject.ApplyModifiedProperties();

        // base.OnInspectorGUI();

    }
}
