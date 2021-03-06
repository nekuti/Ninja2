﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Kojima;


namespace Kondo
{


    public enum CurrentScene
    {
        WireTutorial,
        AttackTutorial,
        ItemTutorial,
        GoToBase,
    }




    public class TutorialManager : MonoBehaviour
    {

       

        private string[] noticeText =
        {
            "前を向いてください",
            "コントローラーを\n見てください",
            "移動してみましょう",
            "攻撃してみましょう",
        };


        // 外部から操作用
        public static TutorialManager instance;

        // 現在のシーン
        public CurrentScene currentScene;

        public List<GameObject> tipsList = new List<GameObject>();

        [SerializeField]
        private  Player player;

        [SerializeField]
        private Canvas displayCanvas;

        private List<DisplayLayout> layout = new List<DisplayLayout>();

        public GameObject selectButton;

        private DisplayText display;
        private int displayCount = 0;

        public float fadeTime = 0.5f;
        private float countTime = 0;
        private bool isFadeRequest = false;
        private bool isFadeing = false;

        


        void Awake()
        {
            Debug.Log("チュートリアルマネージャー　Awake()");

            instance = this;

            // TextのScriptを参照しNoticeを操作可能にする
           // notice = canvas.GetComponentInChildren<Text>().GetComponent<MoveNotice>();
            //Debug.Log("チュートリアルマネージャー　notice : " + notice);

            // CanvasのScriptを参照しdisplayを操作可能にする
            display = displayCanvas.GetComponent<DisplayText>();
            Debug.Log("チュートリアルマネージャー　display : "+ display);



            // 始めのシーンを動かす
            ChangeScene(currentScene);

            SteamVR_FadeEx.Start(Color.clear, 0f);
        }


        void Start()
        {
            Ando.AudioManager.Instance.PlayBGM(AudioName.BGM_TUTORIAL01);
            SteamVR_Fade.Start(Color.black, 0);

        }



        void Update()
        {
            SceneChanger();
        }


        /// <summary>
        /// シーンの変更を行う
        /// </summary>
        private void SceneChanger()
        {
          
            if (isFadeRequest)
            {
                countTime += Time.deltaTime;

                if (countTime >= fadeTime)
                {
                    //　フェードが終了している場合
                    if (!isFadeing)
                    {
                        isFadeing = true;
                        countTime = 0;
                    }
                    else
                    {
                        countTime = 0;
                        isFadeing = false;
                        isFadeRequest = false;
                        NextSceneChanged();
                    }

                }
            }
        }



        /// <summary>
        /// 次のチュートリアルへ移行する
        /// </summary>
        private void NextSceneChanged()
        {
            //　重ねたシーンの破棄
            SceneManager.UnloadSceneAsync(currentScene.ToString());
            ChangeScene(++currentScene);
        }



        /// <summary>
        /// シーンチェンジの要求を出す
        /// </summary>
        public void NextSceneRequest()
        {
            SteamVR_Fade.Start(Color.black, fadeTime);
            isFadeRequest = true;
        }


        ///// <summary>
        ///// 次のシーンに遷移する
        ///// </summary>
        ///// <param name="aScene"></param>
        private void ChangeScene(CurrentScene aScene)
        {
            switch (aScene)
            {
                case CurrentScene.WireTutorial:
                    {
                        // ワイヤーシーン
                        // ワイヤーチュートリアルを重ねる
                        SceneManager.LoadSceneAsync("WireTutorial", LoadSceneMode.Additive);
                        Debug.Log("ワイヤーシーン");
                        break;
                    }
                case CurrentScene.AttackTutorial:
                    {
                        // アタックシーン
                        ResetPlayerTransfome();
                        // アタックチュートリアルを重ねる
                        SceneManager.LoadSceneAsync("AttackTutorial", LoadSceneMode.Additive);
                        Debug.Log("アタックシーン");
                        break;
                    }
                case CurrentScene.ItemTutorial:
                    {
                        // アイテムチュートリアル
                        // アイテムチュートリアルを重ねる
                        SceneManager.LoadSceneAsync("ItemTutorial", LoadSceneMode.Additive);
                        break;
                    }
                case CurrentScene.GoToBase:
                    {
                        RemoveDisplay(false);
                        Ando.SceneTransitionManager.Instance.ChangeSceneSingle(Ando.SceneName.PlayScene);
                        Debug.Log("ベースシーン");
                        break;
                    }

            }
        }




        /// <summary>
        /// イベントを登録
        /// </summary>
        /// <param name="aFuncName">関数の名前</param>
        public void SetSelectEven(UnityEngine.Events.UnityAction aFuncName)
        {
            display.SetSelectEvent(aFuncName);
        }




        /// <summary>
        /// 登録したすべてのEventを削除
        /// </summary>
        public void DeleteSelectEvent()
        {
            display.DeleteSelectEvent();
        }




        /// <summary>
        /// ディスプレイテキストを読み込む
        /// </summary>
        /// <param name="aName">ファイルネーム</param>
        public void LoadText(string aName)
        {
            DisplaySentence.LoadText(aName, layout);
        }



