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

            textAseet = Resources.Load<TextAsset>(aTextName);
            loadText = textAseet.text;
            sprlitText = loadText.Split(char.Parse("\n"));
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
                    item.imageName = sprlit.Remove(0, 1);
                }

                if (sprlit.StartsWith(">"))
                {
                    aLayoutList.Add(item);
                    item = new DisplayLayout();
                }

            }

            return aLayoutList;
            #region コメントアウト

            //    textAseet = Resources.Load<TextAsset>(aTextName);
            //    loadText = textAseet.text;
            //    sprlitHead = loadText.Split(char.Parse("\n"));
            //    sprliteMain = sprlitHead;


            //      string str;

            //    bool flag = false;
            //    for(int j =0;j< 20;j++)
            //    {
            //        for (int i = 0; i < 3; i++)
            //        {
            //            //str = Regex.Match(sprlitHead[count], pattern[i]).Groups["head"].Value;

            //            if (str == null)
            //            {
            //                count++;
            //                Debug.Log("null");
            //                continue;
            //            }

            //            flag = true;

            //            if (i == 0) item.headLine = str;
            //            if (i == 1) item.mainText = str;
            //            if (i == 2) item.imageName = str;

            //            count++;
            //        }

            //        if (flag)
            //        {
            //            flag = false;
            //            layoutList.Add(item);
            //            Debug.Log("タイトル :" + item.headLine);
            //            Debug.Log("メイン :" + item.mainText);
            //            Debug.Log("イメージ :" + item.imageName);
            //        }
            //    }
            #endregion
        }

    }
}

