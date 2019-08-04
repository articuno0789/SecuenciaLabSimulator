﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{

    float distance = 10;

    private void Awake()
    {
        GameObject cam = GameObject.Find("Main Camera");
        float distance2 = Vector3.Distance(transform.position, cam.transform.position);
        distance = distance2;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = objectPos;
    }
}