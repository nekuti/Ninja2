using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiling : MonoBehaviour
{
    //[SerializeField, Range(1, 500)]
    //private float tilingSide = 1;

    //[SerializeField, Range(1, 500)]
    //private float tilingHeight = 1;



    private Renderer render;

    private void OnValidate()
    {
        //if (UnityEditor.PrefabUtility.GetPrefabParent(gameObject) != null)
        //{
        //    UnityEditor.PrefabUtility.DisconnectPrefabInstance(gameObject);
        //}
        //render = GetComponent<Renderer>();
        //if (render != null)
        //{
        //    render.material.mainTextureScale = new Vector2(tilingSide, tilingHeight);
        //    //render.sharedMaterial.mainTextureScale = new Vector2(tilingSide, tilingHeight);
        //}
    }
}
