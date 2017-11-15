using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaa : MonoBehaviour {

    public GameObject left;

    public GameObject model;
	// Use this for initialization
	void Start () {
		if(left != null)
        {
            model = left.transform.Find("Model").gameObject;
        }
	}
    

    // Update is called once per frame
    void Update () {
		
	}
}
