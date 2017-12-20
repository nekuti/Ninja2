using System;
using System.Collections;
using UnityEngine;

namespace Ando
{
    /// <summary>
    /// シングルトンクラス
    /// 作成者：安藤 茂貴
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        protected static T instance;
        public static T Instance
        {
            get
            {
                //  インスタンスがあるかどうか
                if (instance == null)
                {
                    //  typeof省略のために作成
                    Type t = typeof(T);

                    //  インスタンスを探す部分
                    instance = (T)FindObjectOfType(t);

                    //  インスタンスがあったか
                    if (instance == null)
                    {
                        Debug.LogError(typeof(T) + "はありません");
                    }
                }

                return instance;
            }
        }

        /// <summary>
        ///  継承先でもAwakを使う場合は必ず「bace.Awake()」を呼ぶこと
        /// </summary>
        protected void Awake()
        {
            //  インスタンスがあるか確認
            CheckInstance();
        }

        /// <summary>
        /// インスタンスがあるか確認
        /// </summary>
        /// <returns>インスタンスが存在したかどうか</returns>
        protected bool CheckInstance()
        {
            //  インスタンスがあるかどうか
            if (instance == null)
            {
                //  なかったので作る
                instance = (T)this;

                return true;
            }
            else if (Instance == this)
            {
                return true;
            }

            //  すでにインスタンスがあったので破棄する
            Destroy(this.gameObject);

            return false;
        }

        /// <summary>
        /// インスタンスが空かどうか確認
        /// </summary>
        /// <returns></returns>
        public static bool CheckEmpty()
        {
            //  インスタンスがあるかどうか
            if (instance != null)
            {
                return true;
            }

            return false;
        }

    }
}