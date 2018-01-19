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
            if (boss.Hp <= 0)
            {
                time += Time.deltaTime;

                if (3 < time)
                {
                    Ando.PlaySceneManager.SetStageTransition(Ando.StageTransition.ResultGameClear);
                }
            }
            else
            {
                Debug.Log(boss.Hp);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Ando.PlaySceneManager.SetStageTransition(Ando.StageTransition.ResultGameClear);
            }
        }
    }
}