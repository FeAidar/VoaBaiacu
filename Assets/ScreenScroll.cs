using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScroll : MonoBehaviour
{
    [SerializeField] public float velY;
    [SerializeField] public float velX;

    void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;
        Vector2 offset = mat.mainTextureOffset;
        offset.y += velY * Time.deltaTime / 10f;
        offset.x += velX * Time.deltaTime / 10f;
        mat.mainTextureOffset = offset;
    }
}