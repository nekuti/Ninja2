﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class Bomb : Weapon
    {

        protected override void Start()
        {
            base.Start();

            //  プレイヤーの現在の装備を確認
            if (PlaySceneManager.GetPlayer().WeaponData.WeaponName == weaponData.WeaponName)
            {
                //  装備中だったら武器チェックをアクティブに
                equipmentChect.SetActive(true);
            }

            //  武器レベルを表示
            weaponLevel.text = PlaySceneManager.GetBombLevel() + "Lv";
        }

        void Update()
        {
            //  武器レベルを表示
            weaponLevel.text = PlaySceneManager.GetBombLevel() + "LV";
        }

        /// <summary>
        /// 武器のレベルを取得
        /// </summary>
        /// <returns></returns>
        public override int GetWeaponLevel()
        {
            return PlaySceneManager.GetKunaiLevel();
        }

        /// <summary>
        /// 武器のレベルを上昇
        /// </summary>
        public override void AddWeponLevel()
        {
            PlaySceneManager.AddBumbLevel(addWeponLevel);
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