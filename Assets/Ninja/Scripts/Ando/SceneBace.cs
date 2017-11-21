using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Ando
{
    public abstract class SceneBace : MonoBehaviour
    {


        //  SceneBaceを管理するもの
        protected static SceneTransitionManager sceneTransitionManager;
        
        //  シーンがあるか確認
        public bool sceneConfirm;

        //  実行するシーン
        protected SceneName myScene;

        //  現在のシーンの次に遷移するシーン
        [SerializeField]
        protected SceneName nextScene;

        #region プロパティ
        public SceneName MyScene
        {
            //  外部からの変更をできないように
            get { return this.myScene; }
            protected set { }
        }

        public SceneName NextScene
        {
            get { return this.nextScene; }
            protected set { }
        }
        #endregion

        protected virtual void Start()
        {
            //  シーンの有効化
            sceneConfirm = true;
        }

        protected virtual void Update()
        {

        }

        /// <summary>
        /// シーン遷移マネージャの登録
        /// </summary>
        /// <param name="aSceneTransitionManager"></param>
        public static void RgtrSceneTransitionManager(SceneTransitionManager aSceneTransitionManager)
        {
            sceneTransitionManager = aSceneTransitionManager;
        }

        /// <summary>
        /// 継承先の型を取得
        /// </summary>
        /// <returns></returns>
        public abstract System.Type GetTypeInheritance();
    }
}