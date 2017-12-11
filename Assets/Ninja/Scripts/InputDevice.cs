using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// InputManagerのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/08
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

        [System.NonSerialized]
        public Vector2 touchPos;

        [System.NonSerialized]
        public bool clickFlg;
        [System.NonSerialized]
        public bool oldClickFlg;

        // トリガークリックの境界
        public const float CLICK_BORDER = 0.89f;

        #endregion

        #region メソッド

        /// <summary>
        /// 更新前処理
        /// </summary>
         private void Start()
        {
            EntryTrackedDevice(this);
            touchPos = Vector2.zero;
            clickFlg = false;
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        private void Update()
        {
            // デバイスが未登録であれば登録する
            if(!InputDevice.IsDeviceRegisterd(handType))
            {
                EntryTrackedDevice(this);
            }

            // 最後に触れていたトラックパッドの位置を記憶
            Vector2 pos = GetTouchPos(handType);
            if(pos != Vector2.zero)
            {
                touchPos = pos;
            }

            // 前フレームのクリック判定を保持
            oldClickFlg = clickFlg;
            // 現フレームのクリック判定を記憶
            clickFlg = ClickTrriger(handType);
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
                // TrackedObjectを登録
                trackedDevices[(int)type].trackedObject = aInputDevice.GetComponent<SteamVR_TrackedObject>();
                // TrackedObjectの取得に成功した場合
                if (trackedDevices[(int)type].trackedObject != null)
                {
                    // InputDeviceを登録
                    trackedDevices[(int)type].inputDevice = aInputDevice;
                    // Deviceを取得
                    trackedDevices[(int)type].device = SteamVR_Controller.Input((int)trackedDevices[(int)type].trackedObject.index);

                    Debug.Log(type + "の手を登録しました");
                    return true;
                }
                Debug.Log(type + "の登録に失敗しました");
            }
            else
            {
                Debug.Log("すでに" + type + "の手は登録済みです");
            }
                return false;
        }

        /// <summary>
        /// デバイスが登録済みか
        /// </summary>
        /// <returns></returns>
        public static bool IsDeviceRegisterd(HandType aHandType)
        {
            if(trackedDevices[(int)aHandType].inputDevice == null)
            {
                Debug.Log(aHandType + "はデバイスが未登録です");
                return false;
            }
            return true;
        }

        /// <summary>
        /// コントローラーを振動させる
        /// </summary>
        /// <param name="aPower">振動の大きさ(1~3999)</param>
        /// <param name="aHandType">右か左か</param>
        /// <returns></returns>
        public static bool Pulse(ushort aPower, HandType aHandType)
        {
            if (InputDevice.IsDeviceRegisterd(aHandType))
            {
                trackedDevices[(int)aHandType].device.TriggerHapticPulse(aPower);
                return true;
            }
            return false;
        }

        /// <summary>
        /// トラックパッドに触れているか
        /// </summary>
        /// <param name="aHandType">右か左か</param>
        /// <returns></returns>
        public static bool IsTouch(HandType aHandType)
        {
            if (InputDevice.IsDeviceRegisterd(aHandType))
            {
                return trackedDevices[(int)aHandType].device.GetAxis() != Vector2.zero ? true : false;
            }
            return false;
        }

        /// <summary>
        /// 触れているトラックパッドの位置を取得する
        /// </summary>
        /// <param name="aHandType">右か左か</param>
        /// <returns></returns>
        public static Vector2 GetTouchPos(HandType aHandType)
        {
            if (InputDevice.IsDeviceRegisterd(aHandType))
            {
                return trackedDevices[(int)aHandType].device.GetAxis();
            }
            return Vector2.zero;
        }

        /// <summary>
        /// 最後に触れていたトラックパッドの位置を取得する
        /// </summary>
        /// <param name="aHandType">右か左か</param>
        /// <returns></returns>
        public static Vector2 GetLastTouchPos(HandType aHandType)
        {
            if (InputDevice.IsDeviceRegisterd(aHandType))
            {
                return trackedDevices[(int)aHandType].inputDevice.touchPos;
            }
            return Vector2.zero;
        }

        /// <summary>
        /// トリガーをクリックしている
        /// </summary>
        /// <param name="aHandType"></param>
        /// <returns></returns>
        public static bool ClickTrriger(HandType aHandType)
        {
            if (InputDevice.IsDeviceRegisterd(aHandType))
            {
                float value = trackedDevices[(int)aHandType].device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;
                if(value > CLICK_BORDER)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// トリガーをクリックした
        /// </summary>
        /// <param name="aHandType">右か左か</param>
        /// <returns></returns>
        public static bool ClickDownTrriger(HandType aHandType)
        {
            if (InputDevice.IsDeviceRegisterd(aHandType))
            {
                if (!trackedDevices[(int)aHandType].inputDevice.oldClickFlg)
                {
                    return ClickTrriger(aHandType);
                }
            }
            return false;
        }

        /// <summary>
        /// トリガーのクリックを離した
        /// </summary>
        /// <param name="aHandType">右か左か</param>
        /// <returns></returns>
        public static bool ClickUpTrriger(HandType aHandType)
        {
            if (InputDevice.IsDeviceRegisterd(aHandType))
            {
                if (trackedDevices[(int)aHandType].inputDevice.oldClickFlg)
                {
                    return !ClickTrriger(aHandType);
                }
            }
            return false;
        }

        /// <summary>
        /// ボタンに押している
        /// </summary>
        /// <param name="aButtonMask">入力のボタン</param>
        /// <param name="aHandType">右か左か</param>
        /// <returns></returns>
        public static bool Press(ButtonType aButton, HandType aHandType)
        {
            if (InputDevice.IsDeviceRegisterd(aHandType))
            {
                return trackedDevices[(int)aHandType].device.GetPress(aButton.GetButtonMask());
            }
            return false;
        }

        /// <summary>
        /// ボタンに押された
        /// </summary>
        /// <param name="aButtonMask">入力のボタン</param>
        /// <param name="aHandType">右か左か</param>
        /// <returns></returns>
        public static bool PressDown(ButtonType aButton, HandType aHandType)
        {
            if (InputDevice.IsDeviceRegisterd(aHandType))
            {
                return trackedDevices[(int)aHandType].device.GetPressDown(aButton.GetButtonMask());
            }
            return false;
        }

        /// <summary>
        /// ボタンに離された
        /// </summary>
        /// <param name="aButtonMask">入力のボタン</param>
        /// <param name="aHandType">右か左か</param>
        /// <returns></returns>
        public static bool PressUp(ButtonType aButton, HandType aHandType)
        {
            if (InputDevice.IsDeviceRegisterd(aHandType))
            {
                return trackedDevices[(int)aHandType].device.GetPressUp(aButton.GetButtonMask());
            }
                return false;
        }

        /// <summary>
        /// ボタンに触れている
        /// </summary>
        /// <param name="aButtonMask">入力のボタン</param>
        /// <param name="aHandType">右か左か</param>
        /// <returns></returns>
        public static bool Touch(ButtonType aButton, HandType aHandType)
        {
            if (InputDevice.IsDeviceRegisterd(aHandType))
            {
                return trackedDevices[(int)aHandType].device.GetTouch(aButton.GetButtonMask());
            }
            return false;
        }

        /// <summary>
        /// ボタンに触れた
        /// </summary>
        /// <param name="aButtonMask">入力のボタン</param>
        /// <param name="aHandType">右か左か</param>
        /// <returns></returns>
        public static bool TouchDown(ButtonType aButton, HandType aHandType)
        {
            if (InputDevice.IsDeviceRegisterd(aHandType))
            {
                return trackedDevices[(int)aHandType].device.GetTouchDown(aButton.GetButtonMask());
            }
            return false;
        }

        /// <summary>
        /// ボタンを離した
        /// </summary>
        /// <param name="aButtonMask">入力のボタン</param>
        /// <param name="aHandType">右か左か</param>
        /// <returns></returns>
        public static bool TouchUp(ButtonType aButton, HandType aHandType)
        {
            if (InputDevice.IsDeviceRegisterd(aHandType))
            {
                return trackedDevices[(int)aHandType].device.GetTouchUp(aButton.GetButtonMask());
            }
            return false;
        }

        #endregion
    }
}