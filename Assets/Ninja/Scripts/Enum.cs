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
    Boss,
    Boss2,
    Boss3,
}

/// <summary>
/// 攻撃のタイプ
/// </summary>
public enum AttackType
{
    Bullet,
    Blast,
    Strike,
    Slash,
    Stone,
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