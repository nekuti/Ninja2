using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Kojima;


namespace Kondo
{


    public enum NextScene
    {
        WireTutorial,
        AttackTutorial,
        BattalTutorial,
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
        public NextScene nextScene;
        public List<GameObject> tipsList = new List<GameObject>();

        [SerializeField]
        private  Player player;

        private List<DisplayLayout> layout = new List<DisplayLayout>();

        [SerializeField]
        private Canvas canvas;
        [SerializeField]
        private Canvas displayCanvas;


        private MoveNotice notice;
        private DisplayText display;

        // 外部からmodelの操作用
        //public static FindModel conModel = null;

        private int noticeCount = 0;
        private int displayCount = 0;



        //
        public GameObject selectButton;
        //


        void Awake()
        {
            Debug.Log("チュートリアルマネージャー　Awake()");

            instance = this;

            // TextのScriptを参照しNoticeを操作可能にする
            notice = canvas.GetComponentInChildren<Text>().GetComponent<MoveNotice>();
            Debug.Log("チュートリアルマネージャー　notice : " + notice);

            // CanvasのScriptを参照しdisplayを操作可能にする
            display = displayCanvas.GetComponent<DisplayText>();
            Debug.Log("チュートリアルマネージャー　display : "+ display);

         
        }


        void Start()
        {
            Debug.Log("チュートリアルマネージャー　Start()");
            // 始めのシーンを動かす
            ChangeScene(nextScene);

           // SetSelectEven(NextWireTutorial);

            Debug.Log("チュートリアルマネージャー　layout : " + layout);
        }


        public void SetSelectEven(UnityEngine.Events.UnityAction aFuncName)
        {
            display.SetSelectEvent(aFuncName);
        }


        /// <summary>
        /// selectBttonからwireTutorilaを進める
        /// </summary>
        public void NextWireTutorial()
        {
            WireTutorialManager.instance.NextSequenceChanged();
        }




        /// <summary>
        /// selectButtonからAttackを進める
        /// </summary>
        public void NextAttackTutorial()
        {
            AttackTutorialManager.instance.NextSequenceChanged();

        }




        /// <summary>
        /// ディスプレイテキストを読み込む
        /// </summary>
        /// <param name="aName">ファイルネーム</param>
        public void LoadText(string aName)
        {
            layout = DisplaySentence.LoadText(aName, layout);
        }



        /// <summary>
        /// 次のチュートリアルへ移行する
        /// </summary>
        public void NextSceneChanged()
        {
            //　重ねたシーンの破棄
            SceneManager.UnloadSceneAsync(nextScene.ToString());
            ChangeScene(++nextScene);
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




        public void ResetPlayerTransfome()
        {
            player.transform.position = new  Vector3(0, 0, 0);
        }





        /// <summary>
        /// Tipsの表示非表示を設定する
        /// </summary>
        /// <param name="isEnabled">表示 = true 非表示 = false</param>
        /// <param name="aHand">設定するコントローラー</param>
        /// <param name="aParts">設定するパーツ</param>
        public void SetEnabledTips(bool isEnabled,HandType aHand,PartsType aParts)
        {
            Debug.Log("チュートリアルマネージャー　SetEnabledTips()");

            tipsList[((int)aHand * 6) + (int)aParts].SetActive(isEnabled);
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




        public void ShowNotice(int aSelect = 0)
        {
            Debug.Log("チュートリアルマネージャー　ShowNotice()");
            notice.RequestDisplay(noticeText[aSelect], 3, 1, 2.5f);
            //noticeCount++;
        }





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
            //display.HideDisplay();
            displayCanvas.gameObject.SetActive(aSet);
        }



        public void HideSelectButton(bool aSet)
        {
            selectButton.SetActive(aSet);
        }




        ///// <summary>
        ///// 次のシーンに遷移する
        ///// </summary>
        ///// <param name="aScene"></param>
        private void ChangeScene(NextScene aScene)
        {
            switch (aScene)
            {
                case NextScene.WireTutorial:
                    {
                        // ワイヤーシーン
                        // ワイヤーチュートリアルを重ねる
                        SceneManager.LoadSceneAsync("WireTutorial", LoadSceneMode.Additive);
                        Debug.Log("ワイヤーシーン");
                        break;
                    }
                case NextScene.AttackTutorial:
                    {
                        // アタックシーン
                        // アタックチュートリアルを重ねる
                        SceneManager.LoadSceneAsync("AttackTutorial", LoadSceneMode.Additive);
                        Debug.Log("アタックシーン");
                        break;
                    }
                case NextScene.BattalTutorial:
                    {
                        // ベースシ―ン
                        nextScene = NextScene.GoToBase;
                        Debug.Log("ベースシーン");
                        break;
                    }
                case NextScene.GoToBase:
                    break;
            }
        }
    }
}


