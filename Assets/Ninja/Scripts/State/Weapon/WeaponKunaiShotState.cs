using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クナイの攻撃ステート
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/07
/// </summary>
namespace Kojima
{
    public class WeaponKunaiShotState : State<WeaponControl>
    {
        #region メンバ変数
        // 武器のデータ
        private WeaponDataTable data;

        private float timer;

        // 発射数のカウント
        private uint count;

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public WeaponKunaiShotState(WeaponControl owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            Debug.Log("WeaponKunaiの攻撃");

            // 武器データ取得
            data = owner.MyHand.WeaponData;

            timer = 999f;
            count = 0;

            // SEを再生
            Ando.AudioManager.Instance.PlaySE(AudioName.SE_ATTACK_SHOT_KUNAI, owner.transform.position);
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            // 連射速度に合わせて弾を生成
            if(timer > owner.MyHand.WeaponData.FireSpeed)
            {
                Attack.Create(data.WeaponPrefab, owner.transform.position, owner.transform.position + owner.transform.forward,
                    data.Power, data.DestroyTime, data.BulletSpeed,owner.tag);
                count++;
                timer = 0f;
            }
            else
            {
                timer += Time.deltaTime;
            }

            // 設定した発射数に達したら反動へ(強化レベルによって範囲増加)
            if (count >= data.Many * owner.LevelBonus())
            {
                owner.ChangeState(WeaponStateType.Recoil);
            }
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