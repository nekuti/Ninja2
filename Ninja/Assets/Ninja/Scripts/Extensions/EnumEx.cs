using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enumの拡張メソッド
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/08
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
}