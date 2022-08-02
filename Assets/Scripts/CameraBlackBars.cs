using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlackBars : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float w = Screen.height * 4f / 3f;
        float dif = Screen.width - w;

        GetComponent<RectTransform>().sizeDelta = new Vector2(dif, GetComponent<RectTransform>().sizeDelta.y);
    }
}
