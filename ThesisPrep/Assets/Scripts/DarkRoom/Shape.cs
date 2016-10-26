using UnityEngine;
using System.Collections;

namespace DarkRoom
{

    public class Shape : MonoBehaviour
    {
        [SerializeField]
        private ShapeType _type;

        private ShapeType _prevType;

        // Use this for initialization
        void Start()
        {
            _type = ShapeType.Cube;
            UpdateEnabledShapes();
        }

        // Update is called once per frame
        void Update()
        {
            if (_type != _prevType)
            {
                _prevType = _type;
                UpdateEnabledShapes();
            }
        }

        private void UpdateEnabledShapes()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                if (child.name != _type.ToString())
                {
                    child.SetActive(false);
                }
                else
                {
                    child.SetActive(true);
                }
            }
        }

        public enum ShapeType
        {
            Cube, Sphere
        }
    }

}

