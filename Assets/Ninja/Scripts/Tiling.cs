using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiling : MonoBehaviour
{
    [SerializeField, Range(1, 100)]
    private float tilingSide = 1;

    [SerializeField, Range(1, 100)]
    private float tilingHeight = 1;



    private Renderer render;

    private void OnValidate()
    {
        render = GetComponent<Renderer>();
        render.material.mainTextureScale = new Vector2(tilingSide, tilingHeight);
    }
}
