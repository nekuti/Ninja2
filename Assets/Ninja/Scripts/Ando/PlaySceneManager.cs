//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Ando
{
    public class PlaySceneManager : SingletonMonoBehaviour<PlaySceneManager>
    {
        [SerializeField]
        private List<StageName> stageList = new List<StageName>();

        [SerializeField]
        private SceneName nextScene = SceneName.ResultTest;
        [SerializeField]
        private SceneName pauseScene = SceneName.PauseTest;

        private StageName nowStage = StageName.StageTest1;

        public static SceneTransitionManager sceneTransitionManager;

        new void Awake()
        {
            //  継承元のAwakeを実行(インスタンスが生成されているかの確認)
            base.Awake();
        }

        private void Start()
        {
            SceneManager.LoadScene(stageList[(int)nowStage].ToString(), LoadSceneMode.Additive);
        }

        private void Update()
        {
            if (!sceneTransitionManager.GetComponent<PlayTest>().PauseFlag)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    sceneTransitionManager.ChangeSceneSingle(nextScene);
                }
                if (Input.GetMouseButtonDown(1))
                {
                    sceneTransitionManager.ChangeSceneAdd(pauseScene);
                    sceneTransitionManager.GetComponent<PlayTest>().PauseFlag = true;
                    Time.timeScale = 0.0f;
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    StageChange();
                }
            }
            else if (sceneTransitionManager.GetComponent<PlayTest>().PauseFlag)
            {
                if (Input.GetMouseButtonDown(2))
                {
                    sceneTransitionManager.GetComponent<PlayTest>().PauseFlag = false;
                    Time.timeScale = 1.0f;
                }
            }
        }

        public void StageChange()
        {
            SceneManager.UnloadSceneAsync(stageList[(int)nowStage].ToString());

            if (stageList.Count - 1 > (int)nowStage)
            {
                nowStage++;
            }
            else
            {
                nowStage = 0;
            }

            SceneManager.LoadScene(stageList[(int)nowStage].ToString(), LoadSceneMode.Additive);
        }

        /// <summary>
        /// シーン遷移マネージャの登録
        /// </summary>
        /// <param name="aSceneTransitionManager"></param>
        public static void RgtrSceneTransitionManager(SceneTransitionManager aSceneTransitionManager)
        {
            sceneTransitionManager = aSceneTransitionManager;
        }

    }
}
