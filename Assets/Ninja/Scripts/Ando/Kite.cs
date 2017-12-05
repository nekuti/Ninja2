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

        private void OnCollisionEnter(Collision collision)
        {
            //  当たったオブジェクトのタグがAttackだった場合
            if(collision.gameObject.tag == "Attack")
            {
                hit = true;
            }
        }
    }
}
