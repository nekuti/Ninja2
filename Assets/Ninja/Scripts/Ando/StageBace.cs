using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Ando
{
    public abstract class StageBace : MonoBehaviour
    {
        // マネージャ登録に使用
        protected static PlaySceneManager playSceneManager;

        //  スタート位置
       // protected GameObject startPos;

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

            //  開始位置を登録
            // PlaySceneManager.SetStartPos(startPos.gameObject.transform.position);
        }

        protected virtual void Update()
        {
            
        }

        /// <summary>
        /// PlaySceneManagerを登録する
        /// </summary>
        /// <param name="aPlaySceneManager"></param>
        public static void RgtrPlaySceneManager(PlaySceneManager aPlaySceneManager)
        {
            playSceneManager = aPlaySceneManager;
        }

        /// <summary>
        /// 継承先の型を取得
        /// </summary>
        /// <returns></returns>
        public abstract System.Type GetTypeInheritance();
    }
}