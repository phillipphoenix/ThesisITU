using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AnimRecorder {

    public class AnimationPlayer : MonoBehaviour {

        public bool IsPlaying { get; private set; }

        [SerializeField]
        private bool _playOnStart;
        
        [SerializeField]
        private List<Transform> _trackedTransforms;
        [SerializeField]
        private float _animationTime;
        [SerializeField]
        private List<AnimRecording> _recordings = new List<AnimRecording>();

        private int _animationIndex = 0;

        // Use this for initialization
        void Start() {
            if (_playOnStart) {
                StartAnimation();
            }
        }

        public void StartAnimation() {
            if (!IsPlaying && _trackedTransforms != null && _trackedTransforms.Count > 0 && _recordings != null && _recordings.Count == _trackedTransforms.Count) {
                IsPlaying = true;
                InvokeRepeating("NextAnimation", 0f, _animationTime / _recordings[0].AnimRecordingItems.Count);
            }
        }

        private void NextAnimation() {
            for (int i = 0; i < _trackedTransforms.Count; i++) {
                _trackedTransforms[i].position = _recordings[i].GetRecordingPosition(_animationIndex);
                _trackedTransforms[i].eulerAngles = _recordings[i].GetRecordingRotation(_animationIndex);
            }
            _animationIndex++;
            if (_animationIndex >= _recordings[0].AnimRecordingItems.Count) {
                StopAnimation();
            }
        }

        public void StopAnimation() {
            CancelInvoke("NextAnimation");
            _animationIndex = 0;
            IsPlaying = false;
        }

        public void SetAnimation(float animationDuration, List<AnimRecording> recordings) {
            _animationTime = animationDuration;
            _recordings = recordings;
        }

    }
}