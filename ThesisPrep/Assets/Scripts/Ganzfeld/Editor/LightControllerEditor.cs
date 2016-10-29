using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Ganzfeld
{
    [CustomEditor(typeof(LightController))]
    public class LightControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            LightController lc = (LightController)target;

            GUILayout.Space(8);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Start animation"))
            {
                lc.StartAnimation();
            }
            if (GUILayout.Button("Stop animation"))
            {
                lc.StopAnimation();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}