using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class DoorAnime : MonoBehaviour
    {
        ////  アニメーションの開始フラグ
        //static private DoorAnimeState animeFlag = DoorAnimeState.Stop;

        ////  最大移動量
        //public float maxMovementAmount = 1.8f;
        ////  移動量
        //public float movementAmount = 0.02f;

        ////  移動方向(true:右 false:左)
        //public bool movementDirection = true;

        //// Use this for initialization
        //void Start()
        //{
        //    //  ドアの移動方向を判定
        //    if (!movementDirection)
        //    {
        //        //  ドアの移動方向に対する補正
        //        movementAmount *= -1;
        //        maxMovementAmount *= -1;
        //    }

        //    //  最大移動量にドアのZ座標を追加
        //    maxMovementAmount += this.gameObject.transform.position.z;

        //    //  ドアのアニメーションフラグを停止に設定
        //    animeFlag = DoorAnimeState.Stop;
        //}

        //// Update is called once per frame
        //void Update()
        //{
        //    //  ドアのアニメーションフラグがスタートの時の処理
        //    if (animeFlag == DoorAnimeState.Start)
        //    {
        //        //  ドアの移動方向を取得
        //        if (!movementDirection)
        //        {
        //            //  移動可能なら移動
        //            if (maxMovementAmount < this.gameObject.transform.position.z)
        //            {
        //                this.gameObject.transform.position += new Vector3(0, 0, movementAmount);
        //            }
        //            else
        //            {
        //                //  アニメーションフラグを終了に
        //                animeFlag = DoorAnimeState.End;
        //            }
        //        }
        //        else
        //        {
        //            //  移動可能なら移動
        //            if (maxMovementAmount > this.gameObject.transform.position.z)
        //            {
        //                this.gameObject.transform.position += new Vector3(0, 0, movementAmount);
        //            }
        //            else
        //            {
        //                //  アニメーションフラグを終了に
        //                animeFlag = DoorAnimeState.End;
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// ドアのアニメーションの状態を変更
        ///// </summary>
        ///// <param name="state"></param>
        //public static void SetDoorAnimeState(DoorAnimeState state)
        //{
        //    animeFlag = state;
        //}

        ///// <summary>
        ///// 現在のドアのアニメーションの状態を取得
        ///// </summary>
        ///// <returns></returns>
        //public static DoorAnimeState GetDoorAnimeState()
        //{
        //    return animeFlag;
        //}
    }
}