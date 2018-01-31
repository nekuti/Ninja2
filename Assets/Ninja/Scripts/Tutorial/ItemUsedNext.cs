using System.Collections;
using System.Collections.Generic;
using UnityEngine;





namespace Kondo
{
    public class ItemUsedNext : MonoBehaviour
    {

        private TutorialManager manager;
        private bool isEnd = false;

        // Use this for initialization
        void Start()
        {
            manager = TutorialManager.instance;
        }

        // Update is called once per frame
        void Update()
        {
            if(!isEnd)
            {
                ItemUsedCheck();
            }
        }


        private void ItemUsedCheck()
        {

            if (manager.GetOrigiriUsed() && manager.GetKatonUsed())
            {
                isEnd = true;
                ItemTutorialManager.instance.NextSequenceChanged();
            }
        }

    }
}
