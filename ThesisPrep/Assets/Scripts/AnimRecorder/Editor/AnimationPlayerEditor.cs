using UnityEngine;
using System.Collections;
using UnityEditor;
using Newtonsoft.Json;
using System.IO;

namespace AnimRecorder.EditorTools {

    [CustomEditor(typeof(AnimationPlayer))]
    public class AnimationPlayerEditor : Editor {

        public override void OnInspectorGUI() {

            DrawPropertiesExcluding(serializedObject, "m_Script");

            AnimationPlayer animPlayer = (AnimationPlayer)target;

            if (GUILayout.Button("Load animation")) {
                string path = EditorUtility.OpenFilePanelWithFilters("Open animation file", "Assets", new string[] { "JSON Animation", "json" });
                LoadAnimation(animPlayer, path);
            }
        }

        private void LoadAnimation(AnimationPlayer animPlayer, string filePath) {
            AnimRecordingFile file = JsonConvert.DeserializeObject<AnimRecordingFile>(File.ReadAllText(filePath));
            animPlayer.SetAnimation(file.AnimationDuration, file.Recordings);
        }

    }
}