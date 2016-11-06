using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AnimRecorder
{
    [Serializable]
    public class AnimRecording
    {

        public List<AnimRecordingItem> AnimRecordingItems;

        public void AddRecording(Vector3 position, Vector3 eulerAngles)
        {
            // Create record item list, if not exists.
            if (AnimRecordingItems == null)
            {
                AnimRecordingItems = new List<AnimRecordingItem>();
            }
            // Add new record.
            AnimRecordingItems.Add(new AnimRecordingItem
            {
                PosX = position.x,
                PosY = position.y,
                PosZ = position.z,
                RotX = eulerAngles.x,
                RotY = eulerAngles.y,
                RotZ = eulerAngles.z
            });
        }

    }
}