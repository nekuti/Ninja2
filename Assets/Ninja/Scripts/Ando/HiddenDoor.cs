using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    //  ドアのアニメーションの状態
    public enum DoorAnimeState
    {
        Stop,
        Start,
        Run,
        End,
        None,
    }

    public class HiddenDoor : MonoBehaviour,Kojima.ISelectable
    {
        //  アニメーションの開始フラグ
        static private DoorAnimeState animeFlag = DoorAnimeState.None;

        //  停止する回転量
        [SerializeField]
        private float stopRote = 320;

        //  移動速度
        private float speed = 0;

        //  自分が持っているパーティクルシステムを保存
        private ParticleSystem particleSystem;

        // Use this for initialization
        void Start()
        {
            //  ゲームオブジェクトからパーティクルシステムを取得
            particleSystem = this.gameObject.GetComponent<ParticleSystem>();

            //  パーティクルを止める
            particleSystem.Stop();
        }

        // Update is called once per frame
        void Update()
        {
            //  ドアのアニメーションフラグがスタートの時の処理
            if (animeFlag == DoorAnimeState.Run)
            {
                //  停止位置の半分で回転速度を変更
                if (gameObject.transform.eulerAngles.y <= stopRote / 2)
                {
                    speed += 0.1f;
                }
                else
                {
                    //  スピードがマイナスになったか
                    if (speed > 0)
                    {
                        speed -= 0.1f;
                    }
                    else
                    {
                        speed = 0;
                        //  アニメーションフラグを終了に
                        animeFlag = DoorAnimeState.End;
                    }
                }

                //  停止位置を超ていないか確認
                if (gameObject.transform.eulerAngles.y < stopRote)
                {
                    gameObject.transform.eulerAngles += new Vector3(0, speed, 0);
                }
            }

            //  フラグが開始ステートだった場合
            if (animeFlag == DoorAnimeState.Start)
            {
                //  ステートを実行中に変更
                animeFlag = DoorAnimeState.Run;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                particleSystem.Play();
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                particleSystem.Stop();
                particleSystem.Clear();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                particleSystem.Stop();
                animeFlag = DoorAnimeState.Start;
            }
        }

        /// <summary>
        /// ドアのアニメーションの状態を変更
        /// </summary>
        /// <param name="state"></param>
        public static void SetDoorAnimeState(DoorAnimeState state)
        {
            animeFlag = state;
        }

        /// <summary>
        /// 現在のドアのアニメーションの状態を取得
        /// </summary>
        /// <returns></returns>
        public static DoorAnimeState GetDoorAnimeState()
        {
            return animeFlag;
        }

        /// <summary>
        /// レイが当たった
        /// </summary>
        public void HitRayObject()
        {
            //  パーティクルを開始
            particleSystem.Play();
        }

        /// <summary>
        /// レイが外れた
        /// </summary>
        public void OutRayObject()
        {
            //  パーティクルを停止
            particleSystem.Stop();
            //  画面に残ったパーティクルを削除
            particleSystem.Clear();
        }

        /// <summary>
        /// 決定がされた
        /// </summary>
        public void SelectObject()
        {
            //  パーティクルを停止
            particleSystem.Stop();
            //  画面に残ったパーティクルを削除
            particleSystem.Clear();

            //  ステートをアニメーション開始に変更
            animeFlag = DoorAnimeState.Start;
        }
    }
}
