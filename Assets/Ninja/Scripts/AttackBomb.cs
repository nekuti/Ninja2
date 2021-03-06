﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AttackBombのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/15
/// </summary>
namespace Kojima
{
    public class AttackBomb : Attack
    {
        #region メンバ変数
        [SerializeField, Tooltip("爆風のプレハブ")]
        private Attack blastPrefab;

        public float range;

        private Rigidbody myRigidbody;

        private Ando.SoundEffectObject sound;

        #endregion

        #region メソッド

        /// <summary>
        /// 更新前処理
        /// </summary>
        protected override void Start()
        {
            base.Start();

            // シュー音
            sound = Ando.AudioManager.Instance.PlaySE(AudioName.SE_ATTACK_FUSE_BOMB,transform.position);
            sound.transform.parent = transform;
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        protected override void Update()
        {
            // 寿命が来たら爆発する
            if(TimerCount())
            {
                Explosion();
            }
        }

        /// <summary>
        /// 爆発させる
        /// </summary>
        private void Explosion()
        {
            Debug.Log("BOOOOM!!");
            // 爆風を生成して自身を消す
            Attack blast = Attack.Create(blastPrefab, transform.position, TargetPos, power, parentTagName);
            blast.transform.localScale = new Vector3(range,range,range);
            Destroy(this.gameObject);
            // 爆発音を再生
            Ando.AudioManager.Instance.PlaySE(AudioName.SE_ATTACK_EXPLODE_BOMB, transform.position);

            // シュー音を停止
            sound.SoundStop();
        }

        /// <summary>
        /// 攻撃が当たった(Trriger)
        /// </summary>
        /// <param name="other"></param>
        protected new void OnTriggerEnter(Collider other)
        {
            // なにもしない
        }

        /// <summary>
        /// 攻撃が当たった(Collision)
        /// </summary>
        /// <param name="collision"></param>
        protected new void OnCollisionEnter(Collision collision)
        {
            // ダメージを受けるオブジェクトであれば爆発する
            var obj = collision.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
            if (obj != null)
            {
                if (!collision.gameObject.CompareTag(parentTagName))
                {
                    Explosion();
                }
            }
        }

        #endregion
    }
}