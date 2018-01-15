using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Ando
{
    public class SelectButton : MonoBehaviour
    {

        private ParticleSystem particleSystem;

        // Use this for initialization
        void Start()
        {
            //  パーティクルシステムを取得
            particleSystem = this.gameObject.GetComponent<ParticleSystem>();

            //  パーティクルシステムの動作を止める
            particleSystem.Stop();
        }

        /// <summary>
        /// パーティクルシステムを開始
        /// </summary>
        public void ParticleStart()
        {
            if(this == null)
            {
                return;
            }

            particleSystem.Play();
        }

        /// <summary>
        /// パーティクルシステムを停止
        /// </summary>
        public void ParticleStop()
        {
            if(this == null)
            {
                return;
            }
            particleSystem.Stop();
            particleSystem.Clear();
        }
    }
}