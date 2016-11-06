using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace AnimRecorder.EditorTools {

    [Serializable]
    internal class AnimRecordingFile {
        public string GameObjectName;
        public float AnimationDuration;
        public List<AnimRecording> Recordings;

        public AnimRecordingFile(string gameObjectName, float animationDuration, List<AnimRecording> recordings) {
            GameObjectName = gameObjectName;
            AnimationDuration = animationDuration;
            Recordings = recordings;
        }
    }
}