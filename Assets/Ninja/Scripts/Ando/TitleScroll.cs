using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScroll : MonoBehaviour {

    private float time;
    [SerializeField]
    private float moveScale = 1.0f;

    private Vector3 fastPos;
    private Vector3 endPos;

    [SerializeField]
    private bool moveSwitch = true;

    [SerializeField]
    private float waitTime = 5.0f;
    [SerializeField]
    private float WaitTime = 0.0f;
    // Use this for initialization
    void Start () {
        fastPos = this.gameObject.transform.position;

        this.gameObject.transform.position += new Vector3(0, 0, -65);

        endPos = this.gameObject.transform.position;

        WaitTime = waitTime;

        time = 0f;
        time = Random.Range(0.0f, 1.0f);
        Debug.Log(time.ToString());
	}
	
	// Update is called once per frame
	void Update () {

        if (moveSwitch)
        {
            transform.position += new Vector3(0f, 0f, (Mathf.Sin(time) * moveScale));
        }
        else
        {
            WaitTime -= Time.deltaTime;
        }

        if(transform.position.z >= fastPos.z)
        {
            moveSwitch = false;
        }

        if (0.0f > WaitTime)
        {
            moveSwitch = true;

            if (transform.position.z <= endPos.z)
            {
                Destroy(this.gameObject);
            }
        }

        //time += Time.deltaTime;
        time += Random.Range(0.0f, 0.02f);

       // transform.position += new Vector3((Mathf.Sin(time) * moveScale), 0f, 0f);


      
	}
}
