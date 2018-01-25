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

        // 武器のデータ
        private WeaponDataTable data;

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

            // 武器データ取得
            data = owner.MyHand.WeaponData;

            // SEを再生
            Ando.AudioManager.Instance.PlaySE(AudioName.SE_ATTACK_SHOT_SHURIKEN, owner.transform.position);
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            // 武器の精密度
            float diffusion = owner.MyHand.WeaponData.Diffusion;

            // 設定された発射数になるまで繰り返す(強化レベルによって発射数増加)
            for (int i = 0;i < owner.MyHand.WeaponData.Many * owner.LevelBonus();i++)
            {
                // ブレを求める
                Quaternion dire = Quaternion.Euler(Random.Range(-diffusion,diffusion), Random.Range(-diffusion,diffusion),0f);
                Vector3 targetPos = owner.transform.position + owner.transform.rotation * (dire * Vector3.forward);

                // ブレを加えて弾を生成
                Attack.Create(data.WeaponPrefab, owner.transform.position,targetPos,
                    data.Power, data.DestroyTime, data.BulletSpeed, owner.tag);
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