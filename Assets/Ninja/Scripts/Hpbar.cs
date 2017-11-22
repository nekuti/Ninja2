using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hpbarのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/22
/// </summary>
namespace Kojima
{
    public class Hpbar : MonoBehaviour
    {
        #region メンバ変数

        public Player player;

        public GameObject midori;

        #endregion

        #region メソッド

        /// <summary>
        /// 更新処理
        /// </summary>
        void Update()
        {
            float percent = player.Energy / player.MaxEnergy;
            if (percent > 1f) percent = 1f;
            if (percent < 0f) percent = 0f;

            midori.transform.localScale = new Vector3(percent,1f, 1f);
        }

        #endregion
    }
}