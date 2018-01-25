using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class SoundEffectObject : MonoBehaviour
    {

        //  SE用オーディオソース
        [SerializeField]
        private AudioSource SESource;

        void Update()
        {
            if (!SESource.isPlaying)
            {
                Debug.Log("SE用オブジェクトさんが消えますん");
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// SEの音量を設定
        /// </summary>
        /// <param name="aVolume"></param>
        public void SetVolume(float aVolume)
        {
            SESource.volume = aVolume;
            Debug.Log("SEのボリュームを" + aVolume + "に設定しました");
        }

        /// <summary>
        /// SEの音源を設定
        /// </summary>
        /// <param name="aSE"></param>
        public void SetSound(AudioClip aSE)
        {
            SESource.PlayOneShot(aSE);
            Debug.Log("SEの音源に" + aSE.name + "を設定しました");
        }

        /// <summary>
        /// 再生を停止する
        /// </summary>
        public void SoundStop()
        {
            if (SESource != null)
            {
                SESource.Stop();
                Debug.Log("SEの停止");
            }
        }
    }
}