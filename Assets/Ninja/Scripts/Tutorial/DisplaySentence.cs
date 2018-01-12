using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;



namespace Kondo
{
    public class DisplaySentence : MonoBehaviour
    {

        // 確認用
        public List<DisplayLayout> LayoutList = new List<DisplayLayout>();

        // Use this for initialization
        void Start()
        {
            //pattern[0] = @"(\[T)(?<head>.+?)(\])";
            //pattern[1] = @"(\[M)(?<main>.+?)(\])";
            //pattern[2] = @"(\[I)(?<image>.+?)(\])";
        }

        // Update is called once per frame
        void Update()
        {

        }

         


        public static List<DisplayLayout> LoadText(string aTextName , List<DisplayLayout> aLayoutList) 
        {
            TextAsset textAseet;
            string loadText;
            string[] sprlitText;

            textAseet = Resources.Load<TextAsset>("Text/"+aTextName);
            loadText = textAseet.text;
            sprlitText = loadText.Split('\n','\r');
            

            DisplayLayout item = new DisplayLayout();
            foreach (var sprlit in sprlitText)
            {

                if (sprlit == "" || sprlit.StartsWith("#"))  continue;

                if (sprlit.StartsWith("$"))
                {
                    item.headLine = sprlit.Remove(0,1);
                }

                if (sprlit.StartsWith("%"))
                {
                    item.mainText += (sprlit.Remove(0, 1) + "\n");

                }

                if (sprlit.StartsWith("&"))
                {
                    string path = "Image/Tutorial/" + sprlit.Remove(0, 1);
                    Debug.Log("パス : " + path + "  文字数 : " + path.Length);

                    item.sprite = Resources.Load<Sprite>(path);
                    Debug.Log("ディスプレイセンテンス　LoadText()  item.sprite : " + item.sprite);

                    if (item.sprite == null)
                    {
                        item.sprite = Resources.Load<Sprite>("noImage");
                    }

                   // Debug.Log("ディスプレイセンテンス　LoadText()  item.sprite : " + item.sprite);

                }

                if (sprlit.StartsWith(">"))
                {
                    aLayoutList.Add(item);
                    item = new DisplayLayout();
                }

            }

            return aLayoutList;

        }

    }
}

