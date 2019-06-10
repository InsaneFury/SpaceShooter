using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxFX : MonoBehaviour
{
    public Vector2 speed;
    MeshRenderer rend;
    Material m;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        m = rend.material;
    }

    void Update()
    {
        Parallax(m);
    }

    void Parallax(Material mat)
    {
        Vector2 offset = mat.mainTextureOffset;
        offset += speed * Time.deltaTime;
        mat.mainTextureOffset = offset;
    }
}
