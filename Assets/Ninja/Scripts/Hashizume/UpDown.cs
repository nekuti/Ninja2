using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour {

    public float duration = 1.0f;
    public float length = 3f;

    private Rigidbody myRigidbody;

    private Vector3 pos;

    // Use this for initialization
    void Start () {
        pos = transform.position;
        myRigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        myRigidbody.velocity = Vector3.zero;

        Vector3 target = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.frameCount * duration) * length, transform.position.z);

        myRigidbody.AddForce(transform.position - target, ForceMode.VelocityChange);

        //transform.position = new Vector3(transform.position.x, pos.y + Mathf.Sin(Time.frameCount * duration) * length, transform.position.z);

    //     public bool MoveTo(Vector3 aPos)
    //{
    //    Vector3 vec = (aPos - transform.position).normalized * enemyData.MoveSpeed;

    //    // Rigidbodyに力を加える
    //    myRigidbody.AddForce(vec, ForceMode.VelocityChange);

    //    // 目的座標に到着したらtrueを返す
    //    if ((aPos - transform.position).magnitude < enemyData.MoveSpeed * Time.deltaTime)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    }
}
