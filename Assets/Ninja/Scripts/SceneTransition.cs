using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト名 ：SceneManager
/// スクリプト機能 ：シーン遷移
/// 作成者 ：安藤 茂貴
/// 更新日時 ：2017/11/10
/// </summary>


//  シーン遷移するシーン
public enum GameScene
{
    Title_A,
    Play_A,
    Result_A,
}

namespace Ando
{
    public abstract class SceneData
    {
        public abstract GameScene previousScene { get; }
        public abstract GameScene[] previousAdd
    }

    public class SceneTransition : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}