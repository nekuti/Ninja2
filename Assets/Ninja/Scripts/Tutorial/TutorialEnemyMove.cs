using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemyMove : MonoBehaviour
{
    [SerializeField]
    private Transform pos; 

	// Use this for initialization
	void Start ()
    {
        this.transform.position = pos.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
