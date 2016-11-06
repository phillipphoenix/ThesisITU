using System;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using System.Linq;
using Newtonsoft.Json;

namespace AnimRecorder.EditorTools
{
    [CustomEditor(typeof(AnimationRecorder))]
    public class AnimationRecorderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawPropertiesExcluding(serializedObject, "m_Script");

            AnimationRecorder animRec = (AnimationRecorder) target;

            EditorGUILayout.LabelField("Recording progress", EditorStyles.boldLabel);
            if (!animRec.RecordOnStart && !animRec.HoldsRecording && !animRec.IsRecording) {
                if (GUILayout.Button("Start recording")) {
                    animRec.StartRecording();
                }
            }
            else if (!animRec.HoldsRecording || animRec.IsRecording)
            {
                Rect r = EditorGUILayout.BeginVertical();
                EditorGUI.ProgressBar(r, animRec.RecordingProgress, "Recording progress");
                GUILayout.Space(20);
                EditorGUILayout.EndVertical();
                Repaint();
            }
            else
            {
                EditorGUILayout.LabelField("Recording done!");
                if (GUILayout.Button("Save recording"))
                {
                    SaveRecordingToFile(animRec);
                }
            }
        }

        private void SaveRecordingToFile(AnimationRecorder animRec)
        {
            AnimRecordingFile file = new AnimRecordingFile(animRec.gameObject.name, animRec.RecordingDuration, animRec.Recordings);
            string json = JsonConvert.SerializeObject(file, Formatting.Indented);
            string path = EditorUtility.SaveFilePanelInProject("Save animation to json file",
                ToCamelCase(file.GameObjectName) + "-animRecording", "json",
                "Please select the location for the animation file.");
            File.WriteAllText(path, json);
        }

        private static string ToCamelCase(string input)
        {
            // Remove all duplicate spaces before spliting.
            Regex regex = new Regex("[ ]{2,}");
            input = regex.Replace(input, " ");
            // Split around spaces.
            string[] words = input.Split(' ');
            // Put all words together with first char in upper case.
            return words.Select(w => FirstCharToUpper(w)).Aggregate((all, cur) => all + cur);
        }

        private static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("Input needs to be a non-empty string.");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}