using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kondo
{
    public class ConPopCreate : TutorialElement
    {

        [SerializeField]
        private GameObject conPopPrefab;

        private GameObject conPopObj;

        // Use this for initialization
        void Start()
        {
            conPopObj = Instantiate(conPopPrefab);
            Debug.Log("ConPopCreate");

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Destroy(conPopObj);
                OnChangedSequence();
            }

        }
    }
}

