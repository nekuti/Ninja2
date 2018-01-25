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

        public bool isMove = false;


        // Use this for initialization
        void Start()
        {
            pos = transform.localPosition;
            endPos += pos.y;
        }

        // Update is called once per frame
        void Update()
        {
            if (isMove)
            {
                isMove = Moveing();
            }
        }

        private bool Moveing()
        {
            if (pos.y >= endPos)
            {
                return false;
            }

            transform.localPosition = pos;
            pos.y += moveSpeed;

            return true;
        }


        public void StartMove()
        {
            Debug.Log("壁の移動：start");
            Ando.AudioManager.Instance.PlaySE(AudioName.SE_ELEVETORDOOR, this.transform.position);
            isMove = true;
        }
    }
}

