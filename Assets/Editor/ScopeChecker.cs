using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ScopeChecker : EditorWindow
{
    private string _text;
    
    [MenuItem("Window/Scope Checker")]
    public static void ShowWindow()
    {
        GetWindow<ScopeChecker>("Scope Checker");
    }

    private void OnGUI()
    {
        _text = EditorGUILayout.TextArea( _text,GUILayout.Height(300));

        if (GUILayout.Button("Check Scope"))
        {
            if (AreBracketsBalanced(_text))
            {
                EditorUtility.DisplayDialog("Scope Checker", "Scope Check Success", "OK");
            }
            else
            {
                EditorUtility.DisplayDialog("Scope Checker", "Scope Check Failed", "OK");
            }
        }
    }
    
    public bool AreBracketsBalanced(string expression)
    {
        Stack<char> stack = new Stack<char>();

        foreach (char c in expression)
        {
            if (c == '(' || c == '[' || c == '{')
            {
                stack.Push(c);
            }
            else if (c == ')' || c == ']' || c == '}')
            {
                if (stack.Count == 0)
                    return false;

                char top = stack.Pop();
                if ((c == ')' && top != '(') ||
                    (c == ']' && top != '[') ||
                    (c == '}' && top != '{'))
                {
                    return false;
                }
            }
        }

        return stack.Count == 0;
    }
}
