using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace Kondo
{
    public class TutorialCaughtObject : MonoBehaviour
    {
        public float deleteTime = 1.0f;

        [SerializeField]
        private Transform target;
        private bool isContact = false;
        private float time = 0.0f;


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // 目標座標に移動
            gameObject.transform.DOMove(target.position, 1.0f);

            if (isContact)
            {
                time += Time.deltaTime;
                if (time > deleteTime) 
                {
                    WireTutorialManager.instance.DestoroyCurrentElement();
                    TutorialManager.instance.ChangeMenuSelect();
                    WireTutorialManager.instance.NextSequenceChanged();
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(TagName.Player))
            {
                isContact = true;
            }
        }

      

    }
}


