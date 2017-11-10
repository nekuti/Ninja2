using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Ando
{
    /// <summary>
    /// シーン遷移の管理
    /// </summary>
    public class SceneTransitionManager :SingletonMonoBehaviour<SceneTransitionManager>
    {
        //  実行しているシーンリスト
        public List<SceneBace> sceneList;

        new void Awake()
        {
            //  継承元のAwakeを実行(インスタンスが生成されているかの確認)
            base.Awake();

            //  シーンを切り替えた時に破棄されないように
            DontDestroyOnLoad(this.gameObject);

            //  シーンリストの初期化
            sceneList = new List<SceneBace>();

            //SceneBaceにSceneTransitionManagerを登録
            SceneBace.RgtrSceneTransition(this);

        }

        /// <summary>
        /// シーンの追加読み込み
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void ChangeScene<T>() where T : SceneBace
        {
            //  新しいシーンを追加
            T newScene = this.gameObject.AddComponent<T>();

            //  シーンリストに追加
            sceneList.Add(newScene);

            //  新しいシーンを読み込む
            SceneManager.LoadScene(newScene.MyScene.IsName(), LoadSceneMode.Additive);
        }

        public void AddSceneBace(SceneBace aSceneBace)
        {
            sceneList.Add(aSceneBace);
        }

        /// <summary>
        /// シーンの破棄
        /// シーンがない場合は何もしない
        /// </summary>
        public void RevocationScene()
        {

        }
    }
}