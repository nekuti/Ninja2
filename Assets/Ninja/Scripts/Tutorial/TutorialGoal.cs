using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Kondo
{
    public class TutorialGoal : MonoBehaviour
    {

        private 

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(TagName.Player))
            {
                WireTutorialManager.instance.NextSequenceChanged();
            }
        }

      
    }
}

