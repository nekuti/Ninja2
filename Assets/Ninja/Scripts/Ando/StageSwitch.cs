using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public enum FloorLevel
    {
        First = 1,
        Secind,
        Third,

        //  ここより上に追加してね
        None,
    }

    public class StageSwitch : MonoBehaviour
    {
        //  レイが当たった時に表示するパーティクル
        [SerializeField]
        private GameObject particle;

        //  このスイッチの移動階層
        public FloorLevel myFloorLevel = FloorLevel.None;

        //  クリックが押されたか
        private bool clickFlag = false;

        public bool ClickFlag
        {
            get { return clickFlag; }
            protected set { }
        }

        // Use this for initialization
        void Start()
        {
            if(myFloorLevel == FloorLevel.None)
            {
                Debug.Log("階層移動スイッチに移動階層が設定されていません");
            }

            //  クリックフラグをfalseへ
            clickFlag = false;

            //  パーティクルを非アクティブに
            particle.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// レイが当たった
        /// </summary>
        public void HitRayObject()
        {
            //  パーティクルをアクティブへ
            ParticleActive(true);
        }

        /// <summary>
        /// レイが外れた
        /// </summary>
        public void OutRayObject()
        {
            //  パーティクルを非アクティブへ
            ParticleActive(false);
        }

        /// <summary>
        /// 決定がされた
        /// </summary>
        public void SelectObject()
        {
            //  クリックフラグをtrueへ
            clickFlag = true;
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