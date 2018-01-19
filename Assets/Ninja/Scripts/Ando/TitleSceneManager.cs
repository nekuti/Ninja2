using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    //  凧の種類
    public enum KiteType
    {
        Start,
        Tutorial,
        End,
        None,
    }

    public class TitleSceneManager : SingletonMonoBehaviour<TitleSceneManager>
    {
        //  シーン遷移マネージャ
        public static SceneTransitionManager sceneTransitionManager;

        //  次に遷移するシーン
        [SerializeField]
        private List<SceneName> nextScene =new List<SceneName> { SceneName.PlayScene, SceneName.TutorialMainScene, SceneName.End };

        //  凧の情報を格納
        public List<Kite> kites;

        //  シーン遷移のステート
        private KiteType transitionState = KiteType.None;

        //  プレイヤーのGameObject
        [SerializeField]
        private Kojima.Player player;

        //  スタート位置
        [SerializeField]
        private GameObject startPos;

        // Use this for initialization
        void Start()
        {
            transitionState = KiteType.None;

            //  画面の色を初期化するためにClearでフェードをかける
            SteamVR_FadeEx.Start(Color.clear, 1);

            if (player != null && startPos != null)
            {
                //  プレイヤーの初期位置を設定
                player.ResetPosition(startPos.transform.position);
            }

            AudioManager.Instance.PlayBGM(AudioName.BGM_NOMALSTAGE01);
        }

        // Update is called once per frame
        void Update()
        {
            /* デバック処理 */
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                transitionState = KiteType.Start;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                transitionState = KiteType.Tutorial;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                transitionState = KiteType.End;
            }
            /* ここまで */

            foreach (Kite kete in kites)
            {
                //  シーン遷移のステートに情報が入っている場合はループを抜ける
                if(transitionState != KiteType.None)
                {
                    break;
                }

                //  凧に当たったか確認
                if (kete.hit)
                {
                    //  シーン遷移のステートに情報を入れる
                    transitionState = kete.myType;
                }
            }

            //  シーン遷移のステートが変更された場合シーンを遷移する
            if (transitionState != KiteType.None)
            {
                sceneTransitionManager.ChangeSceneSingle(nextScene[(int)transitionState]);
            }  
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