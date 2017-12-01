using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ワイヤーの種類
/// </summary>
public enum WireType
{
    Normal,
}

/// <summary>
/// 武器の種類
/// </summary>
public enum WeaponType
{
    Kunai,
    Shuriken,
    Bomb,
    Katana,
}

/// <summary>
/// 敵の種類
/// </summary>
public enum EnemyType
{
    Strike,
    Assault,
    Sniper,
}

/// <summary>
/// 攻撃のタイプ
/// </summary>
public enum AttackType
{
    Bullet,
    Explosion,
    Strike,
    Slash,
}

/// <summary>
/// 入力ボタンの種類
/// </summary>
public enum ButtonType
{
    System,
    ApplicationMenu,
    Grip,
    Axis0,
    Axis1,
    Axis2,
    Axis3,
    Axis4,
    Touchpad,
    Trigger,
}