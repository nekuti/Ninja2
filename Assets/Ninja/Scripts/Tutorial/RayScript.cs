using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class RayScript : MonoBehaviour
{
    public GameObject eye;
    private Ray ray;



    // Use this for initialization
    void Start ()
    {
        

    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Z))
        {
            float n = 10;
            ray = new Ray(eye.transform.position, eye.transform.forward);
            Vector3 uiPos = eye.transform.position + eye.transform.rotation * (Vector3.forward * n);
            ParticleEffect.Create(ParticleEffectType.Flash01, uiPos);
        }
	}
}
