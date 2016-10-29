using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DarkRoom
{
    public class PRS : MonoBehaviour
    {

        public List<PRSTarget> PositionSequence = new List<PRSTarget>();
        public bool LoopPos = false;
        private Coroutine posCoroutine;
        public List<PRSTarget> RotationSequence = new List<PRSTarget>();
        public bool LoopRot = false;
        private Coroutine rotCoroutine;
        public List<PRSTarget> ScaleSequence = new List<PRSTarget>();
        public bool LoopSca = false;
        private Coroutine scaCoroutine;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        }

        public void StartSeq(PRSType type)
        {
            StopSeq(type);
            switch (type)
            {
                case PRSType.Position:
                    posCoroutine = StartCoroutine(SeqRunner(type));
                    break;
                case PRSType.Rotation:
                    rotCoroutine = StartCoroutine(SeqRunner(type));
                    break;
                case PRSType.Scale:
                    scaCoroutine = StartCoroutine(SeqRunner(type));
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }

        public void StopSeq(PRSType type)
        {
            switch (type)
            {
                case PRSType.Position:
                    if (posCoroutine != null)
                    {
                        StopCoroutine(posCoroutine);
                    }
                    break;
                case PRSType.Rotation:
                    if (rotCoroutine != null)
                    {
                        StopCoroutine(rotCoroutine);
                    }
                    break;
                case PRSType.Scale:
                    if (scaCoroutine != null)
                    {
                        StopCoroutine(scaCoroutine);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }

        private IEnumerator SeqRunner(PRSType type)
        {
            List<PRSTarget> sequence = GetSequence(type);
            do
            {
                foreach (var prsTarget in sequence)
                {
                    Vector3 start = GetVariable(type);
                    Vector3 end = prsTarget.Target;
                    float startTime = Time.time;
                    float endTime = startTime + prsTarget.Duration;
                    do
                    {
                        Vector3 newValue = Vector3.zero;
                        newValue = prsTarget.Duration == 0 ? end : Vector3.Lerp(start, end, (Time.time - startTime) / prsTarget.Duration);
                        SetVariable(type, newValue);
                        yield return null;
                    } while (Time.time <= endTime);
                }
            } while (GetLoop(type));
        }

        private bool GetLoop(PRSType type)
        {
            switch (type)
            {
                case PRSType.Position:
                    return LoopPos;
                case PRSType.Rotation:
                    return LoopRot;
                case PRSType.Scale:
                    return LoopSca;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }

        private List<PRSTarget> GetSequence(PRSType type)
        {
            switch (type)
            {
                case PRSType.Position:
                    return PositionSequence;
                case PRSType.Rotation:
                    return RotationSequence;
                case PRSType.Scale:
                    return ScaleSequence;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }

        private Vector3 GetVariable(PRSType type)
        {
            switch (type)
            {
                case PRSType.Position:
                    return transform.position;
                case PRSType.Rotation:
                    return transform.eulerAngles;
                case PRSType.Scale:
                    return transform.localScale;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }

        private void SetVariable(PRSType type, Vector3 newValue)
        {
            switch (type)
            {
                case PRSType.Position:
                    transform.position = newValue;
                    break;
                case PRSType.Rotation:
                    transform.eulerAngles = newValue;
                    break;
                case PRSType.Scale:
                    transform.localScale = newValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }

        public enum PRSType
        {
            Position, Rotation, Scale
        }

    }
}