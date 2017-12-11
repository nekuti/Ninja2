using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace Kondo
{
    public class TutorialCaughtObject : MonoBehaviour
    {
        [SerializeField]
        private Transform target;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            gameObject.transform.DOMove(target.position, 1.0f);
        }
    }
}


