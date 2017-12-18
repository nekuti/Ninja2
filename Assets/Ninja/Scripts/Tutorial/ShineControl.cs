using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Kondo
{
    public class ShineControl : MonoBehaviour
    {

        [SerializeField]
        private float ShineLength = 1.0f;

        [SerializeField]
        private Color colors = new Color(0,0.918f,1f,1f); 

        private Material emission;

        private bool isStart = false;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if(!isStart)
            {
                ShineExecute();
            }
        }



        private bool ShineExecute()
        {
            emission = GetComponent<Renderer>().material;

            float val = Mathf.PingPong(Time.time, ShineLength);
            float r = colors.r - val * val;
            float g = colors.g - val * val;
            float b = colors.b - val * val;
            Color color = new Color(r, g, b, colors.a);
            emission.SetColor("_EmissionColor", color);
            return false;
        }


        public void  StartShine()
        {
            isStart = true;
        }


        public void FinishShine()
        {
            isStart = false;
        }
    }
}

