using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DarkRoom;

namespace Ganzfeld
{

    public class LightController : MonoBehaviour
    {
        [SerializeField]
        private List<LightSequence> _sequenceList;
        [SerializeField]
        private List<Light> _lights;
        [SerializeField]
        private bool Loop;

        private Color CurrentColor
        {
            get { return RenderSettings.ambientLight; }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartAnimation()
        {
            StopAnimation();
            StartCoroutine(SeqRunner());
        }

        public void StopAnimation()
        {
            StopAllCoroutines();
        }

        private IEnumerator SeqRunner()
        {
            do
            {
                foreach (LightSequence sequence in _sequenceList)
                {
                    for (int i = 0; i < sequence.RepeatCount; ++i)
                    {
                        foreach (LightTarget target in sequence.Targets)
                        {
                            Color start = CurrentColor;
                            Color end = target.Color;
                            float startTime = Time.time;
                            float endTime = startTime + target.Duration;
                            do
                            {
                                Color newColor = target.Duration == 0 ? end : Color.Lerp(start, end, (Time.time - startTime) / target.Duration);
                                SetNewColor(newColor);
                                yield return null;
                            } while (Time.time <= endTime);
                        }
                    }
                }
            } while (Loop);
        }

        private void SetNewColor(Color newColor)
        {
            RenderSettings.ambientLight = newColor;
            // Change light colours.
            foreach (var l in _lights)
            {
                l.color = newColor;
            }
        }
    }
}