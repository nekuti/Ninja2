using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイ中の手のステート
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/08
/// </summary>
namespace Kojima
{
    public class HandPlayState : State<Hand>
    {
        #region メンバ変数

        // 武器管理クラス
        private WeaponControl weapon;
        
        // ワイヤー管理クラス
        private WireControl wire;

        private GameObject mySelectItem;

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public HandPlayState(Hand owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            Debug.Log("手をPlayモードに変更");
        
            // 武器管理クラスを生成してアタッチ
            weapon = owner.gameObject.AddComponent<WeaponControl>();

            // ワイヤー管理クラスを生成してアタッチ
            wire = owner.gameObject.AddComponent<WireControl>();
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            // アイテム使用の処理==============================================
            // グリップを押した
            if (InputDevice.PressDown(ButtonType.Grip, owner.HandType))
            {
                Debug.Log("アイテム使用ウィンドウを表示");
                // ウィンドウを生成
                if (mySelectItem == null)
                {
                    mySelectItem = GameObject.Instantiate(owner.MyPlayer.SelectItemPrefab, owner.transform.position + owner.transform.forward, owner.transform.rotation);
                    mySelectItem.transform.parent = owner.MyPlayer.transform;

                    // SEを再生
                    Ando.AudioManager.Instance.PlaySE(AudioName.SE_ITEM_WINDOW_OPEN, owner.transform.position);
                }
            }
            // グリップを離した
            if(InputDevice.PressUp(ButtonType.Grip,owner.HandType))
            {
                // グリップを離した時点の正面にレイを飛ばす
                Ray ray = new Ray(owner.shotPos.transform.position, owner.transform.rotation * Vector3.forward);
                RaycastHit[] hit = Physics.RaycastAll(ray);

                // SelectItemオブジェクトにレイが当っていた場合
                if (hit.Length > 0)
                {
                    for (int i = 0; i < hit.Length; i++)
                    {
                        SelectItem obj = hit[i].collider.GetComponent(typeof(SelectItem)) as SelectItem;
                        if (obj != null)
                        {
                            // アイテムを使用する
                            obj.UseItem(this.owner.MyPlayer);
                        }
                    }
                }

                // ウィンドウを消す
                if (mySelectItem != null)
                {
                    GameObject.Destroy(mySelectItem);

                    // SEを再生
                    Ando.AudioManager.Instance.PlaySE(AudioName.SE_ITEM_WINDOW_CLOSE, owner.transform.position);
                }
            }
            // ================================================================

        }

        /// <summary>
        /// このステートから他のステートに遷移するときに一度だけ呼ばれる
        /// </summary>
        public override void Exit()
        {
            // 生成した武器管理クラスを破棄
            GameObject.Destroy(weapon);

            // 生成したワイヤー管理クラスを破棄
            GameObject.Destroy(wire);
        }

        #endregion
    }
}