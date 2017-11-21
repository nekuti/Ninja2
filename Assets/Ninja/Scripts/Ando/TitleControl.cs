using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleControl : MonoBehaviour {

    public SteamVR_TrackedObject trackdObject;
    public SteamVR_Controller.Device device;

    private static bool gameStart = false;

    // Use this for initialization
    void Start () {
        trackdObject = GetComponent<SteamVR_TrackedObject>();
        if (trackdObject != null) device = SteamVR_Controller.Input((int)trackdObject.index);
    }

    // Update is called once per frame
    void Update () {
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("トリガーが押されました");

            gameStart = true;
        }
    }

    public static bool GetGameStart()
    {
        return gameStart;
    }
}
