using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enumの拡張メソッド
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/07
/// </summary>
namespace Kojima
{
    #region WeaponTypeの拡張メソッド
    static class WeaponTypeEx
    {
        #region メソッド

        /// <summary>
        /// WeaponTypeに対応した武器の待機ステートを渡す
        /// </summary>
        /// <param name="aSelf">武器種</param>
        /// <param name="aOwner">生成主</param>
        /// <returns></returns>
        public static State<WeaponControl> CreateWeaponWaitState(this WeaponType aSelf,WeaponControl aOwner)
        {
            switch (aSelf)
            {
                case WeaponType.Kunai:
                    return new WeaponKunaiWaitState(aOwner);

                case WeaponType.Shuriken:
                    return new WeaponShurikenWaitState(aOwner);

                case WeaponType.Bomb:
                    return new WeaponBombWaitState(aOwner);

                case WeaponType.Katana:
                    return new WeaponKatanaWaitState(aOwner);
            }
            return null;
        }

        /// <summary>
        /// WeaponTypeに対応した武器の攻撃ステートを渡す
        /// </summary>
        /// <param name="aSelf">武器種</param>
        /// <param name="aOwner">生成主</param>
        /// <returns></returns>
        public static State<WeaponControl> CreateWeaponShotState(this WeaponType aSelf,WeaponControl aOwner)
        {
            switch (aSelf)
            {
                case WeaponType.Kunai:
                    return new WeaponKunaiShotState(aOwner);

                case WeaponType.Shuriken:
                    return new WeaponShurikenShotState(aOwner);

                case WeaponType.Bomb:
                    return new WeaponBombShotState(aOwner);

                case WeaponType.Katana:
                    return new WeaponKatanaShotState(aOwner);
            }
            return null;
        }

        /// <summary>
        /// WeaponTypeに対応した武器の反動ステートを渡す
        /// </summary>
        /// <param name="aSelf">武器種</param>
        /// <param name="aOwner">生成主</param>
        /// <returns></returns>
        public static State<WeaponControl> CreateWeaponRecoilState(this WeaponType aSelf, WeaponControl aOwner)
        {
            switch (aSelf)
            {
                case WeaponType.Kunai:
                    return new WeaponKunaiRecoilState(aOwner);

                case WeaponType.Shuriken:
                    return new WeaponShurikenRecoilState(aOwner);

                case WeaponType.Bomb:
                    return new WeaponBombRecoilState(aOwner);

                case WeaponType.Katana:
                    return new WeaponKatanaRecoilState(aOwner);
            }
            return null;
        }

        #endregion
    }
    #endregion

    #region EnemyTypeの拡張メソッド
    static class EnemyTypeEx
    {
        #region

        /// <summary>
        /// EnemyのWaitStateを生成
        /// </summary>
        /// <param name="aSelf"></param>
        /// <param name="aOwner"></param>
        /// <returns></returns>
        public static State<Enemy> CreateWaitState(this EnemyType aSelf,Enemy aOwner)
        {
            switch(aSelf)
            {
                case EnemyType.Strike:
                    return new EnemyStrikeWaitState(aOwner);
                case EnemyType.Assault:
                    return new EnemyAssaultWaitState(aOwner);
                case EnemyType.Sniper:
                    return new EnemySniperWaitState(aOwner);
                default:
                    Debug.Log("WaitStateが未設定の敵");
                    return null;
            }
        }

        /// <summary>
        /// EnemyのPatrolStateを生成
        /// </summary>
        /// <param name="aSelf"></param>
        /// <param name="aOwner"></param>
        /// <returns></returns>
        public static State<Enemy> CreatePatrolState(this EnemyType aSelf, Enemy aOwner)
        {
            switch (aSelf)
            {
                case EnemyType.Strike:
                    return new EnemyStrikePatrolState(aOwner);
                case EnemyType.Assault:
                    return new EnemyAssaultPatrolState(aOwner);
                case EnemyType.Sniper:
                    return new EnemySniperPatrolState(aOwner);
                default:
                    Debug.Log("PatrolStateが未設定の敵");
                    return null;
            }
        }

