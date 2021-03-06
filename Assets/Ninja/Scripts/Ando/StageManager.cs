﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField]
        protected GameObject startObj;

        [SerializeField]
        protected ElevatorMove elevator;

        //  フェードインを実行しているか
        protected bool fadeInflag = false;
        //  フェードインの色
        [SerializeField]
        protected Color fadeInColor = Color.black;
        //  フェードインの時間
        [SerializeField]
        protected float fadeInTime = 1.0f;
        //  フェード後の経過時間
        protected float fadeElapsedTime = 0.0f;

        protected void Awake()
        {
            //  開始地点を設定
            PlaySceneManager.SetStartPos(startObj.gameObject.transform.position);

            Debug.Log("設定された値：" + startObj.gameObject.transform.position + "　所持している値：" + PlaySceneManager.GetStartPos());
           
            //  プレイヤーの操作をプレイ用に切り替え
            PlaySceneManager.GetPlayer().ChangeHandState(Kojima.HandStateType.Play);

            //  ステージの遷移をNoneに設定
            PlaySceneManager.SetStageTransition(StageTransition.None);

            //  インスペクターで設定しない変数の初期化
            fadeInflag = false;
            fadeElapsedTime = 0.0f;
        }

        protected void Start()
        {
            //  ステージBGMを再生
            AudioManager.Instance.PlayBGM(AudioName.BGM_NOMALSTAGE01);
        }

        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                FadeStart();
            }

            if (elevator != null)
            {           
                //  ゴールした場合
                if (elevator.isMove)
                {
                    //  フェードの開始
                    FadeStart();
                }

                //  フェードを実行中か
                if (fadeInflag)
                {
                    //  経過時間の加算
                    fadeElapsedTime += Time.deltaTime;

                    //  フェードが完了したか
                    if (fadeInTime < fadeElapsedTime)
                    {
                        PlaySceneManager.SetStageTransition(StageTransition.StageClear);
                    }
                }
            }
        }

        //  フェードの開始
        public void FadeStart()
        {
            if (!fadeInflag)
            {
                //  フラグをtrueへ
                fadeInflag = true;
                
                //  指定色、指定時間でフェード開始
                SteamVR_FadeEx.Start(fadeInColor, fadeInTime);
                Debug.Log("フェードを開始");
            }
        }
    }
}