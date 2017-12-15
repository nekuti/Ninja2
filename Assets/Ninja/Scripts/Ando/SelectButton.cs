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
            this.gameObject.GetComponent<ParticleSystem>();

            //  パーティクルシステムの動作を止める
            particleSystem.Stop();
        }

        /// <summary>
        /// パーティクルシステムを開始
        /// </summary>
        public void ParticleStart()
        {
            particleSystem.Play();
        }

        /// <summary>
        /// パーティクルシステムを停止
        /// </summary>
        public void ParticleStop()
        {
            particleSystem.Stop();
            particleSystem.Clear();
        }
    }
}