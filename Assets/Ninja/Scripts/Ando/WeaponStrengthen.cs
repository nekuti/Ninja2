using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ando
{
    public class WeaponStrengthen : MonoBehaviour
    {
        //  武器の名前
        [SerializeField]
        private Text WeaponName;
        //  武器の説明
        [SerializeField]
        private Text WeaponExplanation;
        //  現在の武器レベル
        [SerializeField]
        private Text nowWeaponLevel;
        //  強化後の武器レベル
        [SerializeField]
        private Text newWeaponLevel;
        //  強化に必要な値段
        [SerializeField]
        private Text WeaponStrengthenPrice;

        //  選択された武器
        [SerializeField]
        private Weapon weapon;

        //  購入成功時の文
        [SerializeField]
        private GameObject strengthAccept;
        //  購入不可時の警告文
        [SerializeField]
        private GameObject strengthWarning;

        private void Update()
        {
            //  変動する値を更新
            nowWeaponLevel.text = weapon.GetWeaponLevel().ToString();
            newWeaponLevel.text = (weapon.GetWeaponLevel() + 1).ToString();
            WeaponStrengthenPrice.text = "費用 " + weapon.GetWeponStrengthenPrice() + "両";
        }


        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            //  nullチェック
            if (ShopWepon.GetSelectWeapon() == null)
            {
                return;
            }

            //  選択されたアイテムを設定
            weapon = ShopWepon.GetSelectWeapon();

            //  固定値を入力
            WeaponName.text = weapon.GetWeaponName();
            WeaponExplanation.text = weapon.GetWeaponExplanation();
            nowWeaponLevel.text = weapon.GetWeaponLevel().ToString();
            newWeaponLevel.text = (weapon.GetWeaponLevel() + 1).ToString();
            WeaponStrengthenPrice.text = "費用 " + weapon.GetWeponStrengthenPrice() + "両";

            //  購入成功時の文を経過時間を初期化後に非アクティブへ
            strengthAccept.gameObject.GetComponent<ConfirmationScreen>().InitElapsedTime();
            strengthAccept.SetActive(false);
            //  購入失敗時の文を経過時間を初期化後に非アクティブへ
            strengthWarning.gameObject.GetComponent<ConfirmationScreen>().InitElapsedTime();
            strengthWarning.SetActive(false);
        }

        /// <summary>
        /// 武器を強化
        /// </summary>
        public void StrengthenWeapon()
        {
            //  減少する金額
            var subMoney = weapon.GetWeponStrengthenPrice();

            //  所持金を超えていないか確認
            if (PlaySceneManager.GetPossessionMoney() >= subMoney)
            {
                Debug.Log(subMoney + "を支払い");
                PlaySceneManager.SubPossessionMoney(subMoney);

                Debug.Log(weapon.GetWeaponName() + "を強化");
                weapon.AddWeponLevel();

                //  購入成功時の文を表示
                strengthAccept.SetActive(true);
                //  表示文の経過時間を初期化
                strengthAccept.GetComponent<ConfirmationScreen>().InitElapsedTime();
                //  購入失敗時の文を非表示
                strengthWarning.SetActive(false);
            }
            else
            {
                //  購入不可時の警告文を表示
                strengthWarning.SetActive(true);
                //  表示文の経過時間を初期化
                strengthWarning.GetComponent<ConfirmationScreen>().InitElapsedTime();
                //  購入成功時の文を非表示
                strengthAccept.SetActive(false);
            }
        }

    }
}
