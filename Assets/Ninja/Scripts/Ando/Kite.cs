using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class Kite : MonoBehaviour
    {
        //  凧の種類
        public KiteType myType = KiteType.None;
        
        //  凧に苦無が当たったか
        public bool hit = false;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag = TagName.Attack)
            {
                hit = true;
            }
        }

    }
}
