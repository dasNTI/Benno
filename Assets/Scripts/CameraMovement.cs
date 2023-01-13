using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{
    public Vector2 UpperLeftLimit;
    public Vector2 LowerRightLimit;
    public float distance = 10;
    public bool follow = true;
    private float walkOffset = 0;
    public bool walkBounce = true;
    private InputMaster input;

    private void Start()
    {
        Application.targetFrameRate = 60;
        input = new InputMaster();
        input.Enable();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!follow) return;

        Transform tr = GameObject.Find("Player").transform;
        float x = tr.position.x;
        float y = tr.position.y;
        float z = tr.position.z;

        float cx = Mathf.Clamp(x, UpperLeftLimit.x, LowerRightLimit.x);
        float cy = Mathf.Clamp(y, LowerRightLimit.y, UpperLeftLimit.y);

        if (input.MainInput.Dir.ReadValue<Vector2>().x != 0 && walkBounce) 
            DOTween.To(() => walkOffset, x => walkOffset = x, -Mathf.Pow(Mathf.Cos((float)Time.timeAsDouble * 5), 4) * 0.01f, 1f);

        if ((input.MainInput.Dir.ReadValue<Vector2>().x == 0 && walkOffset != 0) || !walkBounce) 
            DOTween.To(() => walkOffset, x => walkOffset = x, 0, 1f);

        if (follow) transform.position = new Vector3(cx, cy + walkOffset, cy - distance);
    }
}
