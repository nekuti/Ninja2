using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class ConfirmationScreen : MonoBehaviour
    {

        //  警告の表示時間
        [SerializeField]
        private float displayTime = 1.0f;

        //  経過時間
        private float elapsedTime = 0.0f;

        // Update is called once per frame
        void Update()
        {
            elapsedTime += Time.deltaTime;

            //  経過時間を超えたか確認
            if (elapsedTime >= displayTime)
            {
                //  経過時間を初期化
                elapsedTime = 0.0f;

                //  警告を見えないようにする
                this.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 経過時間を初期化
        /// </summary>
        public void InitElapsedTime()
        {
            elapsedTime = 0.0f;
        }
    }
}
