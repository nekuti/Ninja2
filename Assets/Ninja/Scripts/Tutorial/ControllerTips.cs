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
        public bool isEnabled;
        public PartsType searchParts;
        public HandType hand;

        private LineRenderer line;
        private Text[] texts = new Text[2];
        private int countCreate = 0;

        // Use this for initialization
        void Start()
        {
            ResetTips();

            
            // 左が0~6 右が7~11
            // TutorialMnagerに登録
            TutorialManager.instance.tipsList[((int)hand * 6)+(int)searchParts] = gameObject;
            // inspectorで表示非表示を設定
            gameObject.SetActive(isEnabled);

        }

        // Update is called once per frame
        void Update()
        {
            if(Time.time > 5)
            {
                SetLineTo();
                DrawLine();
            }
        
        }


        /// <summary>
        /// 外部からTipsに新しいtextを入力する
        /// </summary>
        /// <param name="aText"></param>
        public void SetText(string aText)
        {
            foreach(var t in texts)
            {
                t.text = aText;
            }
        }



        private void ResetTips()
        {
            SetingText("FrontText");
            SetingText("BackText");
            SetLine();
            SetBackGround();
        }


        private void SetLineTo()
        {
            if (drawLineTo == null)
            {
                Transform trans = ControllerData.instance.GetPartsTransform(hand, searchParts).GetComponentInChildren<Transform>();
                drawLineTo = trans.GetChild(0);

            }
        }



        private void SetingText(string name)
        {
            texts[countCreate] = transform.Find("Canvas/" + name).GetComponent<Text>();
            texts[countCreate].material = Resources.Load("UIText") as Material;
            texts[countCreate].text = displayText;
            texts[countCreate].color = fontColor;
            texts[countCreate].fontSize = fontSize;
            countCreate++;

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

