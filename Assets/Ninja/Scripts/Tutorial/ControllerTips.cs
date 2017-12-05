using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kojima;


namespace Kondo
{
    public class ControllerTips : MonoBehaviour
    {
        public string displayText;
        public int fontSize = 14;
        public Transform drawLineFrom;
        public Transform drawLineTo;
        public float lineWidth = 0.001f;
        public Color fontColor = Color.white;
        public Color lineColor = Color.black;
        public Color backGroundColor = Color.black;

        public PartsType searchParts;
        public HandType hand;



        public LineRenderer line;


        // Use this for initialization
        void Start()
        {
            //InputDevice.ClickDownTrriger(HandType.Left);


            ResetTips();

            SetTransform();

            //if(InputDevice.Press(ButtonType.Trigger, HandType.Left))
            //{

            //}
        }

        // Update is called once per frame
        void Update()
        {
            SetLinePos();
            DrawLine();
        }


        public void ResetTips()
        {
            SetText("FrontText");
            SetText("BackText");
            SetLine();
            SetBackGround();
        }


        private void SetTransform()
        {
            
        }


        private void SetLinePos()
        {
            if (drawLineTo == null)
            {
                drawLineTo = FindControllerParts.GetTransfomeParts(hand, searchParts);

            }
        }


        private void SetText(string name)
        {
            Text t = transform.Find("Canvas/" + name).GetComponent<Text>();
            //t.material = Resources.Load("UIText") as Material;
            t.text = displayText;
            t.color = fontColor;
            t.fontSize = fontSize;
        }


        private void SetBackGround()
        {
            Image backGroud = transform.Find("Canvas/BackGround").GetComponent<Image>();
            backGroud.color = backGroundColor;
        }


        private void SetLine()
        {
            line = transform.Find("Line").GetComponent<LineRenderer>();
            //line.material = Resources.Load("TooltipLine") as Material;
            line.material.color = lineColor;
            line.startColor = lineColor;
            line.endColor = lineColor;
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;

            if (drawLineFrom == null)
            {
                drawLineFrom = transform;
            }
        }


        private void DrawLine()
        {
            if (drawLineTo != null)
            {
                line.SetPosition(0, drawLineFrom.position);
                line.SetPosition(1, drawLineTo.position);
            }
        }

    }
}

