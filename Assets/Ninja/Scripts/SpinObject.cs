using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スピンするオブジェクトのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/12
/// </summary>
namespace Kojima
{
    public class SpinObject : MonoBehaviour
    {
        /// <summary>
        /// ぶつかった時に回転を止めるオブジェクト
        /// </summary>
        [System.Serializable]
        private class StopSpinObject
        {
            public bool WireableObject;
            public bool Object;
            public bool Player;
            public bool Enemy;
            public bool Attack;
        }

        // 回転軸
        private enum SpinAxis
        {
            X,Y,Z
        }

        #region メンバ変数

        [SerializeField]
        private GameObject spinObject;

        [SerializeField]
        private float speed;

        [SerializeField]
        private SpinAxis axis;

        [SerializeField]
        private StopSpinObject stopSpinObject = new StopSpinObject();

        private bool spinFlg;

        #endregion

        #region メソッド

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Awake()
        {
            // 未設定の場合スクリプトの付いたオブジェクトを回す
            if (spinObject == null)
            {
                spinObject = this.gameObject;
            }

            spinFlg = true;
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        private void Update()
        {
            // 回転させる
            if (spinFlg)
            {
                switch(axis)
                {
                    case SpinAxis.X:
                        spinObject.transform.Rotate(new Vector3(90f, 0f, 0f) * Time.deltaTime * speed, Space.Self);
                        break;
                    case SpinAxis.Y:
                        spinObject.transform.Rotate(new Vector3(0f, 90f, 0f) * Time.deltaTime * speed, Space.Self);
                        break;
                    case SpinAxis.Z:
                        spinObject.transform.Rotate(new Vector3(0f, 0f, 90f) * Time.deltaTime * speed, Space.Self);
                        break;
                }
            }
        }

        /// <summary>
        /// 何かにぶつかったときの処理
        /// </summary>
        /// <param name="coll"></param>
        void OnCollisionEnter(Collision other)
        {
            // 壁にぶつかった時は回転を止める
            if (other.gameObject.CompareTag(TagName.WireableObject) && stopSpinObject.WireableObject)
            {
                spinFlg = false;
            }

            // 壁にぶつかった時は回転を止める
            if (other.gameObject.CompareTag(TagName.Object) && stopSpinObject.Object)
            {
                spinFlg = false;
            }

            #endregion
        }
    }
}