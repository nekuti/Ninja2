using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class ShopScroll : MonoBehaviour
    {
        //  パーティクルシステムの保存用
        private ParticleSystem particleSystem;

        // Use this for initialization
        void Start()
        {
            //  パーティクルシステムを取得
            particleSystem = GetComponentInChildren<ParticleSystem>();
            //  パーティクルを開始
            particleSystem.Play();
        }
    }
}
