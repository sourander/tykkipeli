﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotation : MonoBehaviour
{

    public float minRotation;
    public float maxRotation;

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize(); //normalizing the vector. Meaning that all the sum of the vector will be equal to 1.

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // find the angle in degrees
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        if (rotZ > maxRotation)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, maxRotation);
        }

        if (rotZ < minRotation)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, minRotation);
        }
    }
}
