using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クナイの反動ステート
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/07
/// </summary>
namespace Kojima
{
    public class WeaponKunaiRecoilState : State<WeaponControl>
    {
        #region メンバ変数

        private float timer;

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public WeaponKunaiRecoilState(WeaponControl owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            Debug.Log("WeaponKunaiの反動");
            timer = 0f;
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            // 設定した反動時間に達したら待機へ戻す
            if (timer > owner.MyHand.WeaponData.Recoil)
            {
                owner.ChangeState(WeaponStateType.Wait);
            }
            else
            {
                timer += Time.deltaTime;
            }
        }

        /// <summary>
        /// このステートから他のステートに遷移するときに一度だけ呼ばれる
        /// </summary>
        public override void Exit()
        {
            // SEを再生
            Ando.AudioManager.Instance.PlaySE(AudioName.SE_ATTACK_READY, owner.transform.position);
        }

        #endregion
    }
}