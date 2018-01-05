using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject startObj;

        [SerializeField]
        private GoalObj goalObj;

        //  フェードインを実行しているか
        private bool fadeInflag = false;
        //  フェードインの色
        [SerializeField]
        private Color fadeInColor = Color.black;
        //  フェードインの時間
        [SerializeField]
        private float fadeInTime = 1.0f;
        //  フェード後の経過時間
        private float fadeElapsedTime = 0.0f;

        protected void Awake()
        {
            //  開始地点を設定
            PlaySceneManager.SetStartPos(startObj.gameObject.transform.position);

            Debug.Log("設定された値：" + startObj.gameObject.transform.position + "　所持している値：" + PlaySceneManager.GetStartPos());
           
            //  プレイヤーの操作をプレイ用に切り替え
            PlaySceneManager.GetPlayer().ChangeHandState(Kojima.HandStateType.Play);

            //  ステージの繊維をNoneに設定
            PlaySceneManager.SetStageTransition(StageTransition.None);

            //  インスペクターで設定しない変数の初期化
            fadeInflag = false;
            fadeElapsedTime = 0.0f;
        }

        protected void Update()
        {
            //  ゴールした場合
            if(goalObj.GoalFlag == true)
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
                if (fadeElapsedTime < fadeInTime)
                {
                    PlaySceneManager.SetStageTransition(StageTransition.ResultGameClear);
                }
            }
        }

        //  フェードの開始
        public void FadeStart()
        {
            //  フラグをtrueへ
            fadeInflag = true;

            //  指定色、指定時間でフェード開始
            SteamVR_Fade.Start(fadeInColor, fadeInTime);
        }
    }
}