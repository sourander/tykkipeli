using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowForward : MonoBehaviour
{

    public Color RightColor = Color.red;
    public Color UpColor = Color.green;
    public int LineLength = 500;


    // Update is called once per frame
    void Update()
    {
        var forward = transform.TransformDirection(Vector3.right) * LineLength;
        Debug.DrawRay(transform.position, forward, RightColor);

        var upward = transform.TransformDirection(Vector3.up) * LineLength;
        Debug.DrawRay(transform.position, upward, UpColor);
    }
}
