using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃のクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/23
/// </summary>
namespace Kojima
{
    public class Attack : MonoBehaviour
    {
        #region メンバ変数

        [SerializeField,Tooltip("攻撃のタイプ")]
        protected AttackType attackType;

        [SerializeField, Tooltip("当たった際のエフェクト")]
        protected ParticleEffectType effect;

        [SerializeField, Tooltip("攻撃の移動速度")]
        protected float speed = 4f;

        [SerializeField,Tooltip("消えるまでの時間")]
        protected float time = 2f;

        [SerializeField,Tooltip("ユニットを貫通するか")]
        protected bool ThroughUnit = false;

        [SerializeField,Tooltip("マップを貫通するか")]
        protected bool ThroughMap = false;

        // 攻撃力
        public float power;

        // 生成主のタグ
        public string parentTagName;

        private Vector3 parentPos;
        private Vector3 targetPos;

        // 時間を数える変数
        private float timer;

        #endregion

        #region プロパティ
        public Vector3 ParentPos { get { return parentPos; } }
        public Vector3 TargetPos { get { return targetPos; } }
        #endregion

        #region メソッド

        /// <summary>
        /// 更新前処理
        /// </summary>
        protected virtual void Start()
        {
            timer = 0f;
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        protected virtual void Update()
        {
            // 寿命が来たら自身を消す
            if(TimerCount())
            {
                Destroy(this.gameObject);
            }

            // 攻撃を移動させる
            MoveAttack();
        }

        /// <summary>
        /// 攻撃が当たった(Trriger)
        /// </summary>
        /// <param name="other"></param>
        protected void OnTriggerEnter(Collider other)
        {
            Debug.Log(this + "あたったたた");
            // 壁に攻撃が当たった場合
            if (other.gameObject.tag == TagName.WireableObject || other.gameObject.CompareTag(TagName.Object))
            {
                HitTrrigerWall(other.gameObject);
            }

            // ダメージを受けるオブジェクトであれば
            var obj = other.GetComponent(typeof(IDamageable))as IDamageable;
            if(obj != null)
            {
                // ユニットに攻撃が当たった場合
                if(obj.TakeAttack(this))
                {
                    HitTrrigerUnit(other.gameObject);
                }
            }
        }

        /// <summary>
        /// 攻撃が当たった(Collision)
        /// </summary>
        /// <param name="collision"></param>
        protected void OnCollisionEnter(Collision collision)
        {
            // 壁に攻撃が当たった場合
            if (collision.gameObject.CompareTag(TagName.WireableObject) || collision.gameObject.CompareTag(TagName.Object))
            {
                HitCollisionWall(collision.gameObject);
            }

            // ダメージを受けるオブジェクトであれば
            var obj = collision.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
            if (obj != null)
            {
                // ユニットに攻撃が当たった場合
                if (obj.TakeAttack(this))
                {
                    HitCollisionUnit(collision.gameObject);
                }
            }
        }

        /// <summary>
        /// 攻撃を生成する
        /// </summary>
        /// <param name="aPrefab">攻撃のプレハブ</param>
        /// <param name="aParentPos">攻撃発生場所</param>
        /// <param name="aTargetPos">狙う位置</param>
        /// <param name="aPower">攻撃力</param>
        /// <param name="aParentTag">攻撃者のタグ</param>
        /// <returns></returns>
        public static Attack Create(Attack aPrefab,Vector3 aParentPos, Vector3 aTargetPos,float aPower, string aParentTag)
        {
            // プレハブを生成
            Attack obj = Instantiate<Attack>(aPrefab, aParentPos, Quaternion.identity);

            // 初期値を設定
            obj.power = aPower;
            obj.parentTagName = aParentTag;
            obj.transform.LookAt(aTargetPos);

            return obj;
        }

        /// <summary>
        /// タイマーを進めて寿命が来たらtrueを返す
        /// </summary>
        /// <returns></returns>
        protected bool TimerCount()
        {
            if (time < timer)
            {
                return true;
            }
            else
            {
                timer += Time.deltaTime;
                return false;
            }
        }

        /// <summary>
        /// 攻撃を動かす
        /// </summary>
        protected virtual void MoveAttack()
        {
            // 移動させる
            transform.position += transform.rotation * (Vector3.forward * speed * Time.deltaTime);
        }

        /// <summary>
        /// 壁に当たった時に呼び出される処理
        /// </summary>
        /// <param name="aWall"></param>
        protected virtual void HitTrrigerWall(GameObject aWall)
        {
            // 壁に当たったら削除
            if (!ThroughMap)
            {
                ParticleEffect.Create(ParticleEffectType.Flash_small01, transform.position);
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// ユニットに当たった時に呼び出される処理
        /// </summary>
        /// <param name="aUnit"></param>
        protected virtual void HitTrrigerUnit(GameObject aUnit)
        {
            // ユニットを貫通しない弾であれば当たった時点で削除
            if (!ThroughUnit)
            {
                ParticleEffect.Create(effect, transform.position);
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// 壁に当たった時に呼び出される処理
        /// </summary>
        /// <param name="aWall"></param>
        protected virtual void HitCollisionWall(GameObject aWall)
        {
        }

        /// <summary>
        /// ユニットに当たった時に呼び出される処理
        /// </summary>
        /// <param name="aUnit"></param>
        protected virtual void HitCollisionUnit(GameObject aUnit)
        {
        }

        #endregion
    }
}