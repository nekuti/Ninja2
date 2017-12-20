using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kondo
{
    public class TipsManager : MonoBehaviour
    {
        public Transform tips;
        public int cout;

        public PartsType searchParts;
        public HandType hand;

        // Use this for initialization
        void Start()
        {
            cout = 0;
        }

        // Update is called once per frame
        void Update()
        {
            tips = ControllerData.instance.GetPartsTransform(hand, searchParts);
            if (Input.GetKeyDown(KeyCode.X)) cout++;
        }
    }
}


