using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物理挙動を持ったAttackのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/23
/// </summary>
namespace Kojima
{
    public class AttackPhysics : Attack
    {
        #region メンバ変数

        private Rigidbody myRigidbody;
        
        #endregion

        #region メソッド

        /// <summary>
        /// 更新前処理
        /// </summary>
        protected override void Start()
        {
            base.Start();

            myRigidbody = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        protected override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// 攻撃を動かす
        /// </summary>
        protected override void MoveAttack()
        {
            // 移動させる
            myRigidbody.AddForce(transform.rotation * (Vector3.forward * speed * Time.deltaTime),ForceMode.VelocityChange);
            if(myRigidbody.velocity.magnitude > speed)
            {
                myRigidbody.velocity -= myRigidbody.velocity - (myRigidbody.velocity.normalized * speed);
            }
        }

        /// <summary>
        /// 壁に当たった時に呼び出される処理
        /// </summary>
        /// <param name="aWall"></param>
        protected override void HitCollisionWall(GameObject aWall)
        {
            // 壁に当たったら削除
            if (!ThroughMap)
            {
                myRigidbody.velocity = Vector3.zero;
                myRigidbody.isKinematic = true;
                ParticleEffect.Create(ParticleEffectType.Flash_small01, transform.position);

                // SEを再生
                Ando.AudioManager.Instance.PlaySE(AudioName.SE_ATTACK_HIT_KUNAI, transform.position);
            }
        }

        /// <summary>
        /// ユニットに当たった時に呼び出される処理
        /// </summary>
        /// <param name="aUnit"></param>
        protected override void HitCollisionUnit(GameObject aUnit)
        {
            // ユニットを貫通しない弾であれば当たった時点で削除
            if (!ThroughUnit)
            {
                ParticleEffect.Create(effect,transform.position);
                Destroy(this.gameObject);

                // SEを再生
                Ando.AudioManager.Instance.PlaySE(AudioName.SE_ATTACK_HIT_KUNAI, transform.position);
            }
        }

        #endregion
    }
}