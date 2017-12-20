using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SelectItemのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/20
/// </summary>
namespace Kojima
{
    public class SelectItem : MonoBehaviour,ISelectable
    {
        #region メンバ変数

        public Ando.ItemDataTable data;

        public Player myPlayer;


        #endregion

        #region メソッド

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Awake()
        {

        }

        /// <summary>
        /// 更新前処理
        /// </summary>
        private void Start()
        {

        }

        /// <summary>
        /// 更新処理
        /// </summary>
        private void Update()
        {

        }

        /// <summary>
        /// レイが当たった
        /// </summary>
        public void HitRayObject()
        {
        }

        /// <summary>
        /// レイが外れた
        /// </summary>
        public void OutRayObject()
        {
        }

        /// <summary>
        /// 決定がされた
        /// </summary>
        public void SelectObject()
        {
        }

        #endregion
    }
}