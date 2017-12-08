using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class StageSwitch : MonoBehaviour
    {

        [SerializeField]
        private GameObject particle;

        // Use this for initialization
        void Start()
        {
            particle.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// パーティクルを動かすか設定
        /// </summary>
        /// <param name="anActive"></param>
        public void ParticleActive(bool anActive)
        {
            particle.SetActive(anActive);
        }
    }
}