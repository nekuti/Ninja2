using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kondo
{
    public class ConPopCreate : MonoBehaviour
    {

        public GameObject conPopPrefab;

        // Use this for initialization
        void Start()
        {
            Instantiate(conPopPrefab);
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

