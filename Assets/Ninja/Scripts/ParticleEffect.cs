using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ParticleSystemにアタッチして制御するクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/23
/// </summary>
namespace Kojima
{
    public class ParticleEffect : MonoBehaviour
    {
        #region メンバ変数
        [SerializeField, Tooltip("消えるまでの時間")]
        private float time = 1f;

        [SerializeField, Tooltip("再生速度")]
        private float speed = 1f;

        [SerializeField,Tooltip("消えないパーティクル")]
        private bool infinity = false;

        #endregion

        #region メソッド

        /// <summary>
        /// 更新前処理
        /// </summary>
        private void Start()
        {
            // 全ての子パーティクルシステムを取得し再生速度を設定する
            ParticleSystem[] psArray = GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < psArray.Length; i++)
            {
                ParticleSystem.MainModule psm = psArray[i].main;
                psm.simulationSpeed = speed;
            }
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        private void Update()
        {
            if (!infinity)
            {
                if (time <= 0f)
                {
                    // オブジェクトを消す
                    Destroy(transform.gameObject);
                }
                time -= Time.deltaTime;
            }
        }

        /// <summary>
        /// パーティクルを生成する静的関数
        /// </summary>
        /// <param name="aEffectType">パーティクルの種類</param>
        /// <param name="aPos">生成する座標</param>
        /// <returns></returns>
        public static ParticleEffect Create(ParticleEffectType aEffectType,Vector3 aPos)
        {
            return Instantiate(Resources.Load(aEffectType.IsFilePathName()), aPos, Quaternion.identity) as ParticleEffect;
        }

        #endregion
    }
}