using Ulf;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AnimUI))]
public class AnimUIExtension : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AnimUI animator = (AnimUI)target;
        Handles.BeginGUI();

        if (GUILayout.Button("TestAnim"))
        {
            animator.StartAnim();
        }

        Handles.EndGUI();
    }

    
}