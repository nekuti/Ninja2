using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NothingEnemyData : ScriptableObject
{
    [SerializeField, Tooltip("名前")]
    private string name;

    [SerializeField, Tooltip("プレハブ")]
    private Kojima.Enemy enemyPrefab;

    [SerializeField, Tooltip("体力")]
    private float hp = 10f;

    public string Name { get { return name; } }
    public Kojima.Enemy EnemyPrefab { get { return enemyPrefab; } }
    public float Hp { get { return hp; } }

}

