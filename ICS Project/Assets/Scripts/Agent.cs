using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seirs
{
    [RequireComponent(typeof(Renderer))] // require from object to have Renderer script
    public class Agent : MonoBehaviour
    {
        private Renderer rend;

        [SerializeField] private Material redMaterial;
        [SerializeField] private Material blueMaterial;
        [SerializeField] private Material greenMaterial;
        [SerializeField] private Material yellowMaterial;

        public int Id { get; set; }
        public Material RedMaterial { get => redMaterial; set => redMaterial = value; }
        public Material BlueMaterial { get => blueMaterial; set => blueMaterial = value; }
        public Material GreenMaterial { get => greenMaterial; set => greenMaterial = value; }
        public Material YellowMaterial { get => yellowMaterial; set => yellowMaterial = value; }

        public void SetPosition(float x, float y, float z = 0)
        {
            transform.position = new Vector3(x, y, z);
        }

        public void SetRadius(float r)
        {
            transform.localScale = Vector3.one * r;
        }

        public void SetColor(State s)
        {
            rend = rend ?? GetComponent<Renderer>();

            switch (s)
            {
                case State.S:
                    Debug.Log("green");
                    rend.material = greenMaterial;
                    break;
                case State.E:
                    rend.material = yellowMaterial;
                    break;
                case State.I:
                    rend.material = redMaterial;
                    break;
                case State.R:
                    rend.material = blueMaterial;
                    break;
            }
        }
    }

    public enum State
    {
        S,
        E,
        I,
        R
    }
}
