using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ViveコントローラーにアタッチするHandクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/10
/// </summary>
namespace Kojima
{
    /// <summary>
    /// Handのステートの種類
    /// </summary>
    public enum HandStateType
    {
        Wire,
        Weapon,
    }

    public class Hand : StatefulObjectBase<Hand,HandStateType>
    {
        #region メンバ変数
        [SerializeField,Tooltip("ワイヤーのデータ")]
        private WireDataTable wireData;
        [SerializeField, Tooltip("武器のデータ")]
        private WeaponDataTable weaponData;

        [SerializeField, Tooltip("最初のステート")]
        private HandStateType defaultStateType;

        // VRの入力用
        [System.NonSerialized]
        public SteamVR_TrackedObject trackdObject;
        [System.NonSerialized]
        public SteamVR_Controller.Device device;

        // Handを持つプレイヤー
        [System.NonSerialized]
        public Player owner;

        [SerializeField]
        private LayerMask rayMask;
        private Ray ray;

        public GameObject shotPos;
        public GameObject wireObject;

        [SerializeField]
        private GameObject rayObject;
        [SerializeField]
        private GameObject cursorObject;

        // 武器ステートのリスト
        private List<HandWeaponState> weaponStateList = new List<HandWeaponState>();
        #endregion

        #region プロパティ
        public Player Owner{ get { return owner; } }
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

            // ワイヤーと武器データをプレイヤーから取得
            if(wireData == null) wireData = owner.WireData;
            if(weaponData == null) weaponData = owner.WeaponData;

            // 武器種毎のステートを生成してリストに保存
            weaponStateList.Add(WeaponType.Kunai.CreateWeaponState(this));
            weaponStateList.Add(WeaponType.Shuriken.CreateWeaponState(this));
            //weaponStateList.Add(WeaponType.Bomb.CreateWeaponState(this));
            //weaponStateList.Add(WeaponType.Katana.CreateWeaponState(this));

            // ステートマシンのインスタンス化
            stateMachine = new StateMachine<Hand>();

            // ステートリストにステートを追加
            stateList.Add(new HandNormalWireState(this));
            stateList.Add(weaponStateList[(int)weaponData.WeaponType]); // 装備中の装備の武器種のステートを追加
        }

        /// <summary>
        /// 更新前処理
        /// </summary>
        private void Start()
        {
            // VRの入力用変数初期化
            trackdObject = GetComponent<SteamVR_TrackedObject>();
            if (trackdObject != null) device = SteamVR_Controller.Input((int)trackdObject.index);

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
            ray = new Ray(shotPos.transform.position, transform.rotation * Vector3.up);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, wireData.ShotRange, rayMask))
            {
                // 距離を求める
                Vector3 range = hit.point - shotPos.transform.position;

                // レーザーの長さを設定
                rayObject.transform.localScale = new Vector3(1f, range.magnitude, 1f);

                // カーソルを当たった位置に張り付くように回転させる
                cursorObject.transform.rotation = Quaternion.LookRotation(hit.normal);
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
        /// ワイヤーを装備
        /// </summary>
        /// <returns></returns>
        public bool EquipWire()
        {
            ChangeState(HandStateType.Wire);

            return true;
        }

        /// <summary>
        /// 設定済みの武器を装備する
        /// </summary>
        /// <returns></returns>
        public bool EquipWeapon()
        {
            if (weaponStateList.Count > (int)weaponData.WeaponType)
            {
                // ステートリストをその武器に合わせて変更
                stateList[(int)HandStateType.Weapon] = weaponStateList[(int)weaponData.WeaponType];
            }
            else
            {
                Debug.Log("指定された武器種のステートが未生成");
            }
            ChangeState(HandStateType.Weapon);

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

            // 武器を装備中であれば装備しなおす
            if (IsCurrentState(HandStateType.Weapon))
            {
                EquipWeapon();
            }
            return true;
        }

        #endregion
    }
}