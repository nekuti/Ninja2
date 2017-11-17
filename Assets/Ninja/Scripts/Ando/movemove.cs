using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movemove : MonoBehaviour {

    private float time;

	// Use this for initialization
	void Start () {
        time = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        transform.position = new Vector3(Mathf.Sin(time), 0f, 0f);
	}
}
