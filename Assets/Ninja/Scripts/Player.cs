using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/23
/// </summary>
namespace Kojima
{
    /// <summary>
    /// Playerのステートタイプ
    /// </summary>
    public enum PlayerStateType
    {
        Free,
    }
    
    public class Player : StatefulObjectBase<Player,PlayerStateType>,IDamageable
    {
        #region メンバ変数
        [SerializeField, Tooltip("エネルギーの最大値")]
        private float maxEnergy;
        [SerializeField, Tooltip("現在のエネルギー")]
        private float energy;
        
        [SerializeField,Tooltip("左手")]
        private Hand leftHand;
        [SerializeField,Tooltip("右手")]
        private Hand rightHand;

        [SerializeField, Tooltip("ワイヤーのデータ")]
        private WireDataTable wireData;
        [SerializeField, Tooltip("武器のデータ")]
        private WeaponDataTable weaponData;

        [SerializeField,Tooltip("最初に読み込まれる手のステート")]
        private HandStateType defaultStateType;

        [SerializeField, Tooltip("アイテム選択ウィンドウのプレハブ")]
        private GameObject selectItemPrefab;

        [SerializeField, Tooltip("移動時のパーティクル")]
        private ParticleEffect moveParticle;

        [SerializeField]
        private GameObject center;

        [SerializeField]
        private Attack blastPrefab;

        private Rigidbody myRigidbody;


        private bool posResetFlg;
        private bool resultFlg;
        private bool trrigerFlg;

        #endregion

        #region プロパティ
        public float MaxEnergy { get { return maxEnergy; } }
        public float Energy
        {
            get { return energy; }
            private set
            {
                energy = value;
                // エネルギーが上限、下限を超えないようにする
                if (energy > maxEnergy) energy = maxEnergy;
                if (energy < 0)         energy = 0;
            }
        }
        public Hand LeftHand { get { return leftHand; } }
        public Hand RightHand { get { return rightHand; } }
        public WireDataTable WireData { get { return wireData; } }
        public WeaponDataTable WeaponData { get { return weaponData; } }
        public GameObject SelectItemPrefab { get { return selectItemPrefab; } }
        public Rigidbody MyRigidbody { get { return myRigidbody; } }
        #endregion

        #region メソッド

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Awake()
        {
            // プレイヤーを登録
            Ando.PlaySceneManager.SetPlayer(this);

            // Rigidbodyの取得
            myRigidbody = GetComponent<Rigidbody>();

            // 敵にプレイヤーの中心を登録
            Enemy.EntryPlayer(center);

            // 手を取得
            if (leftHand == null)leftHand = transform.Find("LeftHand").GetComponent<Hand>();
            if (rightHand == null)rightHand = transform.Find("RightHand").GetComponent<Hand>();
            // 手の初期ステートタイプを設定
            leftHand.defaultStateType = defaultStateType;
            rightHand.defaultStateType = defaultStateType;
        }

