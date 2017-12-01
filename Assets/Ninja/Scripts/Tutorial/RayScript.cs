using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class RayScript : MonoBehaviour
{
    public GameObject eye;
    private Ray ray;
    private Canvas canvas;
    private bool t;
    private Vector3 uiPos;
    private float time;

    // Use this for initialization
    void Start ()
    {
        canvas =  GetComponent<Canvas>();
        Vector3 a =  canvas.transform.position;
        time = 0.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Z))
        {
            float n = 10;
            ray = new Ray(eye.transform.position, eye.transform.forward);
            uiPos = eye.transform.position + eye.transform.rotation * (Vector3.forward * n);
            //ParticleEffect.Create(ParticleEffectType.Flash01, uiPos);
        }

        if(t)
        {
            time += 0.3f;
            Vector3.Lerp((Vector3)canvas.transform.position, uiPos, time);
        }
    }
}
