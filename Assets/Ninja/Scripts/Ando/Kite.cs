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
            //  nullチェック
            if (this.gameObject == null)
            {
                return;
            }

            //  パーティクルを開始
            particleSystem.Play();
        }

        /// <summary>
        /// レイが外れた
        /// </summary>
        public void OutRayObject()
        {
            //  nullチェック
            if (this.gameObject == null)
            {
                return;
            }

            //  パーティクルを停止
            particleSystem.Stop();
            //  画面に残ったパーティクルを削除
            particleSystem.Clear();
        }

        /// <summary>
        /// 決定がされた
        /// </summary>
        public void SelectObject()
        {
            //  nullチェック
            if (this.gameObject == null)
            {
                return;
            }

            hit = true;
        }

    }
}
