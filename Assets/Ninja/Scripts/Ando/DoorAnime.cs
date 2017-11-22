using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public enum DoorAnimeState
    {
        Stop,
        Start,
        End,
    }

    public class DoorAnime : MonoBehaviour
    {
        //  アニメーションの開始フラグ
        static private DoorAnimeState animeFlag = DoorAnimeState.Stop;

        //  最大移動量
        public float maxMovementAmount = 1.2f;
        //  移動量
        public float movementAmount = 0.01f;

        //  移動方向(true:右 false:左)
        public bool movementDirection = true;

        // Use this for initialization
        void Start()
        {
            if (!movementDirection)
            {
                movementAmount *= -1;
                maxMovementAmount *= -1;
            }
            maxMovementAmount += this.gameObject.transform.position.z;
        }

        // Update is called once per frame
        void Update()
        {
            if (animeFlag == DoorAnimeState.Start)
            {
                if (!movementDirection)
                {
                    if (maxMovementAmount < this.gameObject.transform.position.z)
                    {
                        this.gameObject.transform.position += new Vector3(0, 0, movementAmount);
                    }
                    else
                    {
                        animeFlag = DoorAnimeState.End;
                    }
                }
                else
                {
                    if (maxMovementAmount > this.gameObject.transform.position.z)
                    {
                        this.gameObject.transform.position += new Vector3(0, 0, movementAmount);
                    }
                    else
                    {
                        animeFlag = DoorAnimeState.End;
                    }
                }
            }
        }

        public static void SetDoorAnimeState(DoorAnimeState state)
        {
            animeFlag = state;
        }
        public static DoorAnimeState GetDoorAnimeState()
        {
            return animeFlag;
        }
    }
}