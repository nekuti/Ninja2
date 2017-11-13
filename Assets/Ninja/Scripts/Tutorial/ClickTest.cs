using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kondo
{
    public class ClickTest : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                TutorialManager.Instance.SetCurrentState(TutorialSequence.ControllerPop);
            }
        }
    }
}