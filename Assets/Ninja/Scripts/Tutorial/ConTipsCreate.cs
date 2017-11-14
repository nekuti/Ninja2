using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kondo
{
    public class ConTipsCreate : MonoBehaviour
    {

        public GameObject conTipsPrefab;

        // Use this for initialization
        void Start()
        {
            Instantiate(conTipsPrefab);
        }


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                OnChangedButton();
            }

        }


        private void OnChangedButton()
        {
            TutorialManager.Instance.NextStateChanged();
        }
    }
}

