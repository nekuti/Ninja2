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
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}