using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Ando
{
    public abstract class StageBace : MonoBehaviour
    {
        // 簡易リザルトシーン生成に使用
        protected static SceneTransitionManager sceneTransitionManager;

        //  ステージがあるか確認
        public bool stageConfirm;

        //  ゴールしたか確認
        protected bool goalFlag = false;

        //  実行するステージ
        protected StageName myStage;
        #region プロパティ
        public StageName MyStage
        {
            //  外部からの変更をできないように
            get { return this.myStage; }
            protected set { }
        }
        public bool GoalFlag
        {
            get { return this.goalFlag; }
            protected set { }
        }
        #endregion

        protected virtual void Start()
        {
            //  シーンの有効化
            stageConfirm = true;
        }

        protected virtual void Update()
        {
            
        }

        /// <summary>
        /// SceneTransitionManagerを登録する
        /// </summary>
        /// <param name="aSceneTransitionManager"></param>
        public static void RgtrSceneTransition(SceneTransitionManager aSceneTransitionManager)
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