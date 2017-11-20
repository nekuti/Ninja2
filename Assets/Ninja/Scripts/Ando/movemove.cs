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

	// Use this for initialization
	void Start () {
        time = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

        switch (moveSwitch)
        {
            case MoveSwitch.UpDown:
                transform.position = new Vector3(0f, Mathf.Sin(time), 0f);
                break;
            case MoveSwitch.LeftRight:
                transform.position = new Vector3(Mathf.Sin(time), 0f, 0f);
                break;
        }

      
	}
}
