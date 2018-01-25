using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kondo
{
    public class NextObject : MonoBehaviour
    {
        public GameObject effect;

        [SerializeField,Range(0,10)]
        private float waitTime = 0;
        private float currentTime = 0;

        private GameObject current;
        private bool isStart = false;

        // Use this for initialization
        private void Awake()
        {
            if (effect != null)
            {
                current = Instantiate(effect);
                current.transform.position = gameObject.transform.position;
            }
            else
            {
                Debug.Log(this + "の「effect」がnullです");
            }
        }


        void Update()
        {
            if(isStart)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= waitTime)
                {
                    Destroy(gameObject.GetComponent<NextObject>());
                    WireTutorialManager.instance.NextSequenceChanged();
                }
            }
        }



        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(TagName.Player))
            {
                Ando.AudioManager.Instance.PlaySE(AudioName.SE_DECISION02, this.transform.position);
                isStart = true;
                Destroy(current);
               
            }
        }

      
    }
}

