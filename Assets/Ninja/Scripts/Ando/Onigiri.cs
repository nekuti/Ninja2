using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class Onigiri : Item
    {
        // Use this for initialization
        protected override void Start()
        {
            base.Start();

            //  所持数を表示
            itemPossession.text = PlaySceneManager.GetPossessionOnigiri().ToString() + "個";
        }

        // Update is called once per frame
        void Update()
        {
            //  所持数を表示
            itemPossession.text = PlaySceneManager.GetPossessionOnigiri().ToString() + "個";
        }

        /// <summary>
        /// アイテムの所持数を取得
        /// </summary>
        /// <returns></returns>
        public override int GetItemPossessionNum()
        {
            return PlaySceneManager.GetPossessionOnigiri();
        }

        /// <summary>
        /// アイテムの所持数を加算
        /// </summary>
        /// <param name="anAddNum"></param>
        public override void AddPossessionItem(int anAddNum)
        {
            PlaySceneManager.AddPossessionOnigili(anAddNum);
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
