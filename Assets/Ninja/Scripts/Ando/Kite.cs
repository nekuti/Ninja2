using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class Kite : MonoBehaviour,Kojima.ISelectable
    {
        //  凧の種類
        public KiteType myType = KiteType.None;
        
        //  凧に苦無が当たったか
        public bool hit = false;

        //  凧が持っているパーティクルシステムを保存
        private ParticleSystem particleSystem;

        private void Start()
        {
            //  ゲームオブジェクトからパーティクルシステムを取得
            particleSystem = this.gameObject.GetComponent<ParticleSystem>();

            //  パーティクルを止める
            particleSystem.Stop();
        }

        /// <summary>
        /// レイが当たった
        /// </summary>
        public void HitRayObject()
        {
            particleSystem.Play();
        }

        /// <summary>
        /// レイが外れた
        /// </summary>
        public void OutRayObject()
        {
            particleSystem.Stop();
        }

        /// <summary>
        /// 決定がされた
        /// </summary>
        public void SelectObject()
        {
            hit = true;
        }

        /// <summary>
        /// 何かが凧に当たったか
        /// </summary>
        /// <param name="collision"></param>
        //private void OnCollisionEnter(Collision collision)
        //{
        //    //  当たったオブジェクトのタグがAttackだった場合
        //    if(collision.gameObject.tag == "Attack")
        //    {
        //        //  hitフラグをtrueへ
        //        hit = true;
        //    }

        //    //  当たったオブジェクトのタグがRayだった場合
        //    if (collision.gameObject.tag == "Ray")
        //    {
        //        particleSystem.Play();
        //    }
        //}

        /// <summary>
        /// 凧に当たっていたオブジェクトが離れたか
        /// </summary>
        /// <param name="collision"></param>
        //private void OnCollisionExit(Collision collision)
        //{
        //    //  離れたオブジェクトのタグがRayだった場合
        //    if (collision.gameObject.tag == "Ray")
        //    {
        //        //  パーティクルを停止する
        //        particleSystem.Stop();
        //    }
        //}
    }
}
