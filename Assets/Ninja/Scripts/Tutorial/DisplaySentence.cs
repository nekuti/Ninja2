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
            string path;

            textAseet = Resources.Load<TextAsset>("Text/"+aTextName);
            loadText = textAseet.text;
            sprlitText = loadText.Split('\n','\r');
            path = "Image/Tutorial/";

            DisplayLayout item = new DisplayLayout();
            foreach (var sprlit in sprlitText)
            {
                if(sprlit.StartsWith("!"))
                {
                    path += sprlit.Remove(0, 1) + "/";
                }

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
                    string name = sprlit.Remove(0, 1);
               

                    if (name != "")
                    {
                        Debug.Log("パス : " + path+name);

                        item.sprite = Resources.Load<Sprite>(path+name);
                        Debug.Log("ディスプレイセンテンス　LoadText()  item.sprite : " + item.sprite);

                        if (item.sprite == null)
                        {
                            item.sprite = Resources.Load<Sprite>("Image/Tutorial/htc_vive");
                        }
                    }
                    else
                    {
                        item.sprite = Resources.Load<Sprite>(path+"htc_vive");

                    }



                    // Debug.Log("ディスプレイセンテンス　LoadText()  item.sprite : " + item.sprite);

                }

                if (sprlit.StartsWith(">"))
                {
                    aLayoutList.Add(item);
                    item.headLine = null;
                    item.mainText = null;
                    item.sprite = null;
                }

            }

            return aLayoutList;

        }
    }
}

