using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class Kunai : Weapon
    {
        protected override void Start()
        {
            base.Start();

            //  武器のデータテーブルを保存
            weaponData = PlaySceneManager.GetKunai().weaponData;

            //  プレイヤーの現在の装備を確認
            if (PlaySceneManager.GetPlayer().WeaponData.WeaponName == weaponData.WeaponName)
            {
                //  装備中だったら武器チェックをアクティブに
                equipmentChect.SetActive(true);
            }

            //  武器の名前を表示
            weaponName.text = GetWeaponName();
            //  武器レベルを表示
            weaponLevel.text = GetWeaponLevel() + "Lv";
        }

        void Update()
        {
            if (GetWeaponLevel() < PlaySceneManager.GetWeaponStrengthenMaxLevel())
            {
                //  武器レベルを表示
                weaponLevel.text = GetWeaponLevel() + "LV";
            }
        }

        /// <summary>
        /// 武器の名前を取得
        /// </summary>
        /// <returns></returns>
        public override string GetWeaponName()
        {
            return weaponData.WeaponName;
        }

        /// <summary>
        /// 武器の説明を取得
        /// </summary>
        /// <returns></returns>
        public override string GetWeaponExplanation()
        {
            return weaponData.WeaponText;
        }        

        /// <summary>
        /// 武器のレベルを取得
        /// </summary>
        /// <returns></returns>
        public override int GetWeaponLevel()
        {
            return PlaySceneManager.GetKunai().strengthenLevel;
        }

        /// <summary>
        /// 武器の改造費を取得
        /// </summary>
        /// <returns></returns>
        public override int GetWeponStrengthenPrice()
        {
            return (weaponData.Cost * GetWeaponLevel());
        }

        /// <summary>
        /// 武器のレベルを上昇
        /// </summary>
        public override void AddWeponLevel()
        {
            PlaySceneManager.AddKunaiLevel(addWeponLevel);
        }

        /// <summary>
        /// 武器の切り替え
        /// </summary>
        public override void ChangeWepon()
        {
            //  装備中の武器チェックをアクティブに
            equipmentChect.SetActive(true);

            //  プレイヤーの武器を指定武器に変更
            PlaySceneManager.GetPlayer().ChangeWeapon(weaponData);

            Debug.Log("装備を" + weaponData.WeaponName + "に変更しました");
        }

        /// <summary>
        /// 選択された武器情報を設定
        /// </summary>
        public override void SetSelectWeapon()
        {
            ShopWepon.SetSelectWeapon(this);
        }
    }
}