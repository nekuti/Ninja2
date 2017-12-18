using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// バクダンの攻撃ステート
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/15
/// </summary>
namespace Kojima
{
    public class WeaponBombShotState : State<WeaponControl>
    {
        #region メンバ変数
        // 武器のデータ
        private WeaponDataTable data;

        private AttackBomb myBomb;

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public WeaponBombShotState(WeaponControl owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            Debug.Log("WeaponBombの攻撃");

            // 武器データ取得
            data = owner.MyHand.WeaponData;

            // バクダンを生成
            myBomb = Attack.Create(data.WeaponPrefab, owner.transform.position,owner.transform.forward,
                data.Power, data.DestroyTime, data.BulletSpeed, owner.tag) as AttackBomb;
            // 爆発範囲を設定(強化レベルによって範囲増加)
            myBomb.range = data.Many * owner.LevelBonus();

            FixedJoint fx = owner.gameObject.AddComponent<FixedJoint>();
            fx.breakForce = 20000;
            fx.breakTorque = 20000;

            // 爆弾を手にジョイントさせる
            fx.connectedBody = myBomb.GetComponent<Rigidbody>();
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            if(myBomb == null)
            {
                owner.ChangeState(WeaponStateType.Recoil);
                return;
            }

            // パッドを離した場合バクダンを手から離す
            if (!InputDevice.Press(ButtonType.Touchpad,owner.MyHand.HandType))
            {
                ReleaseBomb();
                owner.ChangeState(WeaponStateType.Recoil);
            }
        }

        /// <summary>
        /// このステートから他のステートに遷移するときに一度だけ呼ばれる
        /// </summary>
        public override void Exit()
        {
        }

        /// <summary>
        /// 爆弾を手から離す処理
        /// </summary>
        private void ReleaseBomb()
        {
            if (owner.GetComponent<FixedJoint>())
            {
                // FixedJointを消す
                FixedJoint[] joints = owner.GetComponents<FixedJoint>();
                for(int i = 0; i < joints.Length;i++)
                {
                    joints[i].connectedBody = null;
                    GameObject.Destroy(joints[i]);
                }

                // 手を離したバクダンに慣性を与える(WeaponDataのspeed分力を強くする)
                Vector3 velocity = InputDevice.GetDevice(owner.MyHand.HandType).velocity;
                Vector3 angularVelocity = InputDevice.GetDevice(owner.MyHand.HandType).angularVelocity;
                myBomb.GetComponent<Rigidbody>().velocity = velocity * owner.MyHand.WeaponData.BulletSpeed;
                myBomb.GetComponent<Rigidbody>().angularVelocity = angularVelocity * owner.MyHand.WeaponData.BulletSpeed;
            }
            myBomb = null;
        }

        #endregion
    }
}