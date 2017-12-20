using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ando
{
    public abstract class Weapon : MonoBehaviour
    {
        //  武器のデータテーブル
        protected Kojima.WeaponDataTable weaponData;

        //  装備中の武器に表示する
        [SerializeField]
        protected GameObject equipmentChect;

        //  武器の名前
        [SerializeField]
        protected Text weaponName;
        //  武器のレベル
        [SerializeField]
        protected Text weaponLevel;

        //  武器レベルの増加量
        [SerializeField]
        protected int addWeponLevel = 1;

        // Use this for initialization
        protected virtual void Start()
        {
            //  装備中の武器チェックを非アクティブに
            equipmentChect.SetActive(false);
        }

        /// <summary>
        /// 武器の名前を取得
        /// </summary>
        /// <returns></returns>
        public abstract string GetWeaponName();

        /// <summary>
        /// 武器の説明を取得
        /// </summary>
        /// <returns></returns>
        public abstract string GetWeaponExplanation();
            //return weaponData.WeaponText;

        /// <summary>
        /// 武器のレベルを取得
        /// </summary>
        /// <returns></returns>
        public abstract int GetWeaponLevel();

        /// <summary>
        /// 武器の改造費を取得
        /// </summary>
        /// <returns></returns>
        public abstract int GetWeponStrengthenPrice();

        /// <summary>
        /// 武器の切り替え
        /// </summary>
        public abstract void ChangeWepon();

        /// <summary>
        /// 武器レベルを上昇
        /// </summary>
        public abstract void AddWeponLevel();

        /// <summary>
        /// 装備中の武器チェックを非アクティブに
        /// </summary>
        public void HideEquipmentChack()
        {
            equipmentChect.SetActive(false);
        }

        /// <summary>
        /// 選択された武器を設定
        /// </summary>
        public abstract void SetSelectWeapon();
    }
}
