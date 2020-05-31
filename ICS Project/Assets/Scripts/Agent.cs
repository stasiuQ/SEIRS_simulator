using System;
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
        [SerializeField] private Vector3 baseScale = new Vector3(0.9f, 0.9f, 0.9f);


        public int Id { get; set; }
        public Material RedMaterial { get => redMaterial; set => redMaterial = value; }
        public Material BlueMaterial { get => blueMaterial; set => blueMaterial = value; }
        public Material GreenMaterial { get => greenMaterial; set => greenMaterial = value; }
        public Material YellowMaterial { get => yellowMaterial; set => yellowMaterial = value; }
        public Vector3 BaseScale { get => baseScale; set => baseScale = value; }

        public void SetPosition(float x, float z, float y = 0)
        {
            transform.position = new Vector3(x, y, z);
        }

        public void SetRadius(float r)
        {
            transform.localScale = baseScale * r;
        }

        public void SetColor(State s)
        {
            rend = rend ? rend : GetComponent<Renderer>();

            switch (s)
            {
                case State.S:
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
                case State.Hidden:
                    rend.enabled = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(s), s, null);
            }
        }
    }

    public enum State
    {
        S,
        E,
        I,
        R,
        Hidden
    }
}
