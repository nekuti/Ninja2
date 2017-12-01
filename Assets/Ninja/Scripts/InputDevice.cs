using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// InputManagerのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/01
/// </summary>
namespace Kojima
{
    public enum HandType
    {
        Left,
        Right,
    }

    public class InputDevice : MonoBehaviour
    {
        public struct TrackedDevice
        {
            public InputDevice inputDevice;
            public SteamVR_TrackedObject trackedObject;
            public SteamVR_Controller.Device device;
        }

        #region メンバ変数
        [SerializeField, Tooltip("どちらの手か")]
        public HandType handType;

        // 右と左のDevice
        private static TrackedDevice[] trackedDevices = new TrackedDevice[2];

        #endregion

        #region メソッド

        /// <summary>
        /// 初期化処理
        /// </summary>
        void Awake()
        {
            EntryTrackedDevice(this);
        }

        /// <summary>
        /// 更新前処理
        /// </summary>
        void Start()
        {

        }

        /// <summary>
        /// 更新処理
        /// </summary>
        void Update()
        {

        }

        /// <summary>
        /// デバイスを登録する
        /// </summary>
        /// <param name="aHandType"></param>
        /// <returns></returns>
        public static bool EntryTrackedDevice(InputDevice aInputDevice)
        {
            // 右手か左手か
            HandType type = aInputDevice.handType;

            if (trackedDevices[(int)type].trackedObject == null)
            {
                // InputDeviceを登録
                trackedDevices[(int)type].inputDevice = aInputDevice;
                // TrackedObjectを登録
                trackedDevices[(int)type].trackedObject = aInputDevice.GetComponent<SteamVR_TrackedObject>();
                // Deviceを取得
                trackedDevices[(int)type].device = SteamVR_Controller.Input((int)trackedDevices[(int)type].trackedObject.index);

                Debug.Log(type + "の手を登録しました");
                return true;
            }
            else
            {
                Debug.Log("すでに" + type + "の手は登録済みです");
                return false;
            }
        }


        /// <summary>
        /// ボタンに押している
        /// </summary>
        /// <param name="aButtonMask">入力のボタン SteamVR_Controller.ButtonMask.hoge</param>
        /// <param name="aHandType">右か左か</param>
        /// <returns></returns>
        public static bool Press(ButtonType aButton, HandType aHandType)
        {
            if (trackedDevices[(int)aHandType].inputDevice == null)
            {
                Debug.Log(aHandType + "はデバイスが未登録です");
                return false;
            }
            else
            {
                return trackedDevices[(int)aHandType].device.GetPress(aButton.GetButtonMask());
            }
        }

        /// <summary>
        /// ボタンに押された
        /// </summary>
        /// <param name="aButtonMask">入力のボタン SteamVR_Controller.ButtonMask.hoge</param>
        /// <param name="aHandType">右か左か</param>
        /// <returns></returns>
        public static bool PressDown(ButtonType aButton, HandType aHandType)
        {
            if (trackedDevices[(int)aHandType].inputDevice == null)
            {
                Debug.Log(aHandType + "はデバイスが未登録です");
                return false;
            }
            else
            {
                return trackedDevices[(int)aHandType].device.GetPressDown(aButton.GetButtonMask());
            }
        }

        /// <summary>
        /// ボタンに離された
        /// </summary>
        /// <param name="aButtonMask">入力のボタン SteamVR_Controller.ButtonMask.hoge</param>
        /// <param name="aHandType">右か左か</param>
        /// <returns></returns>
        public static bool PressUp(ButtonType aButton, HandType aHandType)
        {
            if (trackedDevices[(int)aHandType].inputDevice == null)
            {
                Debug.Log(aHandType + "はデバイスが未登録です");
                return false;
            }
            else
            {
                return trackedDevices[(int)aHandType].device.GetPressUp(aButton.GetButtonMask());
            }
        }

        /// <summary>
        /// ボタンに触れている
        /// </summary>
        /// <param name="aButtonMask">入力のボタン SteamVR_Controller.ButtonMask.hoge</param>
        /// <param name="aHandType">右か左か</param>
        /// <returns></returns>
        public static bool Touch(ButtonType aButton, HandType aHandType)
        {
            if (trackedDevices[(int)aHandType].inputDevice == null)
            {
                Debug.Log(aHandType + "はデバイスが未登録です");
                return false;
            }
            else
            {
                return trackedDevices[(int)aHandType].device.GetTouch(aButton.GetButtonMask());
            }
        }

        /// <summary>
        /// ボタンに触れた
        /// </summary>
        /// <param name="aButtonMask">入力のボタン SteamVR_Controller.ButtonMask.hoge</param>
        /// <param name="aHandType">右か左か</param>
        /// <returns></returns>
        public static bool TouchDown(ButtonType aButton, HandType aHandType)
        {
            if (trackedDevices[(int)aHandType].inputDevice == null)
            {
                Debug.Log(aHandType + "はデバイスが未登録です");
                return false;
            }
            else
            {
                return trackedDevices[(int)aHandType].device.GetTouchDown(aButton.GetButtonMask());
            }
        }

        /// <summary>
        /// ボタンを離した
        /// </summary>
        /// <param name="aButtonMask">入力のボタン SteamVR_Controller.ButtonMask.hoge</param>
        /// <param name="aHandType">右か左か</param>
        /// <returns></returns>
        public static bool TouchUp(ButtonType aButton, HandType aHandType)
        {
            if (trackedDevices[(int)aHandType].inputDevice == null)
            {
                Debug.Log(aHandType + "はデバイスが未登録です");
                return false;
            }
            else
            {
                return trackedDevices[(int)aHandType].device.GetTouchUp(aButton.GetButtonMask());
            }
        }

        #endregion
    }
}