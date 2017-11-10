using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// WireDataTableのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/09
/// </summary>
namespace Kojima
{
    [CreateAssetMenu]
    public class WireDataTable : ScriptableObject
    {
        #region メンバ変数
        [SerializeField,Tooltip("名前")]
        private string wireName;
        
        [SerializeField, Tooltip("ワイヤーのプレハブ")]
        private GameObject wirePrefab;
        
        [SerializeField, Tooltip("ワイヤーの種類")]
        private WireType wireType;
        
        [SerializeField, Tooltip("射程距離")]
        private float shotRange;
        
        [SerializeField, Tooltip("射出速度")]
        private float shotSpeed;
        
        [SerializeField, Tooltip("巻き取り時の最高速度")]
        private float pullSpeed;

        #endregion

        #region プロパティ
        public string WireName { get { return wireName; } }
        public GameObject WirePrefab { get { return wirePrefab; } }
        public WireType WireType { get { return wireType;} }
        public float ShotRange { get { return shotRange; } }
        public float ShotSpeed { get { return shotSpeed; } }
        public float PullSpeed { get { return pullSpeed; } }
        #endregion

        #region メソッド

        #endregion
    }
}