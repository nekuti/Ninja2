using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ViveコントローラーにアタッチするHandクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/11
/// </summary>
namespace Kojima
{
    /// <summary>
    /// Handのステートの種類
    /// </summary>
    public enum HandStateType
    {
        Play,
        MenuSelect,
    }

    public class Hand : StatefulObjectBase<Hand,HandStateType>
    {
        #region メンバ変数
        [SerializeField,Tooltip("ワイヤーのデータ")]
        private WireDataTable wireData;
        [SerializeField, Tooltip("武器のデータ")]
        private WeaponDataTable weaponData;

        // 最初に読み込まれるステート
        [System.NonSerialized]
        public HandStateType defaultStateType;

        // Handを持つプレイヤー
        private Player owner;
        // 右手か左手か
        private HandType handType;

        [SerializeField]
        private LayerMask rayMask;
        private Ray ray;

        public GameObject shotPos;
        public GameObject wireObject;

        [SerializeField]
        private GameObject rayObject;
        [SerializeField]
        private GameObject cursorObject;
        
        #endregion

        #region プロパティ
        public Player Owner{ get { return owner; } }
        public HandType HandType { get { return handType; } }
        public WireDataTable WireData { get { return wireData; } }
        public WeaponDataTable WeaponData { get { return weaponData; } }
        #endregion

        #region メソッド

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Awake()
        {
            // Handを持つプレイヤーを取得
            owner = transform.parent.GetComponent<Player>();
            // HandTypeを取得
            handType = GetComponent<InputDevice>().handType;

            // ワイヤーと武器データをプレイヤーから取得
            wireData = owner.WireData;
            weaponData = owner.WeaponData;

            // ステートマシンのインスタンス化
            stateMachine = new StateMachine<Hand>();
            // ステートリストにステートを追加
            stateList.Add(new HandPlayState(this));
            stateList.Add(new HandMenuselectState(this));
        }

        /// <summary>
        /// 更新前処理
        /// </summary>
        private void Start()
        {
            // 初期のステートを設定
            ChangeState(defaultStateType);
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        protected override void Update()
        {
            base.Update();

            // rayを設定
            ray = new Ray(shotPos.transform.position, transform.rotation * Vector3.forward);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, wireData.ShotRange, rayMask))
            {
                // 距離を求める
                Vector3 range = hit.point - shotPos.transform.position;

                // レーザーの長さを設定
                rayObject.transform.localScale = new Vector3(1f, range.magnitude, 1f);

                // カーソルを当たった位置に張り付くように回転させる
                cursorObject.transform.rotation = Quaternion.LookRotation(hit.normal);
                // 移動させる
                cursorObject.transform.position = hit.point;
                // カーソルを表示
                cursorObject.SetActive(true);                
            }
            else
            {
                // レーザーの長さを設定
                rayObject.transform.localScale = new Vector3(1f, wireData.ShotRange, 1f);
                // カーソルを非表示
                cursorObject.SetActive(false);
            }
        }

        /// <summary>
        /// 設定済みの武器を装備する
        /// </summary>
        /// <returns></returns>
        public bool EquipWeapon()
        {
            ChangeState(HandStateType.Play);

            return true;
        }

        /// <summary>
        /// 武器を変更する
        /// </summary>
        /// <param name="aWeaponData"></param>
        /// <returns></returns>
        public bool ChangeWeapon(WeaponDataTable aWeaponData)
        {
            weaponData = aWeaponData;

            // プレイ中の変更であれば即時に再生成
            if (IsCurrentState(HandStateType.Play))
            {
                EquipWeapon();
            }
            return true;
        }

        #endregion
    }
}