using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイ中の手のステート
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/08
/// </summary>
namespace Kojima
{
    public class HandPlayState : State<Hand>
    {
        #region メンバ変数

        // 武器管理クラス
        private WeaponControl weapon;
        
        // ワイヤー管理クラス
        private WireControl wire;

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public HandPlayState(Hand owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            Debug.Log("手をPlayモードに変更");
        
            // 武器管理クラスを生成してアタッチ
            weapon = owner.gameObject.AddComponent<WeaponControl>();

            // ワイヤー管理クラスを生成してアタッチ
            wire = owner.gameObject.AddComponent<WireControl>();
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            if(InputDevice.Press(ButtonType.Grip,owner.HandType))
            {
                Debug.Log("アイテム使用ウィンドウを表示");
            }
        }

        /// <summary>
        /// このステートから他のステートに遷移するときに一度だけ呼ばれる
        /// </summary>
        public override void Exit()
        {
            // 生成した武器管理クラスを破棄
            GameObject.Destroy(weapon);

            // 生成したワイヤー管理クラスを破棄
            GameObject.Destroy(wire);
        }

        #endregion
    }
}