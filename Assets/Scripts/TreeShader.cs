using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeShader : MonoBehaviour
{
    private Material dynamicMaterialInstance;
    private float dynamicOffset = 0f;

    void Start()
    {
        dynamicMaterialInstance = GetComponent<SpriteRenderer>().material;
        dynamicOffset = Random.Range(0f, 200f);
        dynamicMaterialInstance.SetFloat("_WindCount", Time.time + dynamicOffset);
    }

    void Update()
    {
        // Animate the Shininess value
        dynamicMaterialInstance.SetFloat("_WindCount", Time.time + dynamicOffset);
    }
}
