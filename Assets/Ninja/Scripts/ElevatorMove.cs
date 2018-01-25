﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ElevatorMove : MonoBehaviour
{
    [SerializeField]
    private float moveTime = 1f;
    [SerializeField]
    private float moveDistance = 2f;
    [SerializeField,Range(0,10)]
    private float moveDelay = 1f;

    private Vector3 targetPos;

    private Transform elevator;
    public bool isMove = false;

	// Use this for initialization
	void Start ()
    {
        elevator = GetComponent<Transform>().parent.gameObject.transform;
        targetPos = elevator.position + Quaternion.identity * (Vector3.up * moveDistance);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(isMove)
        {
            elevator.DOMove(targetPos, moveTime).SetDelay(moveDelay);
            Ando.AudioManager.Instance.PlaySE(AudioName.SE_ELEVETORRUN, this.transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagName.Player))
        {
            Ando.AudioManager.Instance.PlaySE(AudioName.SE_ELEVETORSTOP, this.transform.position);
            isMove = true;
        }
    }
}
