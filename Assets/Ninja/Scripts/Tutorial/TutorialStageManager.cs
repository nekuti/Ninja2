//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

using Ando;

namespace Kondo
{
    public enum StageTransition
    {
        None,
        StageChange,
        TutorialEnd,
    }

    public class PlaySceneManager : SingletonMonoBehaviour<PlaySceneManager>
    {
        //  ステージがすでに生成しているか
        private bool stageExist = false;

        [SerializeField]
        private List<StageName> stageList = new List<StageName>();

        private StageName nowStage = StageName.StageTest1;


        private static StageTransition stageTransition = StageTransition.None;


        new void Awake()
        {
            //  継承元のAwakeを実行(インスタンスが生成されているかの確認)
            base.Awake();
        }

        private void Start()
        {
            stageTransition = StageTransition.None;

            StageChange();
        }

        private void Update()
        {
            switch (stageTransition)
            {
            
                case StageTransition.StageChange:
                    //  ステージ変更
                    StageChange();
                    stageTransition = StageTransition.None;
                    break;
                case StageTransition.TutorialEnd:
                    //  

                    stageTransition = StageTransition.None;
                    break;
            }
          
        }

        private void StageChange()
        {

            if(!stageExist)
            {
                // ステージの生成フラグを有効
                stageExist = true;
            }
            else
            {
                // ステージを破棄する
                DestroyCurrentStage();

                if (stageList.Count - 1 > (int)nowStage)
                {
                    nowStage++;
                }
                else
                {
                    nowStage = 0;
                }
            }


            

            //  ステージの読み込み
            SceneManager.LoadScene(stageList[(int)nowStage].ToString(), LoadSceneMode.Additive);

        }


        /// <summary>
        /// 現在のステージを破棄
        /// </summary>
        private void DestroyCurrentStage()
        {
            //  ステージを削除
            SceneManager.UnloadSceneAsync(stageList[(int)nowStage].ToString());
        }



        /// <summary>
        ///  シーン内の遷移
        /// </summary>
        /// <param name="aStageTransition"></param>
        public static void SetStageTransition(StageTransition aStageTransition)
        {
            stageTransition = aStageTransition;
        }
    }
}
