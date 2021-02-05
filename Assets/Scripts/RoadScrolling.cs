﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScrolling : MonoBehaviour
{
    public float scrollSpeed;
    public Vector2 offset;

    private void Update()
    {
        offset = new Vector2(0, Time.time * scrollSpeed);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }

}
