using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class SummonsSkill : Item
    {
        void Start()
        {
            //  アイテムのデータテーブルを取得
            itemData = PlaySceneManager.GetSummonsSkill().itemData;

            //  名前を表示
            itemName.text = GetItemName();
            //  値段を表示
            itemPrice.text = GetItemPrice().ToString();
            //  所持数を表示
            itemPossession.text = GetItemPossessionNum() + "個";
        }

        // Update is called once per frame
        void Update()
        {
            //  所持数を表示
            itemPossession.text = GetItemPossessionNum() + "個";
        }

        /// <summary>
        /// アイテムの名前を取得
        /// </summary>
        /// <returns></returns>
        public override string GetItemName()
        {
            return itemData.itemName;
        }

        /// <summary>
        /// アイテムの説明を取得
        /// </summary>
        /// <returns></returns>
        public override string GetItemExplanation()
        {
            return itemData.explanation;
        }

        /// <summary>
        /// アイテムの金額を取得
        /// </summary>
        /// <returns></returns>
        public override int GetItemPrice()
        {
            return itemData.price;
        }

        /// <summary>
        /// アイテムの所持数上限を取得
        /// </summary>
        /// <returns></returns>
        public override int GetItemMaxPossessionNum()
        {
            return itemData.maxPossessionNum;
        }

        /// <summary>
        /// アイテムの所持数を取得
        /// </summary>
        /// <returns></returns>
        public override int GetItemPossessionNum()
        {
            return PlaySceneManager.GetSummonsSkill().possession;
        }

        /// <summary>
        /// アイテムの所持数を加算
        /// </summary>
        /// <param name="anAddNum"></param>
        public override void AddPossessionItem(int anAddNum)
        {
            PlaySceneManager.AddPossessionSummonsSkill(anAddNum);
        }

        /// <summary>
        /// 選択されたアイテム情報を設定
        /// </summary>
        public override void SetSelectItem()
        {
            ShopItem.SetSelectItem(this);
        }
    }
}