        /// <summary>
        /// EnemyのChaseStateを生成
        /// </summary>
        /// <param name="aSelf"></param>
        /// <param name="aOwner"></param>
        /// <returns></returns>
        public static State<Enemy> CreateChaseState(this EnemyType aSelf, Enemy aOwner)
        {
            switch (aSelf)
            {
                case EnemyType.Strike:
                    return new EnemyStrikeChaseState(aOwner);
                case EnemyType.Assault:
                    return new EnemyAssaultChaseState(aOwner);
                case EnemyType.Sniper:
                    return new EnemySniperChaseState(aOwner);
                default:
                    Debug.Log("PatrolStateが未設定の敵");
                    return null;
            }
        }

        /// <summary>
        /// EnemyのAttackStateを生成
        /// </summary>
        /// <param name="aSelf"></param>
        /// <param name="aOwner"></param>
        /// <returns></returns>
        public static State<Enemy> CreateAttackState(this EnemyType aSelf, Enemy aOwner)
        {
            switch (aSelf)
            {
                case EnemyType.Strike:
                    return new EnemyStrikeAttackState(aOwner);
                case EnemyType.Assault:
                    return new EnemyAssaultAttackState(aOwner);
                case EnemyType.Sniper:
                    return new EnemySniperAttackState(aOwner);
                default:
                    Debug.Log("AttackStateが未設定の敵");
                    return null;
            }
        }

        /// <summary>
        /// EnemyのDamageStateを生成
        /// </summary>
        /// <param name="aSelf"></param>
        /// <param name="aOwner"></param>
        /// <returns></returns>
        public static State<Enemy> CreateDamageState(this EnemyType aSelf, Enemy aOwner)
        {
            switch (aSelf)
            {
                case EnemyType.Strike:
                    return new EnemyStrikeDamageState(aOwner);
                case EnemyType.Assault:
                    return new EnemyAssaultDamageState(aOwner);
                case EnemyType.Sniper:
                    return new EnemySniperDamageState(aOwner);
                default:
                    Debug.Log("DamageStateが未設定の敵");
                    return null;
            }
        }

        /// <summary>
        /// EnemyのDieStateを生成
        /// </summary>
        /// <param name="aSelf"></param>
        /// <param name="aOwner"></param>
        /// <returns></returns>
        public static State<Enemy> CreateDieState(this EnemyType aSelf, Enemy aOwner)
        {
            switch (aSelf)
            {
                case EnemyType.Strike:
                    return new EnemyStrikeDieState(aOwner);
                case EnemyType.Assault:
                    return new EnemyAssaultDieState(aOwner);
                case EnemyType.Sniper:
                    return new EnemySniperDieState(aOwner);
                default:
                    Debug.Log("DieStateが未設定の敵");
                    return null;
            }
        }

        #endregion
    }
    #endregion

    #region ButtonTypeの拡張メソッド
    static class ButtonTypeEx
    {
        #region メソッド

        public static ulong GetButtonMask(this ButtonType aSelf)
        {
            switch(aSelf)
            {
                case ButtonType.System:
                    return SteamVR_Controller.ButtonMask.System;
                case ButtonType.ApplicationMenu:
                    return SteamVR_Controller.ButtonMask.ApplicationMenu;
                case ButtonType.Grip:
                    return SteamVR_Controller.ButtonMask.Grip;
                case ButtonType.Axis0:
                    return SteamVR_Controller.ButtonMask.Axis0;
                case ButtonType.Axis1:
                    return SteamVR_Controller.ButtonMask.Axis1;
                case ButtonType.Axis2:
                    return SteamVR_Controller.ButtonMask.Axis2;
                case ButtonType.Axis3:
                    return SteamVR_Controller.ButtonMask.Axis3;
                case ButtonType.Axis4:
                    return SteamVR_Controller.ButtonMask.Axis4;
                case ButtonType.Touchpad:
                    return SteamVR_Controller.ButtonMask.Touchpad;
                case ButtonType.Trigger:
                    return SteamVR_Controller.ButtonMask.Trigger;
                default:
                    Debug.Log("未設定のButtonType");
                    return 0;
            }
        }

        #endregion
    }
    #endregion
}