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
    }

    public static bool RunCheck()
    {
        if (run)
        {
            var compositor = Valve.VR.OpenVR.Compositor;
            //  引数がよくわからないのでとりあえずtrue
            var color = compositor.GetCurrentFadeColor(true);
            Color myColor = new Color(color.r, color.g, color.b, color.a);

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
