using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Animatorの拡張メソッド
/// 最終更新 : 2017/ 5/29
/// </summary>
static class AnimatorEx
{
    /// <summary>
    /// 指定したアニメーションのAnimatorStateInfoを取得する(許容型)
    /// </summary>
    /// <param name="_animator"></param>
    /// <param name="_animeName">取得したいアニメーションの名前</param>
    /// <param name="_layerIndex"></param>
    /// <returns>AnimatorStateInfoの許容型 指定のアニメーションが再生中でなければnullを返す</returns>
    public static AnimatorStateInfo? GetNullableAnimatorStateInfo(this Animator _animator,string _animeName,System.Int32 _layerIndex = 0)
    {
        // 現行のAnimatorStateInfoを取得する
        AnimatorStateInfo animeState = _animator.GetCurrentAnimatorStateInfo(_layerIndex);
        if(animeState.IsName(_animeName))
        {
            return animeState;
        }

        // 次のAnimatorStateInfoを取得する
        animeState = _animator.GetNextAnimatorStateInfo(_layerIndex);
        if (animeState.IsName(_animeName))
        {
            return animeState;
        }
        else
        {
            // 指定アニメーションは再生中ではないためnullを返す
            return null;
        }
    }

    /// <summary>
    /// 指定したアニメーションのAnimatorStateInfoを取得する
    /// </summary>
    /// <param name="aSelf"></param>
    /// <param name="anAnimationName">取得したいアニメーションの名前</param>
    /// <param name="aLayerIndex"></param>
    /// <returns></returns>
    public static AnimatorStateInfo GetStateAnimatorStateInfo(this Animator aSelf,string anAnimationName,System.Int32 aLayerIndex = 0)
    {
        AnimatorStateInfo info = new AnimatorStateInfo();

        // 現行のAnimatorStateInfoを取得する
        AnimatorStateInfo animeState = aSelf.GetCurrentAnimatorStateInfo(aLayerIndex);
        if (animeState.IsName(anAnimationName))
        {
            return animeState;
        }

        // 次のAnimatorStateInfoを取得する
        animeState = aSelf.GetNextAnimatorStateInfo(aLayerIndex);
        if (animeState.IsName(anAnimationName))
        {
            return animeState;
        }

        return info;
    }

    /// <summary>
    /// 指定のアニメーションの進行率を取得
    /// </summary>
    /// <param name="aSelf"></param>
    /// <param name="anAnimationName">取得したいアニメーションの名前</param>
    /// <param name="aLayerIndex"></param>
    /// <returns>進行率を返す(指定アニメーションが再生中でなければ-1を返す)</returns>
    public static float GetAnimationProgress(this Animator aSelf,string anAnimationName,System.Int32 aLayerIndex = 0)
    {
        float progress = -1f;

        // 許容型のAnimatorStateInfoの取得
        AnimatorStateInfo? nullableInfo = aSelf.GetNullableAnimatorStateInfo(anAnimationName, aLayerIndex);

        if(nullableInfo != null)
        {
            AnimatorStateInfo info = (AnimatorStateInfo)nullableInfo;
            progress = info.normalizedTime;
        }

        return progress;
    }
}