using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    //  凧の種類
    public enum KiteType
    {
        None,
        Start,
        Tutorial,
        End,
    }

    public class TitleManager : MonoBehaviour
    {
        //  凧の情報を格納
        public List<Kite> kites;

        //  シーン遷移のステート
        private KiteType transitionState = KiteType.None;

        // Use this for initialization
        void Start()
        {
            transitionState = KiteType.None;
        }

        // Update is called once per frame
        void Update()
        {
            foreach(Kite kete in kites)
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

            switch (transitionState)
            {
                case KiteType.Start:
                    Debug.Log("ゲームを開始");
                    break;
                case KiteType.Tutorial:
                    Debug.Log("ゲームをチュートリアル");
                    break;
                case KiteType.End:
                    Debug.Log("ゲームを終了");
                    break;
            }

        }

    }
}