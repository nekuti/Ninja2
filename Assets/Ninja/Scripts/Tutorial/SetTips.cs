using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Kondo
{
    public class SetTips : MonoBehaviour
    {
        public HandType hand;
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
            if (baseTrans == null)
            {
                
            }

            baseTrans = ControllerData.instance.GetPartsTransform(hand, PartsType.Base);
            this.transform.position = baseTrans.position;
            this.transform.rotation = baseTrans.rotation;

        }
    }

}
