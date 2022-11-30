using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaling : MonoBehaviour
{
    public float units = 650;
    public bool fitaspect = true;

    private float initialheight;
    private float initialwidth;
    private float initialaspectratio;
    private RectTransform rt;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
        initialheight = rt.sizeDelta.y;
        initialwidth = rt.sizeDelta.x;
        initialaspectratio = initialwidth / initialheight;
    }
    void Update()
    {
        float height = Screen.height / units * initialheight;
        if (!fitaspect) return;

        rt.sizeDelta = new Vector2(1, height);
        rt.sizeDelta = new Vector2(rt.sizeDelta.y * initialaspectratio, rt.sizeDelta.y);
    }
}
