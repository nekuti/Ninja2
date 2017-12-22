using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kojima;


namespace Kondo
{
    public class ControllerTips : MonoBehaviour
    {

        enum TipsChild
        {
            BackGround,
            FrontText,
            BackText,
        }



        public string displayText;
        public Transform drawLineFrom;
        public Transform drawLineTo;
        public bool isEnabled;
        public PartsType searchParts;
        public HandType hand;
        public TipsData tipsData;

        private LineRenderer line;
        private Text[] texts = new Text[2];
        private int countCreate = 0;
        private Renderer partsRender;
        private Color basePartsColor;

        private const int LINE_NUM = 1;
        private const int CANVAS_NUM = 2;

        // Use this for initialization
        void Start()
        {
            ResetTips();

            // 左が0~6 右が7~11
            // TutorialMnagerに登録
            TutorialManager.instance.tipsList[((int)hand * 6)+(int)searchParts] = gameObject;
            // inspectorで表示非表示を設定
            gameObject.SetActive(isEnabled);

            partsRender = ControllerData.instance.GetPartsTransform(hand, searchParts).GetComponentInChildren<Renderer>();
            basePartsColor = partsRender.material.color;

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


        /// <summary>
        /// Tipsの着く先のMateroalの色を変更
        /// </summary>
        /// <param name=""></param>
        public void SetMaterialColor(Color aColor)
        {
            partsRender.material.SetColor("_Color",aColor);
        }



        public void ResetMaterialColor()
        {
            partsRender.material.SetColor("_Color", basePartsColor);
        }




        private void ResetTips()
        {
            SetingText(TipsChild.FrontText);
            SetingText(TipsChild.BackText);
            SetLine();
            SetBackGround();
        }


        private void SetLineTo()
        {
            if (drawLineTo == null && ControllerData.instance.IsEndFind)
            {
                Transform trans = ControllerData.instance.GetPartsTransform(hand, searchParts).GetComponentInChildren<Transform>();
                drawLineTo = trans.GetChild(0);

            }
        }



        private void SetingText(TipsChild aChild)
        {
            texts[countCreate] = transform.GetChild(CANVAS_NUM).GetChild((int)aChild).GetComponent<Text>();
            texts[countCreate].material = Resources.Load("UIText") as Material;
            texts[countCreate].text = displayText;
            texts[countCreate].color = tipsData.fontColor;
            texts[countCreate].fontSize = tipsData.fontSize;
            countCreate++;

        }


        private void SetBackGround()
        {
            Image backGroud = transform.GetChild(CANVAS_NUM).GetChild((int)TipsChild.BackGround).GetComponent<Image>();
            backGroud.color = tipsData.backColor;
        }




        private void SetLine()
        {
            line = transform.GetChild(LINE_NUM).GetComponent<LineRenderer>();
            //line.material = Resources.Load("TooltipLine") as Material;
            line.material.color = tipsData.lineColor;
            line.startColor = tipsData.lineColor;
            line.endColor = tipsData.lineColor;
            line.startWidth = tipsData.lineWidth;
            line.endWidth = tipsData.lineWidth;

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

