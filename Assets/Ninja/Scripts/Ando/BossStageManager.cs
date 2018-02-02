using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando {
    public class BossStageManager : StageManager {

        [SerializeField]
        private EnemyBoss boss;

        private float time = 0.0f;

        new void Start() {
            time = 0;

            //  ステージBGMを再生
            AudioManager.Instance.PlayBGM(AudioName.BGM_BOSSSTAGE01);
        }

        new void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                FadeStart();
            }

            if (boss.Hp <= 0)
            {
                FadeStart();
            }


            //  フェードを実行中か
            if (fadeInflag)
            {
                //  経過時間の加算
                fadeElapsedTime += Time.deltaTime;

                //  フェードが完了したか
                if (fadeInTime < fadeElapsedTime)
                {
                    Ando.PlaySceneManager.SetStageTransition(Ando.StageTransition.ResultGameClear);
                    Debug.Log("フェード終わり");
                }
            }    
        }
    }
}