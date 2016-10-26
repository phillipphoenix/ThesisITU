using UnityEngine;
using System.Collections;
using UnityEditor;

namespace DarkRoom
{
    [CustomEditor(typeof(PRS))]
    public class PRSEditor : Editor
    {
        public override void OnInspectorGUI()
        {

            DrawDefaultInspector();

            PRS prs = (PRS) target;

            GUILayout.Space(8);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Start position"))
            {
                prs.StartSeq(PRS.PRSType.Position);
            }
            if (GUILayout.Button("Stop position"))
            {
                prs.StopSeq(PRS.PRSType.Position);
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Start rotation"))
            {
                prs.StartSeq(PRS.PRSType.Rotation);
            }
            if (GUILayout.Button("Stop rotation"))
            {
                prs.StopSeq(PRS.PRSType.Rotation);
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Start scale"))
            {
                prs.StartSeq(PRS.PRSType.Scale);
            }
            if (GUILayout.Button("Stop scale"))
            {
                prs.StopSeq(PRS.PRSType.Scale);
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(8);


            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Start all"))
            {
                prs.StartSeq(PRS.PRSType.Position);
                prs.StartSeq(PRS.PRSType.Rotation);
                prs.StartSeq(PRS.PRSType.Scale);
            }
            if (GUILayout.Button("Stop all"))
            {
                prs.StopSeq(PRS.PRSType.Position);
                prs.StopSeq(PRS.PRSType.Rotation);
                prs.StopSeq(PRS.PRSType.Scale);
            }
            EditorGUILayout.EndHorizontal();

        }
    }

}
