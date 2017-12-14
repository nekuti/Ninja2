using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メニュー操作の手のステート
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/11
/// </summary>
namespace Kojima
{
    public class HandMenuselectState : State<Hand>
    {
        #region メンバ変数

        // レイが当たっているSelectableオブジェクト
        private ISelectable hitRayObject;

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public HandMenuselectState(Hand owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            Debug.Log("手をMenuselectモードに変更");

            hitRayObject = null;
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            // rayを設定
            Ray ray = new Ray(owner.shotPos.transform.position, owner.transform.rotation * Vector3.forward);
            RaycastHit[] hit;

            hit = Physics.RaycastAll(ray);

            bool hitFlg = false;

            // Selectableオブジェクトがレイに当たった時の処理
            if (hit.Length > 0)
            {
                for (int i = 0; i < hit.Length; i++)
                {
                    ISelectable obj = hit[i].collider.GetComponent(typeof(ISelectable)) as ISelectable;
                    if (obj != null)
                    {
                        if (hitRayObject != obj && hitRayObject != null)
                        {
                            hitRayObject.OutRayObject();
                        }
                        // レイが当たった時の処理を呼び出す
                        if (hitRayObject != obj)
                        {
                            obj.HitRayObject();
                        }
                        hitRayObject = obj;
                        hitFlg = true;
                    }
                }
            }
            // Selectableオブジェクトに当たってない場合
            if (!hitFlg)
            {
                if (hitRayObject != null)
                {
                    // レイが外れた時の処理を呼び出す
                    hitRayObject.OutRayObject();
                }
                hitRayObject = null;
            }

            // レイが当たっている状態でトリガーまたはトラックパッドを押すと決定処理
            if (InputDevice.ClickTrriger(owner.HandType) ||
            InputDevice.PressDown(ButtonType.Touchpad, owner.HandType))
            {
                if (hitRayObject != null)
                {
                    hitRayObject.SelectObject();
                }
            }
        }

        /// <summary>
        /// このステートから他のステートに遷移するときに一度だけ呼ばれる
        /// </summary>
        public override void Exit()
        {
        }

        #endregion
    }
}