using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class PlayerBaseManager : MonoBehaviour
    {
        //  プレイシーンマネージャ
        public static PlaySceneManager playSceneManager;

        //  
        [SerializeField]
        private List<GameObject> stageSwitch = new List<GameObject>();

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            int i = 0;
           foreach(bool clearFloorLavel in playSceneManager.clearFloorLevel)
            {
                var a = stageSwitch[i].GetComponent<StageSwitch>();

                if (clearFloorLavel)
                {
                    a.ParticleActive(true);
                }
                else
                {
                    a.ParticleActive(false);
                }
                i++;
            }
        }

        /// <summary>
        /// プレイシーンマネージャの登録
        /// </summary>
        /// <param name="aSceneTransitionManager"></param>
        public static void RgtrPlaySceneManager(PlaySceneManager aPlaySceneManager)
        {
            playSceneManager = aPlaySceneManager;
        }
    }
}
