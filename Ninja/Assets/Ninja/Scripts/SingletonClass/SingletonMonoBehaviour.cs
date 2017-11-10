using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton用のジェネリッククラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/07
/// </summary>
namespace Kojima
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        #region メンバ変数
        private static T instance;
        #endregion

        #region プロパティ
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (instance == null)
                    {
                        Debug.LogError(typeof(T) + "is nothing");
                    }
                }

                return instance;
            }
        }
        #endregion

        #region メソッド

        /// <summary>
        /// 初期化処理
        /// </summary>
        protected virtual void Awake()
        {
            // このクラスが既にインスタンス化されていた場合は自分を消す
            if (this != Instance)
            {
                Destroy(this.gameObject);
                return;
            }

            // シーン切り替え時に破棄されないようにする
            DontDestroyOnLoad(this.gameObject);
        }

        #endregion
    }
}