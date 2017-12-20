using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DOTweenRot : MonoBehaviour {
    [SerializeField]
    private RectTransform rectTransform ;

    public GameObject target;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        transform.DOLocalRotate(target.transform.position, 1f);
        transform.DOMove(target.transform.position, 1f);
    }
}
