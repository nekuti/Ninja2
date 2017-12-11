using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kondo
{
    public class CanvasHide : MonoBehaviour
    {
        private CanvasGroup alpha;

        // Use this for initialization
        void Start()
        {
            alpha = GetComponent<CanvasGroup>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void HideON()
        {
            alpha.alpha = 1.0f;
        }


        public void HideOFF()
        {
            alpha.alpha = 0.0f;
        }
    }
}

