using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ganzfeld
{
    [Serializable]
    public class LightSequence
    {
        public List<LightTarget> Targets;
        public int RepeatCount;
    }
}

