using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kondo
{
    public class FindModel : MonoBehaviour
    {

        // 探すオブジェクトを取得
        public List<GameObject> findObjectList = new List<GameObject>();

        public Transform tLeft, tRight;


        // Use this for initialization
        void Start()
        {
            tLeft = FindTransform(findObjectList[0], "Model");   // Controller(left)
            tRight = FindTransform(findObjectList[1], "Model");  // Controller(right)
            TutorialElement.findModel = this;
        }


        // Update is called once per frame
        void Update()
        {

        }


        /// <summary>
        /// 指定したオブジェクトの子から指定したトランスフォームを取得
        /// </summary>
        /// <param name="aFindObject"></param>
        /// <param name="aFindName"></param>
        /// <returns></returns>
        private Transform FindTransform(GameObject aFindObject, string aFindName)
        {
            Transform t = aFindObject.transform.Find(aFindName);
            return t;
        }
    }
}


