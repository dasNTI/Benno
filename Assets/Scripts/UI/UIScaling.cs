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

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        initialheight = rt.sizeDelta.y;
        initialwidth = rt.sizeDelta.x;
        initialaspectratio = initialwidth / initialheight;
    }
    void Update()
    {
        float height = Screen.height / 759f * initialheight;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, height);

        if (!fitaspect) return;
        rt.sizeDelta = new Vector2(rt.sizeDelta.y * initialaspectratio, rt.sizeDelta.y);
    }
}
