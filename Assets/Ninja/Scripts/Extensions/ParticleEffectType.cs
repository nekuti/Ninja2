﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ParticleEffectTypeのenumとその拡張メソッド
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/23
/// </summary>
namespace Kojima
{
    /// <summary>
    /// パーティクルの種類
    /// </summary>
    public enum ParticleEffectType
    {
        Flash01,
        Flash02,
        Flash_small01,
        Ring01,
        Explosion01,
        Explosion02,
        Explosion_small01,
        Heal01,
    }

    /// <summary>
    /// ParticleEffectTypeの拡張メソッド
    /// </summary>
    static class ParticleEffectTypeEx
    {
        #region メソッド
        /// <summary>
        /// ParticleEffectのプレハブ名を文字列で渡す関数
        /// </summary>
        /// <param name="aSelf"></param>
        /// <returns></returns>
        public static string IsName(this ParticleEffectType aSelf)
        {
            switch(aSelf)
            {
                case ParticleEffectType.Flash01:        return "Flash01";
                case ParticleEffectType.Flash02:        return "Flash02";
                case ParticleEffectType.Flash_small01:  return "Flash_small01";
                case ParticleEffectType.Ring01:         return "Ring01";
                case ParticleEffectType.Explosion01:    return "Explosion01";
                case ParticleEffectType.Explosion02:    return "Explosion02";
                case ParticleEffectType.Heal01:         return "Heal01";
                default:                                return "error";
            }
        }

        /// <summary>
        /// Resourcesフォルダからのパスを含めたプレハブ名を文字列で渡す関数
        /// </summary>
        /// <param name="aSelf"></param>
        /// <returns></returns>
        public static string IsFilePathName(this ParticleEffectType aSelf)
        {
            string name = aSelf.IsName();
            if (name == "error")
            {
                name = aSelf.ToString();
                Debug.Log(name + "のIsNameが未設定です");
            }
            return "ParticleEffects/" + name;
        }

        #endregion
    }
}