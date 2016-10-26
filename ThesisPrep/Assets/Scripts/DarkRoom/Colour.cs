using UnityEngine;
using System.Collections;

namespace DarkRoom
{

    public class Colour : MonoBehaviour
    {
        [SerializeField]
        private Color _color;


        private Material _material;

        // Use this for initialization
        void Start()
        {
            _material = new Material(Shader.Find("Standard"));
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).GetComponent<MeshRenderer>().material = _material;

            }
        }

        // Update is called once per frame
        void Update()
        {
            _material.color = _color;
        }
    }

}


