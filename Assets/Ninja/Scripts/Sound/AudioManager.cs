using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Ando
{
    /// <summary>
    /// BGMとSEの管理をするマネージャ シングルトンで実装
    /// 最終更新 : 2017/ 4/10
    /// 参考:http://kan-kikuchi.hatenablog.com/entry/AudioManager
    /// </summary>
    public class AudioManager : Ando.SingletonMonoBehaviour<AudioManager>
    {
        //ボリューム保存用のkeyとデフォルト値
        private const string BGM_VOLUME_KEY = "BGM_VOLUME_KEY";
        public const string SE_VOLUME_KEY = "SE_VOLUME_KEY";
        private const float BGM_VOLUME_DEFULT = 1.0f;
        private const float SE_VOLUME_DEFULT = 1.0f;

        //BGMがフェードするのにかかる時間
        private const float BGM_FADE_SPEED_RATE_HIGH = 0.9f;
        private const float BGM_FADE_SPEED_RATE_LOW = 0.3f;
        private float bgmFadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH;

        //次流すBGM名、SE名
        private string nextBGMName;
        private string nextSEName;

        //BGMをフェードアウト中か
        private bool isFadeOut = false;

        //BGM用オーディオソース
        [SerializeField]
        private AudioSource BGMSource;

        //  SE用Prefab
        [SerializeField]
        private GameObject sePrefab;

        //  SEの音量
        private float seVolume = SE_VOLUME_DEFULT;

        //全Audioを保持
        private Dictionary<string, AudioClip> bgmDic, seDic;

        new void Awake()
        {
            //  継承元のAwakeを実行(インスタンスが生成されているかの確認)
            base.Awake();

            //  シーンを切り替えた時に破棄されないように
            DontDestroyOnLoad(this.gameObject);

            //リソースフォルダから全SE&BGMのファイルを読み込みセット
            bgmDic = new Dictionary<string, AudioClip>();
            seDic = new Dictionary<string, AudioClip>();

            //  音の名前を設定
            object[] bgmList = Resources.LoadAll("Audio/BGM");
            object[] seList = Resources.LoadAll("Audio/SE");

            //  オーディオクリップを設定
            foreach (AudioClip bgm in bgmList)
            {
                bgmDic[bgm.name] = bgm;
            }
            foreach (AudioClip se in seList)
            {
                seDic[se.name] = se;
            }
        }

        void Start()
        {
            //  音量を設定
            BGMSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
            seVolume = PlayerPrefs.GetFloat(SE_VOLUME_KEY, SE_VOLUME_DEFULT);
        }

        void Update()
        {
            if (!isFadeOut)
            {
                return;
            }

            //徐々にボリュームを下げていき、ボリュームが0になったらボリュームを戻し次の曲を流す
            BGMSource.volume -= Time.deltaTime * bgmFadeSpeedRate;

            if (BGMSource.volume <= 0)
            {
                BGMSource.Stop();
                BGMSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
                isFadeOut = false;

                if (!string.IsNullOrEmpty(nextBGMName))
                {
                    PlayBGM(nextBGMName);
                }
            }

        }

        #region SE
        /// <summary>
        /// 指定したファイル名のSEを流す。第二引数のdelayに指定した時間だけ再生までの間隔を空ける
        /// </summary>
        public SoundEffectObject PlaySE(string aSEName, Vector3 aSEPos, float aDelay = 0.0f)
        {
            if (!seDic.ContainsKey(aSEName))
            {
                Debug.Log(aSEName + "という名前のSEがありません");

                return null;
            }

            nextSEName = aSEName;

            return Create(aSEPos);
        }

        /// <summary>
        /// SE再生用オブジェクトを生成
        /// </summary>
        /// <param name="aSEPos"></param>
        private SoundEffectObject Create(Vector3 aSEPos)
        {
            if (sePrefab == null)
            {
                return null;
            }

            //  生成したGameObjectを保存
            var se = Instantiate(sePrefab, aSEPos, Quaternion.identity) as GameObject;
            Debug.Log("Prefab生成");

            //  スクリプトを取得
            SoundEffectObject seScript = se.GetComponent<SoundEffectObject>();

            //  各種設定
            seScript.SetSound(seDic[nextSEName] as AudioClip);
            seScript.SetVolume(seVolume);

            return se.GetComponent<SoundEffectObject>();

        }
        #endregion

        #region BGM
        /// <summary>
        /// 指定したファイル名のBGMを流す。ただし既に流れている場合は前の曲をフェードアウトさせてから。
        /// 第二引数のfadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
        /// </summary>
        public void PlayBGM(string aBgmName, float aFadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH)
        {
            if (!bgmDic.ContainsKey(aBgmName))
            {
                Debug.Log(aBgmName + "という名前のBGMがありません");
                return;
            }

            //現在BGMが流れていない時はそのまま流す
            if (!BGMSource.isPlaying)
            {
                nextBGMName = "";
                BGMSource.clip = bgmDic[aBgmName] as AudioClip;
                BGMSource.Play();
            }
            //違うBGMが流れている時は、流れているBGMをフェードアウトさせてから次を流す。同じBGMが流れている時はスルー
            else if (BGMSource.clip.name != aBgmName)
            {
                nextBGMName = aBgmName;
                FadeOutBGM(aFadeSpeedRate);
            }

        }

        /// <summary>
        /// 現在流れている曲をフェードアウトさせる
        /// fadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
        /// </summary>
        public void FadeOutBGM(float aFadeSpeedRate = BGM_FADE_SPEED_RATE_LOW)
        {
            bgmFadeSpeedRate = aFadeSpeedRate;
            isFadeOut = true;
        }
        #endregion

        /// <summary>
        /// BGMとSEのボリュームを別々に変更&保存
        /// </summary>
        public void ChangeVolume(float aBGMVolume, float aSEVolume)
        {
            BGMSource.volume = aBGMVolume;
            seVolume = aSEVolume;

            //  設定されたデータをUnityに保存
            PlayerPrefs.SetFloat(BGM_VOLUME_KEY, aBGMVolume);
            PlayerPrefs.SetFloat(SE_VOLUME_KEY, aSEVolume);
        }
    }
}