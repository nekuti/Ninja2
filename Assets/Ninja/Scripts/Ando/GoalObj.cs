using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Ando
{
    public class GoalObj : MonoBehaviour
    {
        //  ゴールしたか
        private bool goalFlag = false;

        //  プロパティ
        public bool GoalFlag
        {
            get { return goalFlag; }
            private set { goalFlag = value; }
        }

        private void OnCollisionEnter(Collision collision)
        {
            //  ぶつかったオブジェクトのタグがプレイヤーの場合
            if (collision.gameObject.tag == TagName.Player)
            {
                GoalFlag = true;
            }
        }
    }
}