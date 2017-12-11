using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kondo
{
    public class WallMove : MonoBehaviour
    {

        public float moveSpeed = 1.0f;

        public float endPos = 15.0f;

        private Vector3 pos;

        public bool moveEnd;


        // Use this for initialization
        void Start()
        {
            pos = transform.localPosition;
            endPos += pos.y;
        }

        // Update is called once per frame
        void Update()
        {
            if (!moveEnd)
            {
                moveEnd = Moveing();
            }
        }

        private bool Moveing()
        {
            if (pos.y >= endPos)
            {
                return true;
            }

            transform.localPosition = pos;
            pos.y += moveSpeed;

            return false;
        }


        public void StartMove()
        {
            Debug.Log("start");
            moveEnd = false;
        }
    }
}

