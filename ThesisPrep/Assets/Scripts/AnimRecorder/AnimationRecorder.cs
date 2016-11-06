using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AnimRecorder
{

    public class AnimationRecorder : MonoBehaviour
    {
        public float RecordingProgress
        {
            get { return (Time.time - _recordingStartTime) / _recordingTime; }
        }

        public bool IsRecording { get; private set; }

        public bool HoldsRecording {
            get
            {
                return _recordings.Count > 0 && _recordings[0].AnimRecordingItems.Count > 0;
            }
        }

        public float RecordingDuration { get { return _recordingTime; } }

        public List<AnimRecording> Recordings { get { return _recordings; } }

        [SerializeField] private bool _recordOnStart;

        [SerializeField] [Tooltip("How often to save a record (The more often, the more precise, but less performance)")]
        private float _recordingInterval;

        [SerializeField] [Tooltip("The amount of time to record")] private float _recordingTime;
        [SerializeField] private List<Transform> _trackedTransforms;

        private List<AnimRecording> _recordings = new List<AnimRecording>();
        private float _recordingStartTime;

        public void Awake()
        {
            _recordings = new List<AnimRecording>(_trackedTransforms.Count);
            for (int i = 0; i < _trackedTransforms.Count; i++)
            {
                _recordings.Add(new AnimRecording());
            }
        }

        // Use this for initialization
        void Start()
        {
            if (_recordOnStart)
            {
                StartRecording();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (IsRecording && Time.time >= _recordingStartTime + _recordingTime)
            {
                StopRecording();
            }
        }

        public void StartRecording()
        {
            IsRecording = true;
            InvokeRepeating("SaveRecord", 0f, _recordingInterval);
            _recordingStartTime = Time.time;
        }

        public void StopRecording()
        {
            CancelInvoke("SaveRecord");
            IsRecording = false;
        }

        private void SaveRecord()
        {
            for (int i = 0; i < _trackedTransforms.Count; i++)
            {
                _recordings[i].AddRecording(_trackedTransforms[i].localPosition, _trackedTransforms[i].localEulerAngles);
            }
        }
    }
}