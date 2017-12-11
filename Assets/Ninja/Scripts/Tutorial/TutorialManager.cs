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
        };
       


        // prefab
        [SerializeField]
        private List<GameObject> sequenceList = new List<GameObject>();

        private List<DisplayLayout> layout = new List<DisplayLayout>();

        public List<GameObject> tipsList = new List<GameObject>();

        [SerializeField]
        private Canvas canvas;

        [SerializeField]
        private Canvas displayCanvas;

        // 外部から操作用
        public static TutorialManager instance;

        // 現在のシーン
        public NextScene nextScene;

        private MoveNotice notice;

        private DisplayText display;
        private int noticeCount = 0;
        private int displayCount = 0;


        // チュートリアルの動作順序
        //private int sequenceNum;

        // 現在の要素
        //private GameObject currentElement;



        void Awake()
        {
            instance = this;

            // 最初のシーンを設定
            nextScene = NextScene.WireTutorial;

            // WireStartPopに設定
            //sequenceNum = (int)TutorialSequence.WireStartPop;

            // TextのScriptを参照しNoticeを操作可能にする
            notice = canvas.GetComponentInChildren<Text>().GetComponent<MoveNotice>();
            // CanvasのScriptを参照しdisplayを操作可能にする
            display = displayCanvas.GetComponent<DisplayText>();

            // 始めの要素生成し要素を保存
            //currentElement = Instantiate(sequenceList[sequenceNum]);


        }


        void Start()
        {
            //Ando.PlaySceneManager.GetStartPos();
            //Quaternion direction = InputTracking.GetLocalRotation(VRNode.Head);
            //Vector3 trm = InputTracking.GetLocalPosition(VRNode.Head);
            ChangeScene(nextScene);
            // テキストをロード
            layout = DisplaySentence.LoadText("displayText", layout);
        }


        // Update is called once per frame
        void Update()
        {
           if (Input.GetKeyDown(KeyCode.A))
            {
            }
        }


        /// <summary>
        /// 状態を次に移行する
        /// </summary>
        public void NextStateChanged()
        {
            //if (currentElement != null)
            //{
            //    Debug.Log(sequenceNum + "エレメント削除");
            //    Destroy(currentElement);
            //    sequenceNum++;
            //}
            //else
            //{
            //    //Debug.Log("エレメントが空");
            //}


            //// 要素が削除されていた場合
            //// シーケンスが最後の時でない場合
            //if (currentElement == null && sequenceNum != (int)TutorialSequence.MaxSequence)
            //{
            //    // 次の要素を実行
            //    currentElement = Instantiate(sequenceList[sequenceNum]);
            //    Debug.Log(sequenceList[sequenceNum] + "Scene");
            //}

        }




        /// <summary>
        /// Tipsの表示非表示を設定する
        /// </summary>
        /// <param name="isEnabled">表示 = true 非表示 = false</param>
        /// <param name="aHand">設定するコントローラー</param>
        /// <param name="aParts">設定するパーツ</param>
        public void SetEnabledTips(bool isEnabled,HandType aHand,PartsType aParts)
        {
            tipsList[((int)aHand * 6) + (int)aParts].SetActive(isEnabled);
        }



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
            notice.RequestDisplay(noticeText[aSelect], 3, 1, 2.5f);
            //noticeCount++;
        }



        public void SetTipsText(string aText, HandType aHand, PartsType aParts)
        {
            tipsList[((int)aHand * 6) + (int)aParts].GetComponentInChildren<ControllerTips>().SetText(aText);
        }



        public void ShowDisplay(Transform pos)
        {
            display.RequestDisplay(layout[displayCount], pos);
            displayCount++;
        }



        public void RemoveDisplay()
        {
            // 非表示にする
            display.HideDisplay();
        }


        ///// <summary>
        ///// 次のシーンに遷移する
        ///// </summary>
        ///// <param name="aScene"></param>
        public void ChangeScene(NextScene aScene)
        {
            switch (aScene)
            {
                case NextScene.WireTutorial:
                    {
                        // アタックシ―ン
                        nextScene = NextScene.AttackTutorial;
                        SceneManager.LoadSceneAsync("WireTutorial", UnityEngine.SceneManagement.LoadSceneMode.Additive);
                        Debug.Log("ワイヤーシーン");
                        break;
                    }
                case NextScene.AttackTutorial:
                    {
                        // バトルシ―ン
                        nextScene = NextScene.BattalTutorial;
                        //　追加読み込みシーンの破棄
                        SceneManager.UnloadSceneAsync("WireTutorial");
                        //SceneManager.LoadSceneAsync("BattleTutorial", UnityEngine.SceneManagement.LoadSceneMode.Additive);
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


