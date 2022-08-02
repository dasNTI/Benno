using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector2 UpperLeftLimit;
    public Vector2 LowerRightLimit;
    public float distance = 10;
    public bool follow = true;

    void Update()
    {
        if (!follow) return;

        Transform tr = GameObject.Find("Player").transform;
        float x = tr.position.x;
        float y = tr.position.y;
        float z = tr.position.z;

        float cx = Mathf.Clamp(x, UpperLeftLimit.x, LowerRightLimit.x);
        float cy = Mathf.Clamp(y, LowerRightLimit.y, UpperLeftLimit.y);

        transform.position = new Vector3(cx, cy, cy - distance);
    }
}
