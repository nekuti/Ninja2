using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Kondo
{
    public class SetTips : MonoBehaviour
    {
        public HandType hand;
        public float upPos = 0.05f;

        private Transform baseTrans;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {


            SetBase();

        }

        private void SetBase()
        {
            if(!ControllerData.instance.IsEndFind)
            {
                return;
            }

            baseTrans = ControllerData.instance.GetPartsTransform(hand, PartsType.Base);

            Vector3 pos = baseTrans.position;
            pos.y += upPos;
            this.transform.localPosition = pos;

            this.transform.rotation = baseTrans.rotation;
            this.transform.Rotate(90, 0, 0);


        }
    }

}
