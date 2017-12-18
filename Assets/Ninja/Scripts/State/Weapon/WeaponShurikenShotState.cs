using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スリケンの攻撃ステート
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/12
/// </summary>
namespace Kojima
{
    public class WeaponShurikenShotState : State<WeaponControl>
    {
        #region メンバ変数

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public WeaponShurikenShotState(WeaponControl owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            Debug.Log("WeaponShurikenの攻撃");
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            // 武器の精密度
            float diffusion = owner.MyHand.WeaponData.Diffusion;

            // 設定された発射数になるまで繰り返す
            for (int i = 0;i < owner.MyHand.WeaponData.Many;i++)
            {
                // ブレを求める
                Quaternion dire = Quaternion.Euler(Random.Range(-diffusion,diffusion), Random.Range(-diffusion,diffusion),0f);

                // ブレを加えて弾を生成
                Attack.Create(owner.MyHand.WeaponData.WeaponPrefab, owner.transform.position,
                    owner.transform.position + owner.transform.rotation * (dire * Vector3.forward),owner.MyHand.WeaponData.Power, owner.tag);
            }

            // 反動ステートへ移行
            owner.ChangeState(WeaponStateType.Recoil);
        }

        /// <summary>
        /// このステートから他のステートに遷移するときに一度だけ呼ばれる
        /// </summary>
        public override void Exit()
        {
        }

        #endregion
    }
}