        /// <summary>
        /// 更新前処理
        /// </summary>
        private void Start()
        {


            posResetFlg = true;
            //resultFlg = false;
            //trrigerFlg = true;

            /*β版処理*/
            Ando.PlaySceneManager.SetPlayer(this);
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        protected override void Update()
        {
            // 移動時のパーティクルの更新
            UpdateMoveParticle();

            // =============================
            // やべー処理===================
            if (posResetFlg)
            {
                if(PosReset())
                {
                    posResetFlg = false;
                }
            }
            // やべー処理===================
            // =============================
        }

        /// <summary>
        /// 攻撃を受ける
        /// </summary>
        /// <param name="aDamage">攻撃のダメージ量</param>
        public bool TakeAttack(Attack anAttack)
        {
            // 自身が発射した攻撃でなければ
            if (anAttack.parentTagName != gameObject.tag)
            {
                Energy -= anAttack.power;
                // SEを再生
                Ando.AudioManager.Instance.PlaySE(AudioName.SE_PLAYER_DAMAGE, transform.position);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ゴールにぶつかった時の処理
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(TagName.Goal))
            {
                // ゴールした時の処理
                posResetFlg = true;

                //Ando.PlaySceneManager.SetStageTransition(Ando.StageTransition.ResultGameClear);
                // resultFlg = true;

            }
        }

        /// <summary>
        /// 武器を変更する
        /// </summary>
        /// <param name="aWeaponData"></param>
        /// <returns></returns>
        public bool ChangeWeapon(WeaponDataTable aWeaponData)
        {
            weaponData = aWeaponData;

            // Handにも変更を適応
            leftHand.ChangeWeapon(aWeaponData);
            rightHand.ChangeWeapon(aWeaponData);

            return true;
        }

        /// <summary>
        /// プレイヤーを飛ばす
        /// </summary>
        /// <param name="aForce">飛ばす力</param>
        public void PullPlayer(Vector3 aForce, float aMaxVelocity)
        {
            // 力を加える
            myRigidbody.AddForce(aForce,ForceMode.Acceleration);

            if(myRigidbody.velocity.sqrMagnitude > aMaxVelocity * aMaxVelocity)
            {
                myRigidbody.velocity -= myRigidbody.velocity - (myRigidbody.velocity.normalized * aMaxVelocity);
            }
        }

        /// <summary>
        /// 手のステートを変更する
        /// </summary>
        /// <param name="aType">手のステート</param>
        /// <returns></returns>
        public bool ChangeHandState(HandStateType aType)
        {
            if (rightHand != null && leftHand != null)
            {
                rightHand.ChangeState(aType);
                leftHand.ChangeState(aType);
                return true;
            }
            else
            {
                Debug.Log("プレイヤーにHandが登録されていません");
                return false;
            }
        }


        public bool PosReset()
        {
            // 初期座標へ移動
            GameObject pos = GameObject.Find("StartPos");
            if (pos != null)
            {
                transform.position = pos.transform.position;
                transform.rotation = pos.transform.rotation;
                Destroy(pos);
                Debug.Log("プレイヤーの初期座標を設定");
                return true;
            }
            else
            {
                Debug.Log("プレイヤーの初期座標が設定されていません");
                return false;
            }
        }
        public void ResetPosition(Vector3 aPos)
        {
            transform.position = aPos;
            myRigidbody.velocity = Vector3.zero;
        }

        /// <summary>
        /// オニギリを使用
        /// </summary>
        /// <param name="aData"></param>
        public void UseOnigiri()
        {
            if(Ando.PlaySceneManager.CheckEmpty())
            {
                Ando.ItemData onigiri = Ando.PlaySceneManager.GetOnigiri();
                Debug.Log(onigiri);
                // オニギリが1個以上あるか
                if(onigiri.possession > 0)
                {
                    // オニギリを消費
                    Ando.PlaySceneManager.SubPossessionOnigiri(1);
                    // 回復量に合わせてエネルギーを割合回復
                    Energy += maxEnergy * ((onigiri.itemData as ItemHealDataTable).HealPoint / 100f);


                    // SEを再生
                    Ando.AudioManager.Instance.PlaySE(AudioName.SE_ITEM_USE_ONIGIRI, transform.position);
                }
            }
            else
            {
                Debug.Log("PlaySceneManagerが無いのでオニギリ食べ放題");
                Energy += maxEnergy * (5f / 100f);
                // SEを再生
                Ando.AudioManager.Instance.PlaySE(AudioName.SE_ITEM_USE_ONIGIRI, transform.position);
            }
        }

        /// <summary>
        /// 火遁術の使用
        /// </summary>
        public void UseKaton()
        {
            if(Ando.PlaySceneManager.CheckEmpty())
            {
                Ando.ItemData katon = Ando.PlaySceneManager.GetFireSkill();
                Debug.Log(katon);
                // アイテムが1個以上あるか
                if(katon.possession > 0)
                {
                    // アイテムを消費
                    Ando.PlaySceneManager.SubPossessionFireSkill(1);

                    ItemAttackDataTable data = katon.itemData as ItemAttackDataTable;
                    AttackBlast blast = Attack.Create(data.Prefab, transform.position, 
                        transform.forward, data.Power, data.DestroyTime, data.BulletSpeed, tag) as AttackBlast;
                    blast.transform.localScale = new Vector3(data.Range, data.Range, data.Range);

                    // SEを再生
                    Ando.AudioManager.Instance.PlaySE(AudioName.SE_ITEM_USE_KATON, transform.position);
                }
            }
            else
            {
                Debug.Log("PlaySceneManagerが無いのでカトン使い放題");

                AttackBlast blast = Attack.Create(blastPrefab, transform.position, transform.forward, 20, 0.2f, 0, tag) as AttackBlast;
                // 爆発範囲を設定(強化レベルによって範囲増加)
                blast.transform.localScale = new Vector3(10f, 10f, 10f);
                // SEを再生
                Ando.AudioManager.Instance.PlaySE(AudioName.SE_ITEM_USE_KATON, transform.position);
            }
        }

        /// <summary>
        /// エネルギーを消費
        /// </summary>
        /// <param name="aEnergy">消費エネルギー量</param>
        /// <returns></returns>
        public bool ExpenseEnergy(float aEnergy)
        {
            if(Energy - aEnergy > 0)
            {
                Energy -= aEnergy;
                return true;
            }
            return false;
        }

        /// <summary>
        /// エネルギーのリセット
        /// </summary>
        /// <returns></returns>
        public bool ResetEnergy()
        {
            Energy = MaxEnergy;
            return true;
        }

        /// <summary>
        /// 移動時のパーティクルの更新
        /// </summary>
        public void UpdateMoveParticle()
        {
            Vector3 velocity = MyRigidbody.velocity;

            // 最大速度に対する割合
            float speedProportion = velocity.magnitude / wireData.PullSpeed;

            // ある程度スピードが出ている場合
            if(speedProportion > 0.5f)
            {
                moveParticle.gameObject.SetActive(true);
                // 移動方向へパーティクルを向ける
                moveParticle.transform.LookAt(transform.position + velocity);
            }
            else
            {
                moveParticle.gameObject.SetActive(false);
            }
        }

        #endregion
    }
}