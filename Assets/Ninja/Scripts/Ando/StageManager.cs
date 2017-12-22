using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject startObj;

        [SerializeField]
        private GameObject goalObj;
       
        // Use this for initialization
        void Awake()
        {
            //  開始地点を設定
            PlaySceneManager.SetStartPos(startObj.gameObject.transform.position);

            Debug.Log("設定された値：" + startObj.gameObject.transform.position + "　所持している値：" + PlaySceneManager.GetStartPos());
           
            //  プレイヤーの操作をプレイ用に切り替え
            PlaySceneManager.GetPlayer().ChangeHandState(Kojima.HandStateType.Play);

            //  ステージの繊維をNoneに設定
            PlaySceneManager.SetStageTransition(StageTransition.None);
        }

    }
}