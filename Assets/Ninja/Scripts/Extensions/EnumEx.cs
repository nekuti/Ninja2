using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enumの拡張メソッド
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/15
/// </summary>
namespace Kojima
{
    static class WeaponTypeEx
    {
        #region メソッド

        /// <summary>
        /// WeaponTypeに対応した武器ステートを渡す
        /// </summary>
        /// <param name="aSelf"></param>
        /// <param name="aOwner"></param>
        /// <returns></returns>
        public static HandWeaponState CreateWeaponState(this WeaponType aSelf,Hand aOwner)
        {
            switch (aSelf)
            {
                case WeaponType.Kunai:
                    return new HandWeaponKunaiState(aOwner);

                case WeaponType.Shuriken:
                    return new HandWeaponShurikenState(aOwner);

                case WeaponType.Bomb:
                    break;
                case WeaponType.Katana:
                    break;
            }
            return null;
        }

        #endregion
    }

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
}