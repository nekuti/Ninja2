using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerRot : MonoBehaviour {

    public float speed = 1.0f;
    public bool reverse = true;
    private float RotY = 90f;

    // Use this for initialization
    void Start()
    {
        if (!reverse) RotY *= -1;
        else { }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, RotY, 0) * Time.deltaTime * speed, Space.Self);
    }
}
