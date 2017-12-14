using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ando
{
    public abstract class Weapon : MonoBehaviour
    {
        //  武器のデータテーブルを保存
        [SerializeField]
        protected Kojima.WeaponDataTable weaponData;

        //  武器の名前
        [SerializeField]
        protected Text weaponName;
        //  武器のレベル
        [SerializeField]
        protected Text weaponLevel;

        // Use this for initialization
        protected virtual void Start()
        {
            //  テキストにデータを挿入
            weaponName.text = GetWeaponName();
            weaponLevel.text = "?" + "Lv";
        }

        // Update is called once per frame
        void Update()
        {

        }


        /// <summary>
        /// 武器の名前を取得
        /// </summary>
        /// <returns></returns>
        public string GetWeaponName()
        {
            return weaponData.WeaponName;
        }

        /// <summary>
        /// 選択された武器を設定
        /// </summary>
        public abstract void SetSelectWeapon();
    }
}
