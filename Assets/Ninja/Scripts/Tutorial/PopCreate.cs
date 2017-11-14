using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kondo
{
    public class PopCreate : MonoBehaviour
    {

        public GameObject popPrefab;

        // Use this for initialization
        void Start()
        {
            Instantiate(popPrefab);
        }


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                OnChangedButton();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                TutorialManager.Instance.ChangeScene(NextScene.WireTutorial);
            }
        }


        private void OnChangedButton()
        {
            TutorialManager.Instance.NextStateChanged();
        }
    }
}


