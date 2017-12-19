using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// WeaponControlのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/08
/// </summary>
namespace Kojima
{
    /// <summary>
    /// 攻撃時のステートの種類
    /// </summary>
    public enum WeaponStateType
    {
        Wait,
        Shot,
        Recoil,
    }
    public class WeaponControl : StatefulObjectBase<WeaponControl,WeaponStateType>
    {
        #region メンバ変数

        private Hand myHand;

        [SerializeField]
        private int maxWeaponLevel = 5;

        public int weaponLevel = 1;

        #endregion

        #region プロパティ
        public Hand MyHand { get { return myHand; } }
        #endregion

        #region メソッド

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Awake()
        {
            // 生成主の手を取得
            myHand = GetComponent<Hand>();

            // ステートマシンのインスタンス化
            stateMachine = new StateMachine<WeaponControl>();
            // ステートリストにステートを追加
            stateList.Add(myHand.WeaponData.WeaponType.CreateWeaponWaitState(this));
            stateList.Add(myHand.WeaponData.WeaponType.CreateWeaponShotState(this));
            stateList.Add(myHand.WeaponData.WeaponType.CreateWeaponRecoilState(this));
        }

        /// <summary>
        /// 更新前処理
        /// </summary>
        private void Start()
        {
            // 初期のステートを設定
            ChangeState(WeaponStateType.Wait);
            Debug.Log("武器管理クラスができました");
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        protected override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// 強化レベルによるボーナス%を取得(100% ~ 200%)
        /// </summary>
        /// <returns></returns>
        public float LevelBonus()
        {
            if(Ando.PlaySceneManager.CheckEmpty())
            {
                switch(myHand.WeaponData.WeaponType)
                {
                    case WeaponType.Kunai:
                        weaponLevel = Ando.PlaySceneManager.GetKunaiLevel();
                        break;
                    case WeaponType.Shuriken:
                        weaponLevel = Ando.PlaySceneManager.GetThrowingStarLevel();
                        break;
                    case WeaponType.Bomb:
                        weaponLevel = Ando.PlaySceneManager.GetBombLevel();
                        break;
                    default:
                        Debug.Log(myHand.WeaponData.WeaponType + "のレベルが未設定");
                        break;
                }
            }
            return LevelBonus(weaponLevel);

        }
        public float LevelBonus(int aLevel)
        {
            // 武器強化の上限値を取得
            if(Ando.PlaySceneManager.CheckEmpty())
            {
                maxWeaponLevel = Ando.PlaySceneManager.GetWeaponStrengthenMaxLevel();
            }

            // 強化レベルによるボーナス%を取得(100% ~ 200%)
            return 1f + (aLevel - 1) / (maxWeaponLevel - 1);
        }

        #endregion
    }
}