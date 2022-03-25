using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadows : MonoBehaviour
{
    void Start()
    {
        GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }
}
