using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveSwitch
{
   UpDown,
   LeftRight,
}

public class movemove : MonoBehaviour {
    [SerializeField]
    private MoveSwitch moveSwitch = MoveSwitch.LeftRight;

    private float time;
    [SerializeField]
    private float moveScale = 1.0f;

    // Use this for initialization
    void Start () {
        time = 0f;
        time = Random.Range(0.0f, 1.0f);
        Debug.Log(time.ToString());
	}
	
	// Update is called once per frame
	void Update () {
        //time += Time.deltaTime;
        time += Random.Range(0.0f, 0.1f);

        switch (moveSwitch)
        {
            case MoveSwitch.UpDown:
                transform.position += new Vector3(0f, (Mathf.Sin(time)* moveScale), 0f);
                break;
            case MoveSwitch.LeftRight:
                transform.position += new Vector3((Mathf.Sin(time) * moveScale), 0f, 0f);
                break;
        }

      
	}
}
