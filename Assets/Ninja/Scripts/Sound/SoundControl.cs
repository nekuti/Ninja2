using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kondo
{
    public class SoundControl : MonoBehaviour
    {
        enum playSelect
        {
            None,
            OneShot,
            Loop
        }

        private class Sound
        {
            public string name;
            public AudioSource audioSouce;
            public bool isPlay;
            public playSelect select;
        }

        public AudioSource test;
        [SerializeField]
        private List<AudioClip> clipList = new List<AudioClip>();

        private List<Sound> soundList = new List<Sound>();
      //  private List<AudioSource> sourceList = new List<AudioSource>();

        // Use this for initialization
        void Start()
        {
            Sound sound = new Sound();
            foreach (var list in clipList)
            {
                sound.name = list.name;
                sound.audioSouce = GetComponent<AudioSource>();
                sound.audioSouce.clip = list;
                sound.isPlay = false;
                soundList.Add(sound);
            }

            //gameObject.AddComponent<AudioSource>();
            test = GetComponent<AudioSource>();
            test.clip = clipList[0];
            //test.Play();

        }

        // Update is called once per frame
        void Update()
        {
            //soundList[0].audioSouce;
            if(Input.GetKeyDown(KeyCode.S))
            {
                //AudioPlay(clipList[0].name);
                soundList[0].audioSouce.Play();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                //AudioPlay(clipList[0].name);
                soundList[1].audioSouce.Play();
            }
        }

        bool AudioPlay(string aAudioName)
        {
            int count = 0;
            foreach(var list in soundList)
            {
                if (list.name == aAudioName)
                {
                    soundList[count].isPlay = true;
                    //soundList[count].audioSouce.Play();
                    return true;
                }
                count++;
            }

            return false;
        }
    }
}


