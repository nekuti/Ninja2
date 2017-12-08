using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

namespace Ando
{
    /// <summary>
    /// シーン遷移の管理
    /// </summary>
    public class SceneTransitionManager :SingletonMonoBehaviour<SceneTransitionManager>
    {
        //  実行しているシーンリスト
        public List<SceneBace> sceneList= new List<SceneBace>();

        //  シーンリストの初期化
        public SceneName firstScene = SceneName.TitleTest;

        new void Awake()
        {
            //  継承元のAwakeを実行(インスタンスが生成されているかの確認)
            base.Awake();

            //  シーンを切り替えた時に破棄されないように
            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            //SceneBaceにSceneTransitionManagerを登録
            SceneBace.RgtrSceneTransitionManager(this);

            #region スイッチに嫌悪する部分(初回シーン設定)
            switch (firstScene)
            {
                case SceneName.TitleTest:
                    ChangeSceneSingle(SceneName.TitleTest);
                    break;
                case SceneName.PlayTest:
                    ChangeSceneSingle(SceneName.PlayTest);
                    break;
                case SceneName.ResultTest:
                    ChangeSceneSingle(SceneName.ResultTest);
                    break;
                case SceneName.PauseTest:
                    ChangeSceneSingle(SceneName.PauseTest);
                    break;
                case SceneName.TitleScene:
                    ChangeSceneSingle(SceneName.TitleScene);
                    break;
                default:
                    break;
            }
            #endregion
        }

        /// <summary>
        /// シーンの上書き ※すべてのシーンが削除される
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void ChangeSceneSingle<T>() where T : SceneBace
        {
            //  新しいシーンを追加
            T newScene = this.gameObject.AddComponent<T>();

            //  シーンのスクリプトを破棄
            foreach (SceneBace list in sceneList)
            {
                Destroy(list);
            }

            //  シーンリストを初期化
            sceneList = new List<SceneBace>();

            //  シーンリストに追加
            AddSceneBace(newScene);

            //  新しいシーンを読み込む
            SceneManager.LoadScene(newScene.MyScene.IsName(), LoadSceneMode.Single);

            Debug.Log(sceneList[sceneList.Count - 1] + "にシーンが移動しました");
        }
        public void ChangeSceneSingle(SceneName aSceneName)
        {
            //  引数がEndの場合
            if (aSceneName == SceneName.End)
            {
                //  ゲームを終了する
                GameEnd();
                return;
            }
            
            //  新しいシーンを追加
            var newScene = this.gameObject.AddComponent(Type.GetType("Ando."+aSceneName.ToString()));

            //  シーンのスクリプトを破棄
            foreach (SceneBace list in sceneList)
            {
                Destroy(list);
            }

            //  シーンリストを初期化
            sceneList = new List<SceneBace>();

            //  シーンリストに追加
            AddSceneBace(newScene as SceneBace);

            //  新しいシーンを読み込む
            SceneManager.LoadScene(aSceneName.ToString(), LoadSceneMode.Single);

            Debug.Log(sceneList[sceneList.Count - 1] + "にシーンが移動しました");
        }

        /// <summary>
        /// シーンの追加読み込み
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void ChangeSceneAdd<T>() where T : SceneBace
        {
            //  新しいシーンを追加
            T newScene = this.gameObject.AddComponent<T>();

            //  シーンリストに追加
            AddSceneBace(newScene);

            //  新しいシーンを読み込む
            SceneManager.LoadScene(newScene.MyScene.IsName(), LoadSceneMode.Additive);

            Debug.Log(sceneList[sceneList.Count - 1] + "にシーンが移動しました");
        }
        public void ChangeSceneAdd(SceneName aSceneName)
        {
            //  引数がEndの場合
            if (aSceneName == SceneName.End)
            {
                //  ゲームを終了する
                GameEnd();
                return;
            }


            //  新しいシーンを追加
            var newScene = this.gameObject.AddComponent(Type.GetType("Ando." + aSceneName.ToString()));

            //  シーンリストに追加
            AddSceneBace(newScene as SceneBace);

            //  新しいシーンを読み込む
            SceneManager.LoadScene(aSceneName.ToString(), LoadSceneMode.Additive);

            Debug.Log(sceneList[sceneList.Count - 1] + "にシーンが移動しました");
        }

        /// <summary>
        /// シーン情報をリストに追加
        /// </summary>
        /// <param name="aSceneBace"></param>
        public void AddSceneBace(SceneBace aSceneBace)
        {
            sceneList.Add(aSceneBace);

            Debug.Log(sceneList[sceneList.Count - 1] + "が追加されました");
        }

        /// <summary>
        /// シーンの破棄
        /// シーンがない場合は何もしない
        /// </summary>
        public void RevocationScene(SceneName aSceneName)
        {
            //  破棄する配列番号
            int revocation_count = -1;

            //  リスト破棄用のカウンタ
            List<int> revocation_list = new List<int>();

            foreach (SceneBace sceneBace in sceneList)
            {
                revocation_count++;

                if (sceneBace.MyScene == aSceneName)
                {
                    //  シーンのスクリプトを破棄
                    Destroy(sceneBace);

                    revocation_list.Add(revocation_count);
                }
            }

            foreach (int revocationCount in revocation_list)
            {
                //  シーンを破棄
                SceneManager.UnloadSceneAsync(aSceneName.ToString());

                sceneList.RemoveAt(revocationCount);
            }
        }

        /// <summary>
        /// 該当シーンが存在するか
        /// </summary>
        /// <param name="aSceneName"></param>
        /// <returns></returns>
        public bool SearchScene(SceneName aSceneName)
        {
            foreach(SceneBace sceneBace in sceneList)
            {
                if(sceneBace.MyScene == aSceneName)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// ゲームを終了する
        /// </summary>
        private void GameEnd()
        {
            //  シーンのスクリプトを破棄
            foreach (SceneBace list in sceneList)
            {
                Destroy(list);
            }

            //  シーンリストを初期化
            sceneList = new List<SceneBace>();

            //  アプリケーションを終了する
            Application.Quit();
        }
    }
}