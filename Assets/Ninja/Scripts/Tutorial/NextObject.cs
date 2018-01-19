using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kondo
{
    public class NextObject : MonoBehaviour
    {
        public GameObject effect;
        private GameObject current;

        // Use this for initialization
        private void Awake()
        {
            if (effect != null)
            {
                current = Instantiate(effect);
            }
            else
            {
                Debug.Log(this + "の「effect」がnullです");
            }
        }



        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(TagName.Player))
            {
                Destroy(gameObject.GetComponent<NextObject>());
                Destroy(current);
                WireTutorialManager.instance.NextSequenceChanged();
               
            }
        }

      
    }
}

