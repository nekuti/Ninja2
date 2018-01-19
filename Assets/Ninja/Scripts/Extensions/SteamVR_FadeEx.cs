using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SteamVR_FadeEx
{
    static Color fadeColor = Color.black;
    static bool run = false;

    public static void Start(Color newColor, float duration, bool fadeOverlay = false)
    {
        SteamVR_Fade.Start(newColor, duration, fadeOverlay);

        fadeColor = newColor;
        run = true;

        Debug.Log("設定した色" + fadeColor);
    }

    public static bool RunCheck()
    {
        if (run)
        {
            var compositor = Valve.VR.OpenVR.Compositor;
            var color = compositor.GetCurrentFadeColor(false);
            Color myColor = new Color(color.r, color.g, color.b, color.a);

            Debug.Log("設定した色:" + fadeColor + "と現在の色:" + myColor + "で比較");

            if (fadeColor == myColor)
            {
                run = false;

                return false;
            }
            return true;
        }

        return false;
    }
}