        /// <summary>
        /// ハンドステートをメニューに変更する
        /// </summary>
        public  void ChangeMenuSelect()
        {
            Debug.Log("ハンドステートをMenuSelectに変更");
            player.ChangeHandState(HandStateType.MenuSelect);
        }





        /// <summary>
        /// ハンドステートをプレイに変更する
        /// </summary>
        public  void ChangePlay()
        {
            Debug.Log("ハンドステートをPlayに変更");
            player.ChangeHandState(HandStateType.Play);
        }



        /// <summary>
        /// プレイヤーの座標を初期化する
        /// </summary>
        public void ResetPlayerTransfome()
        {
            player.transform.position = new  Vector3(0, 0, 0);
        }




        /// <summary>
        /// 指定したパーツのTipsのアクティブフラグを取得する
        /// </summary>
        /// <param name="aHand"></param>
        /// <param name="aParts"></param>
        /// <returns></returns>
        public bool  GetEnabledTips(HandType aHand, PartsType aParts)
        {
            return tipsList[((int)aHand * 6) + (int)aParts].activeSelf;
        }




        /// <summary>
        /// tipsが全て無効であるか
        /// 全て無効であればtrueを返す
        /// </summary>
        /// <returns></returns>
        public bool GetEnabledAllTips()
        {
            Debug.Log("チュートリアルマネージャー　GetEnabledAllTips()");

            foreach (var list in tipsList)
            {
                if (list.activeSelf)
                {
                    return false;
                }
            }

            return true;
        }



        /// <summary>
        /// Tipsの表示非表示を設定する
        /// </summary>
        /// <param name="isEnabled">表示 = true 非表示 = false</param>
        /// <param name="aHand">設定するコントローラー</param>
        /// <param name="aParts">設定するパーツ</param>
        public void SetEnabledTips(bool isEnabled, HandType aHand, PartsType aParts)
        {
            Debug.Log("チュートリアルマネージャー　SetEnabledTips()");

            tipsList[((int)aHand * 6) + (int)aParts].SetActive(isEnabled);
        }





        /// <summary>
        /// 全てのTipsの表示非表示を設定する
        /// </summary>
        /// <param name="isEnabled">表示 = true 非表示 = false</param>
        public void SetEnabledAllTips(bool isEnabled)
        {
            Debug.Log("チュートリアルマネージャー　SetEnableAllTips()");
            foreach(var list in tipsList)
            {
                list.SetActive(isEnabled);
            }
        }




        ///// <summary>
        ///// プレイヤーへの通知を表示
        ///// </summary>
        ///// <param name="aSelect"></param>
        //public void ShowNotice(int aSelect = 0)
        //{
        //    Debug.Log("チュートリアルマネージャー　ShowNotice()");
        //    notice.RequestDisplay(noticeText[aSelect], 3, 1, 2.5f);
        //}



        /// <summary>
        /// 指定したパーツのTipsのテキストを変更する
        /// </summary>
        /// <param name="aText">表示したいテキスト</param>
        /// <param name="aHand">手の指定</param>
        /// <param name="aParts">パーツの指定</param>
        public void SetTipsText(string aText, HandType aHand, PartsType aParts)
        {
            tipsList[((int)aHand * 6) + (int)aParts].GetComponentInChildren<ControllerTips>().SetText(aText);
        }



        /// <summary>
        /// 次のディスプレイTextを表示する
        /// </summary>
        /// <param name="pos">表示する座標</param>
        public void ShowDisplay(Transform pos)
        {
            RemoveDisplay(true);
            Debug.Log("チュートリアルマネージャー　ShowDisplay()");
            display.RequestDisplay(layout[displayCount], pos);
            displayCount++;
        }


        /// <summary>
        /// ディスプレイの表示を操作
        /// </summary>
        public void RemoveDisplay(bool aSet)
        {
            // 非表示にする
            displayCanvas.gameObject.SetActive(aSet);
        }


        /// <summary>
        /// セレクトボタンの表示非表示を設定
        /// </summary>
        /// <param name="aSet"></param>
        public void HideSelectButton(bool aSet)
        {
            selectButton.SetActive(aSet);
        }

        /// <summary>
        /// SelectButtonのエフェクトを動かす
        /// </summary>
        public void EnabledSelectEffect()
        {
            selectButton.GetComponentInChildren<Ando.SelectButton>().ParticleStart();
        }


        /// <summary>
        /// SelectButtonのエフェクトを止める
        /// </summary>
        public void DisabledSelectEffect()
        {
            selectButton.GetComponentInChildren<Ando.SelectButton>().ParticleStop();
        }



        /// <summary>
        /// プレイヤーがおりぎりを使用したか
        /// </summary>
        /// <returns></returns>
        public bool GetOrigiriUsed()
        {
            if(player.OnigiriUsedCount >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



        /// <summary>
        /// プレイヤーが火遁の巻物を使用したか
        /// </summary>
        /// <returns></returns>
        public bool GetKatonUsed()
        {
            if(player.KatonUsedCount >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}


